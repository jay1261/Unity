using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int dmg = 1;
    public GameObject hits3;
    Rigidbody2D rigidbody2D;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BorderBullet")
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        if (collision.gameObject.tag == "Monster" || collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
            GameObject Hit3 = Instantiate(hits3, transform.position, transform.rotation);
            Destroy(Hit3, 0.3f);
            collision.rigidbody.AddForce(Vector2.up * 6);
        }

        else 
        {
            Destroy(gameObject);
            GameObject Hit3 = Instantiate(hits3, transform.position, transform.rotation);
            Destroy(Hit3, 0.3f);
        }
    }
}
