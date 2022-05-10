using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toGetCSVData : MonoBehaviour
{
    public GameObject player;

    private void Update()
    {
        KartGame_Data temp = new KartGame_Data
        {
            playtime = GameManager.Instance.playTime,
            carspeed = player.GetComponent<Rigidbody>().velocity.magnitude,
            isbooster = player.GetComponent<KartGame.KartSystems.ArcadeKart>().isbooster
        };

        CSVData.Data.Add(temp);
    }
}
