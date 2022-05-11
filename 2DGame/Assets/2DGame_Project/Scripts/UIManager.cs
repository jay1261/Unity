using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text Text_Chur;
    CharacterControl chur;
    private void Start()
    {
        chur = GameObject.Find("Player_1004").GetComponent<CharacterControl>();
        Text_Chur.text = chur.churCount.ToString();
    }
    
    public void UpDate_ChurCount()
    {
        Text_Chur.text = chur.churCount.ToString();
    }
}
