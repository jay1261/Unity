using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class CarController : MonoBehaviour
{
    public float carspeed = 100;    // 차 스피드
    public float wAngle = 30f; //각도

    public WheelCollider[] wheelColliders = new WheelCollider[4];   // 휠콜라이더
    public Transform[] tireMeshes = new Transform[4];               // 타이어 메쉬
    public Transform centerOfMass;                                  //차 무게중심
    private Rigidbody rigidbody;
    public bool isdrift;
    public float drift_time;
    //-----------------속도-------------
    public Vector3 prePosition;
    public Vector3 nowPosition;

    //-------------드리프트 변수 --------------
    WheelFrictionCurve fowardFrictionCurveRearLeft;
    WheelFrictionCurve sidewaysFrictionCurveRearLeft;
    WheelFrictionCurve fowardFrictionCurveRearRight;
    WheelFrictionCurve sidewaysFrictionCurveRearRight;
    public float slipRate = 1.0f;
    public float handBreakSlipRate = 0.8f;
    public TrailRenderer trr;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.centerOfMass = centerOfMass.localPosition;
        //---------------------브레이크 --------------------------
        fowardFrictionCurveRearLeft = wheelColliders[2].forwardFriction;
        sidewaysFrictionCurveRearLeft = wheelColliders[2].sidewaysFriction;
        fowardFrictionCurveRearRight = wheelColliders[3].forwardFriction;
        sidewaysFrictionCurveRearRight = wheelColliders[3].sidewaysFriction;
        isdrift = false; 
        drift_time = 0f;//아직 안씀
        slipRate = 1.5f; 

        prePosition = gameObject.transform.position;//이전 포지션(속도)
    }

    void Update()
    {
        UpdateMeshesPositions();
        Skidmark();
        getVelocity();
    }

    void FixedUpdate()  //물리 적용 FixedUpdate
    {
        float steer = Input.GetAxis("Horizontal");      //수평 조향
        
        float accelerate = Input.GetAxis("Vertical");

        float finalsteer = steer * wAngle;
        wheelColliders[0].steerAngle = finalsteer;
        wheelColliders[1].steerAngle = finalsteer;


        for (int i = 0; i < 4; i++)
        {
            wheelColliders[i].motorTorque = accelerate * carspeed;
        }
        
        HandBreakDrift();
        booster();
    }
    void UpdateMeshesPositions()
    {
        for(int i = 0; i < 4; i++)
        {
            Quaternion quat;
            Vector3 pos;
            wheelColliders[i].GetWorldPose(out pos, out quat);

            tireMeshes[i].position = pos;
            tireMeshes[i].rotation = quat;
        }
    }

    void Skidmark()
    {
        if (isdrift)
            trr.emitting = true;
        else
            trr.emitting = false;
    }

    void getVelocity()
    {
        var distance = gameObject.transform.position - prePosition;
        var speed = distance.magnitude / Time.deltaTime;
        // print("속력 찍히냐" + speed);//스피드 프린트
        prePosition = gameObject.transform.position;
    }

    void booster()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            GetComponent<Rigidbody>().AddForce(transform.forward * 20000);
        }
    }

    void HandBreakDrift()
    {
        //---------------------드리프트-------------
        //쉬프트 누를때 핸드브레이크(후륜 타이어 마찰 조절)
        if (Input.GetKey(KeyCode.LeftShift))
        {
            fowardFrictionCurveRearLeft.stiffness = handBreakSlipRate;
            wheelColliders[2].forwardFriction = fowardFrictionCurveRearLeft;

            sidewaysFrictionCurveRearLeft.stiffness = handBreakSlipRate;
            wheelColliders[2].sidewaysFriction = sidewaysFrictionCurveRearLeft;

            fowardFrictionCurveRearRight.stiffness = handBreakSlipRate;
            wheelColliders[3].forwardFriction = fowardFrictionCurveRearRight;

            sidewaysFrictionCurveRearRight.stiffness = handBreakSlipRate;
            wheelColliders[3].sidewaysFriction = sidewaysFrictionCurveRearRight;
            isdrift = true;
        }
    
        //키 뗄때 마찰 복원
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            print("마찰 2");
            fowardFrictionCurveRearLeft.stiffness = slipRate;
            wheelColliders[2].forwardFriction = fowardFrictionCurveRearLeft;

            sidewaysFrictionCurveRearLeft.stiffness = 2f;   //slipRate;
            wheelColliders[2].sidewaysFriction = sidewaysFrictionCurveRearLeft;

            fowardFrictionCurveRearRight.stiffness = slipRate;
            wheelColliders[3].forwardFriction = fowardFrictionCurveRearRight;

            sidewaysFrictionCurveRearRight.stiffness = 2f; //slipRate;
            wheelColliders[3].sidewaysFriction = sidewaysFrictionCurveRearRight;
            isdrift = false;
            StartCoroutine(backStiffness());
        }
    }
    IEnumerator backStiffness()
    {
        yield return new WaitForSeconds(2f);

        print("마찰 1");
        fowardFrictionCurveRearLeft.stiffness = 1f;
        wheelColliders[2].forwardFriction = fowardFrictionCurveRearLeft;

        sidewaysFrictionCurveRearLeft.stiffness = 2f;   //slipRate;
        wheelColliders[2].sidewaysFriction = sidewaysFrictionCurveRearLeft;

        fowardFrictionCurveRearRight.stiffness = 1f;
        wheelColliders[3].forwardFriction = fowardFrictionCurveRearRight;

        sidewaysFrictionCurveRearRight.stiffness = 2f; //slipRate;
        wheelColliders[3].sidewaysFriction = sidewaysFrictionCurveRearRight;
    }
}
