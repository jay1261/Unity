using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float PlayTime = 0f;
    #region Singleton
    public static GameManager Instance;

    void Awake()
    {
        Instance = GetComponent<GameManager>();
        Instance = this;
        print(Instance + " 할당완료 ");
        PlayTime = 0f;
    }
    #endregion



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayTime += Time.deltaTime; 
    }
}
