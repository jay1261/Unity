using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car2 : MonoBehaviour
{
    Rigidbody rigidbody;
    public float moveSpeed;
    public float rotationSpeed = 5f;
    public float boosterPower = 100f;
    bool isBoosterCooltime;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //transform.position += direction * moveSpeed * Time.deltaTime;
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);
        if(v>0)
            //rigidbody.AddForce(transform.forward * moveSpeed * 1000*Time.deltaTime);
        transform.Translate(this.transform.rotation * Vector3.forward * moveSpeed *Time.deltaTime);
        
        //if(v<0)
            //rigidbody.AddForce(-transform.forward * moveSpeed * 1000);
        if (h!=0)
        {
            transform.Rotate(new Vector3(0,h * 2.2f,0));
        }
    }
}
