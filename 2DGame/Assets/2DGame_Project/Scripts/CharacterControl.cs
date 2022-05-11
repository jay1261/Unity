using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public float movespeed;
    public float PlayerHP;
    private Animator animator;
    public GameObject PlayerBullet_A;
    public int churCount;

    private void Start()
    {
        animator = GetComponent<Animator>();
        churCount = 0;
    }

    void Update()
    {
        move();
        gunFire();
    }

    public void Die()
    {
        if (PlayerHP <= 0)
        {
            Debug.Log("player die");
            Destroy(gameObject, 1f);
        }
    }

    void move()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        if (inputX > 0)
        {
            transform.Translate(Vector2.right * movespeed * Time.deltaTime);
            animator.SetInteger("Direction", 3); // Right
        }
        else if (inputX < 0)
        {
            transform.Translate(Vector2.left * movespeed * Time.deltaTime);
            animator.SetInteger("Direction", 2); // Left
        }
        if (inputY > 0)
        {
            transform.Translate(Vector2.up * movespeed * Time.deltaTime);
            animator.SetInteger("Direction", 1); // Up
        }
        else if (inputY < 0)
        {
            transform.Translate(Vector2.down * movespeed * Time.deltaTime);
            animator.SetInteger("Direction", 0); // Down
        }
    }
    
    void gunFire()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (animator.GetInteger("Direction") == 3)
            {
                GameObject bullet = Instantiate(PlayerBullet_A, transform.position + new Vector3(1.1f, 0.7f, 0), transform.rotation);

                Rigidbody2D rigidbody2D = bullet.GetComponent<Rigidbody2D>();
                rigidbody2D.AddForce(Vector2.right * 10, ForceMode2D.Impulse);
            }

            else if (animator.GetInteger("Direction") == 2)
            {
                GameObject bullet = Instantiate(PlayerBullet_A, transform.position + new Vector3(-1.3f, 0.85f, 0), Quaternion.Euler(new Vector3(0, 180, 0)));

                Rigidbody2D rigidbody2D = bullet.GetComponent<Rigidbody2D>();
                rigidbody2D.AddForce(Vector2.left * 10, ForceMode2D.Impulse);
            }

            else if (animator.GetInteger("Direction") == 1)
            {
                GameObject bullet = Instantiate(PlayerBullet_A, transform.position + new Vector3(0.45f, 1.9f, 0), Quaternion.Euler(new Vector3(0, 0, 90)));

                Rigidbody2D rigidbody2D = bullet.GetComponent<Rigidbody2D>();
                rigidbody2D.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
            }

            else if (animator.GetInteger("Direction") == 0)
            {
                GameObject bullet = Instantiate(PlayerBullet_A, transform.position + new Vector3(-0.8f, 0.2f, 0), Quaternion.Euler(new Vector3(0, 0, -90)));

                Rigidbody2D rigidbody2D = bullet.GetComponent<Rigidbody2D>();
                rigidbody2D.AddForce(Vector2.down * 10, ForceMode2D.Impulse);
            }
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            animator.SetTrigger("OnHit");
            Die();
        }
        if (collision.gameObject.tag == "Bullet_Boss")
        {
            Destroy(collision.gameObject);
            animator.SetTrigger("OnHit");
            Die();
        }

        if (collision.gameObject.tag == "Chur")
        {
            Destroy(collision.gameObject);
            churCount += 1;

            UIManager uimanager = GameObject.Find("ScripstManager").GetComponent<UIManager>();
            uimanager.UpDate_ChurCount();

            if(churCount == 5)
            {
                GameManager.Instance.MakeBoss();
            }
        }
    }
}
