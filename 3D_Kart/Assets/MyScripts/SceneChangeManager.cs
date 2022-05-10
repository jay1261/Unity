using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    GameObject MainPanel;
    GameObject Panel_forLevel;
    GameObject Panel_whenPaused;
    GameObject KartClassic_Player;
    
        

    private void Awake()
    {
        Panel_whenPaused = GameObject.Find("Panel_whenPaused");
        if(Panel_whenPaused)
            Panel_whenPaused.SetActive(false);

        MainPanel = GameObject.Find("Panel_Main");
        Panel_forLevel = MainPanel.transform.Find("Panel_forLevel").gameObject;
        if(Panel_forLevel)
            Panel_forLevel.SetActive(false);
        KartClassic_Player = GameObject.Find("KartClassic_Player");
        KartClassic_Player.GetComponent<Rigidbody>().useGravity = false;
    }

    #region selectLevel
    public void selectLevel()
    {
        Panel_forLevel.SetActive(true);
    }
    public void selectLevel1()
    {
        StartCoroutine(waitForStartGame(1));
    }

    public void selectLevel2()
    {
        StartCoroutine(waitForStartGame(2));
    }

    public void selectLevel3()
    {
        StartCoroutine(waitForStartGame(3));
    }

    IEnumerator waitForStartGame(int Level)
    {
        KartClassic_Player.GetComponent<Rigidbody>().useGravity = true;
        KartClassic_Player.GetComponent<Rigidbody>().AddForce(Vector3.forward*5f, ForceMode.Impulse);
        KartClassic_Player.GetComponent<Rigidbody>().AddForce(Vector3.up * 2f, ForceMode.Impulse);
        yield return new WaitForSeconds(3f);
        if (Level == 1)
            SceneManager.LoadScene("Game_L1");
        else if (Level == 2)
            SceneManager.LoadScene("Game_L2");
        else if (Level == 3)
            SceneManager.LoadScene("Game_L3");
    }
    #endregion

    //유니티 종료
    public void GameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    //일시정지
    public void Pause()
    {
        Panel_whenPaused.SetActive(true);
        Time.timeScale = 0f;
    }

    //메인화면으로 이동(끝내기)
    public void moveToMain()
    {
        Panel_whenPaused.SetActive(false);
        SceneManager.LoadScene("Main");
        Time.timeScale = 1f;
    }

    //계속하기
    public void continueGame()
    {
        Panel_whenPaused.SetActive(false);
        Time.timeScale = 1f;
    }
#region reStart
    //재시작 (1,2,3)
    public void reStart_L1()
    {
        Panel_whenPaused.SetActive(false);
        SceneManager.LoadScene("Game_L1");
        Time.timeScale = 1f;
    }

    public void reStart_L2()
    {
        Panel_whenPaused.SetActive(false);
        SceneManager.LoadScene("Game_L2");
        Time.timeScale = 1f;
    }

    public void reStart_L3()
    {
        Panel_whenPaused.SetActive(false);
        SceneManager.LoadScene("Game_L3");
        Time.timeScale = 1f;
    }
#endregion

}
