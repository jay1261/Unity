using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneChangeManager : MonoBehaviour
{
    public GameObject countdownPanel;
    public Text countdownText;
    public void movehome() // 게임씬_게임중단 -> 메인씬 이동
    {
        SceneManager.LoadScene("Main"); // 메인씬 로드
        Time.timeScale = 1f;
    }

    public void replay() // 게임씬_다시하기 -> 게임씬 재로드
    {
        SceneManager.LoadScene("Game"); // 게임씬 다시 로드
    }

    public void Main_play() // 메인씬_시작하기 -> 게임씬 이동
    {
        StartCoroutine(StartCount());
         // 게임씬 다시 로드
    }

    IEnumerator StartCount()
    {
        countdownPanel.SetActive(true);
        GameObject.Find("countdownText").GetComponent<Text>().text = "5";
        yield return new WaitForSecondsRealtime(1.0f);
        GameObject.Find("countdownText").GetComponent<Text>().text = "4";
        yield return new WaitForSecondsRealtime(1.0f);
        GameObject.Find("countdownText").GetComponent<Text>().text = "3";
        yield return new WaitForSecondsRealtime(1.0f);
        GameObject.Find("countdownText").GetComponent<Text>().text = "2";
        yield return new WaitForSecondsRealtime(1.0f);
        GameObject.Find("countdownText").GetComponent<Text>().text = "1";
        yield return new WaitForSecondsRealtime(1.0f);
        GameObject.Find("countdownText").GetComponent<Text>().text = "Start";
        yield return new WaitForSecondsRealtime(0.3f);
        SceneManager.LoadScene("Game");
    }




    public void GameExit() // 메인씬_종료하기 -> 유니티 실행 종료
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
             Application.Quit(); // 어플리케이션 종료
#endif
    }
}
