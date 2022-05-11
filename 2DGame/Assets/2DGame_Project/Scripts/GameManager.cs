using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float playTime;
    public GameObject boss;
    Vector3 bossPosition;
    #region Singleton
    public static GameManager Instance;

    private void Awake()
    {
        Instance = GetComponent<GameManager>();
        Instance = this;
        playTime = 0f;
        bossPosition = new Vector3(4.86f, 17.33f, 0);
    }
    #endregion

    private void Update()
    {
        playTime += Time.deltaTime;
    }

    public void MakeBoss()
    {
        Instantiate(boss,bossPosition,transform.rotation);
    }

}
