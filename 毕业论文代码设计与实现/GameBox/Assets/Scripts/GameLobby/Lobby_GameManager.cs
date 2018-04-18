using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lobby_GameManager : MonoBehaviour
{
    //获得界面中的三个GameObject,设置后面的玩法规则和关于界面
    public GameObject Bg;
    public GameObject GameRule;
    public GameObject About;
    public GameObject xialacaidan;


    public void Enter_MemoryCard()
    {
        SceneManager.LoadSceneAsync(4);
    }

    public void Enter_2048()
    {
          SceneManager.LoadSceneAsync(5);
    }

    public void Enter_AngryBird()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void Btn_Quit()
    {
#if UNITY_EDTTOR
        UnityEditor.EditorApplication.isPlaying=false;
#else
        Application.Quit();
#endif
    }

    //主界面
    public void Backmain()
    {
        Bg.SetActive(true);
        GameRule.SetActive(false);
        About.SetActive(false);
        xialacaidan.SetActive(false);
    }

    //游戏规则
    public void SetGameRule()
    {
        //Bg.SetActive(false);
        GameRule.SetActive(true);
        About.SetActive(false);
    }

    //关于
    public void SetAbout()
    {
       // Bg.SetActive(false);
        GameRule.SetActive(false);
        About.SetActive(true);
    }

    //展示下拉菜单
    public void Xialacaidan()
    {
        xialacaidan.SetActive(true);
        GameRule.SetActive(false);
        About.SetActive(false);
    }


}
