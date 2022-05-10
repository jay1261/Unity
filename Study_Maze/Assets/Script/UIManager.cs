using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text PlayTimeText;
    public GameObject pausepanel;
    //일시정지
    private void Start()
    {
        pausepanel.SetActive(false);
        PlayTimeText.text = "lkkjkj";
    }
    public void Pause()
    {
        print("퍼즈 호출");
        pausepanel.SetActive(true);
        Time.timeScale = 0f;
    }
    //계속하
    public void play()
    {
        Time.timeScale = 1f;
        pausepanel.SetActive(false);
    }
    void Update()
    {
        if (GameManager.Instance == null)
            print("게임 매니져 객체 생성 전임!");
        PlayTimeText.text = GameManager.Instance.PlayTime.ToString("N2");
    }
}
