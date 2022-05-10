using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterControll : MonoBehaviour
{
    public GameObject goal;
    public GameObject Santa;
    public GameObject obs;
    public float movespeed;
    public float dashspeed;
    public float JumpDelayTime = 0f;
    public bool isJumpDelay;
    public Text cooltime;
    //public AudioClip AudioClip;
    //public AudioSource AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        movespeed = 10f;
        dashspeed = 3f;
        isJumpDelay = false;
        //AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                GetComponent<Rigidbody>().AddForce(Vector3.back * dashspeed);
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                GetComponent<Rigidbody>().AddForce(Vector3.forward * dashspeed);
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                GetComponent<Rigidbody>().AddForce(Vector3.right * dashspeed);
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                GetComponent<Rigidbody>().AddForce(Vector3.left * dashspeed);
            }
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            transform.Translate(Vector3.back * movespeed * Time.deltaTime);

        //대쉬 애니메이션
        if(Input.GetKey(KeyCode.LeftShift))
        {
            GetComponent<Animator>().SetBool("isDash", true);
            Invoke("scale1", 2f);
        }
        else {
            GetComponent<Animator>().SetBool("isDash", false);
        }




        //점프
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(!isJumpDelay)
            {
                GetComponent<Rigidbody>().AddForce(Vector3.up * movespeed * 50);
                SoundManager._Instance.JumpSound();
                isJumpDelay = true;
                StartCoroutine(CountJumpDelay());
                JumpDelayTime = 0f;
            }
        }
        if (!isJumpDelay)
        {
            cooltime.text = "점프 가능";
        }
        else
        {
            cooltime.text = "쿨타임 : " + (1.0f - JumpDelayTime).ToString("N2");
        }
        //점프 쿨타임 카운트해주는 코드
        JumpDelayTime += Time.deltaTime;
    }
    //점프 코루틴 카운트
    IEnumerator CountJumpDelay()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        isJumpDelay = false;
        //cooltime.text = "점프 가능";
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Debug.Log("1. 충돌 발생 대상 : ", collision.gameObject);
        // print(collision.gameObject.name);

        // goal에 pig를 지정했고, 충돌한 collision.gameObject가 pig이다.
        if (collision.gameObject == goal)
        {
            Debug.Log("1. Angel와 충돌함");
            SoundManager._Instance.GameOverSound();
            SceneManager.LoadScene(0); // 씬을 다시 불러온다.
        }
        if (collision.collider.CompareTag("Finish"))
        {
            Debug.Log("2. Angel와 충돌함");
            SceneManager.LoadScene(0); // 씬을 다시 불러온다.
        }
        //산타, 탱구리몬 충돌시 다시시작
        if (collision.collider.CompareTag("Obstacles")) {
            Debug.Log("장애물에 충돌");
            SceneManager.LoadScene(0);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //싼타와 충돌했을시 출력하고, 재시작
        if(other.gameObject == Santa)
        {
            Debug.Log("산타와 충돌");
            SceneManager.LoadScene(0);
        }
        // 레이저 충돌시 돌떨어짐
        if(other.gameObject.CompareTag("trigger1"))
        {
            Destroy(obs);
        }
    }
}