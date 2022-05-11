using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int bossHP;
    public int bossPower;
    public bool isDead_boss;
    public GameObject bullet_Boss;
    GameObject player;
    public float bulletCooltime;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player_1004");
    }
    private void Update()
    {
        bulletCooltime += Time.deltaTime;
    }

    void OnHit(int dmg)
    {
        bossHP -= dmg;
        //피격도 추가

        if (bossHP <= 0)
        {
            Destroy(gameObject);
            isDead_boss = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CharacterControl player = collision.gameObject.GetComponent<CharacterControl>();
            //collision.rigidbody.AddForce(Vector2.right * 5, ForceMode2D.Impulse);
            player.PlayerHP -= bossPower;
        }

        if (collision.gameObject.tag == "Bullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            //Destroy(collision.gameObject);
            OnHit(bullet.dmg);
            animator.SetTrigger("OnHit");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("캬오");

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("트리거 ");
        if (collision.gameObject.tag == "Player")
        {
            if (bulletCooltime >= 0.2f)
            {
                GameObject BB = Instantiate(bullet_Boss, Vector3.zero, transform.rotation);
                Rigidbody2D rigidbody = BB.GetComponent<Rigidbody2D>();
                Destroy(BB, 5f);
                Vector3 bulletDirection = -player.transform.position - (transform.position + new Vector3(0, 5, 0));
                rigidbody.AddForce(bulletDirection.normalized * 5, ForceMode2D.Impulse);
                bulletCooltime = 0f;
            }
        }
    }

}
