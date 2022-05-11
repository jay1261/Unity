using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    public GameObject player;
    public float speed;
    Vector3 PT;

    void Start()
    {
        PT = player.transform.position;
    }

    void Update()
    {

        PT.Set(player.transform.position.x, player.transform.position.y, this.transform.position.z);

        this.transform.position = Vector3.Lerp(this.transform.position, PT, speed * Time.deltaTime);
    }
}
