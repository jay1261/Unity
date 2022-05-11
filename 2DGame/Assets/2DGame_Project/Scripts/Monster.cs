using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float MonsterHP = 2;
    public float MonsterPower = 1;
    public int moveDirection = 0; // 0 stop
    public float moveSpeed = 2;
    public bool istracing = false;
    Rigidbody2D rigidbody;
    private Animator animator;
    GameObject traceTarget;
    public GameObject Die;
    public GameObject Chur;
    


    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        StartCoroutine(ChangeDirection());
    }

    private void FixedUpdate()
    {
        Move();
    }

    void OnHit(int dmg)
    {
        MonsterHP -= dmg;
        //피격도 추가

        if (MonsterHP <= 0)
        {
            //Destroy(gameObject);
            

            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(MakeChur());
        }
    }

    IEnumerator MakeChur()
    {
        Vector3 Dieposition = transform.position;
        Destroy(Instantiate(Die, transform.position, transform.rotation), 2f);
        yield return new WaitForSeconds(2f);
        Debug.Log("츄르야");
        Instantiate(Chur, Dieposition, transform.rotation);
        Destroy(gameObject);
    }

    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;
        string dist = "";

        if (istracing)
        {
            Vector3 playerPos = traceTarget.transform.position;
            if (playerPos.x < transform.position.x && (transform.position.x - playerPos.x) > 0.2)
                animator.SetInteger("Direction", 2);
            else if (playerPos.x > transform.position.x && -0.2 > (transform.position.x - playerPos.x))
                animator.SetInteger("Direction", 3);
            else if (playerPos.y < transform.position.y && (transform.position.y - playerPos.y) > 0.2)
                animator.SetInteger("Direction", 0);
            else if ((transform.position.y - playerPos.y) < -0.2)
                animator.SetInteger("Direction", 1);

            transform.position = Vector3.Slerp(transform.position, playerPos, 0.007f * moveSpeed);
        }

        else
        {
            if (moveDirection == 3)
                dist = "Right";
            else if(moveDirection == 2)
                dist = "Left";
            else if (moveDirection == 1)
                dist = "Up";
            else if (moveDirection == 0)
                dist = "Down";
            else if (moveDirection == 4)
                dist = "Stop";
        }

        if(dist == "Left")
        {
            animator.SetInteger("Direction", 2);
            moveVelocity = Vector3.left;
        }
        else if(dist == "Right")
        {
            animator.SetInteger("Direction", 3);
            moveVelocity = Vector3.right;
        }
        else if (dist == "Down")
        {
            animator.SetInteger("Direction", 0);
            moveVelocity = Vector3.down;
        }
        else if (dist == "Up")
        {
            animator.SetInteger("Direction", 1);
            moveVelocity = Vector3.up;
        }
        else if(dist == "Stop")
        {
            moveVelocity = Vector3.zero;
        }

        transform.position += moveVelocity * moveSpeed * Time.deltaTime;
    }

    IEnumerator ChangeDirection()
    {
        moveDirection = Random.Range(0, 5);

        yield return new WaitForSeconds(Random.Range(0.5f,2f));
        StartCoroutine(ChangeDirection());

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CharacterControl player = collision.gameObject.GetComponent<CharacterControl>();
            //collision.rigidbody.AddForce(Vector2.right * 5, ForceMode2D.Impulse);
            player.PlayerHP -= MonsterPower;
        }

        if (collision.gameObject.tag == "Bullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            //Destroy(collision.gameObject);
            OnHit(bullet.dmg);
            animator.SetTrigger("OnHit");
        }

    }

    // 캐릭 검색하면
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            traceTarget = collision.gameObject;
            StopCoroutine(ChangeDirection());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            istracing = true;
            moveSpeed = 3.5f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            istracing = false;
            moveSpeed = 2f;
            StartCoroutine(ChangeDirection());
        }
    }
}
