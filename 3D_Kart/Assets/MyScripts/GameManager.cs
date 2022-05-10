using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float playTime;


    #region Singleton
    public static GameManager Instance;

    private void Awake()
    {
        Instance = GetComponent<GameManager>();
        Instance = this;
        playTime = 0f;
    }
    #endregion

    void Update()
    {
        //3초 대기 추가??
        playTime += Time.deltaTime;
    }
}