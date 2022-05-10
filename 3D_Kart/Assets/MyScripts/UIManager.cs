using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using KartGame;


public class UIManager : MonoBehaviour
{
    public Text text_playTime;
    public Text text_result;

    GameObject Panel_forFinish;
    public GameObject player;
    private void Start()
    {
        Panel_forFinish = GameObject.Find("Panel_forFinish");
        if(Panel_forFinish)
        {
            Panel_forFinish.SetActive(false);
            StartCoroutine(beforeGameStart3Second());
        }
    }
    IEnumerator beforeGameStart3Second()
    {
        yield return new WaitForSeconds(3f);
        GameManager.Instance.playTime = 0;
    }
    


    public void getPlaytimeFunc()
    {
        StartCoroutine(getPlaytime());
    }
    public IEnumerator getPlaytime()
    {
        if (Panel_forFinish.activeSelf == true)
        {
            float playtime = GameManager.Instance.playTime;
            text_playTime.text = "기록" + playtime.ToString("N4");

            if (playtime <= 60)
            {
                text_result.text = "통과";
            }
            else
            {
                text_result.text = "실패";
            }

            // 디비 연동 코드
            string date = System.DateTime.Now.ToString("yyyy년 MM월 dd일 HH시 mm분 ss초");
            date = SqlFormat(date);
            sqlite.DbConnectionCHek(); // DB 연결, 연결상태 확인
            string sql = string.Format("Insert into Game(Datetime, Playtime, Score) VALUES({0}, {1}, {2})", date, playtime, 0);
            print(sql);
            sqlite.DatabaseSQLAdd(sql); // 위에서 짠 SQL문을 디비에 쏴주는 함수 실행

            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene("Main");

           
        }
    }
    public static string SqlFormat(string sql)
    {
        return string.Format("\"{0}\"", sql);
    }
}
