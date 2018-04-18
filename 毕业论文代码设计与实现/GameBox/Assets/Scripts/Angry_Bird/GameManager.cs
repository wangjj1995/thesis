using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public List<Bird> birds;
    public List<Pig> pig;
    public static GameManager _instance;
    private Vector3 originPos; //初始化的位置  第一只弹弓上的小鸟的的位置
    //win和lose 两个面板
    public GameObject win;
    public GameObject lose;
    //储存一个关卡结束以后，界面所展示的星星
    public GameObject[] stars;
    //用于存储各个关卡的星星的数量
    private int starsNum = 0;
    //目前所打算设计的关卡总数量
    private int totalNum = 9;

    private void Awake()
    {
        _instance = this;
        if(birds.Count > 0) {
            originPos = birds[0].transform.position;
        }
    }

    private void Start()
    {
        Initialized();
    }

    /// <summary>
    /// 初始化小鸟
    /// </summary>
    private void Initialized()
    {
        //处理多只小鸟时，当第一只小鸟（当前可用），否则让SpringJoint2D和brids脚本不可用
        for (int i = 0; i < birds.Count; i++)
        {
            if (i == 0) //第一只小鸟
            {
                birds[i].transform.position = originPos;
                birds[i].enabled = true;
                birds[i].sp.enabled = true;
                birds[i].canMove = true;

            }
            else
            {
                birds[i].enabled = false;
                birds[i].sp.enabled = false;
                birds[i].canMove = false;
            }
        }
    }

    /// <summary>
    /// 判定游戏逻辑
    /// </summary>
   public  void NextBird()
    {
        if(pig.Count > 0)
        {
            if (birds.Count > 0)
            {
                //下一只飞吧
                Initialized();
            }
            else
            {
                //输了
                lose.SetActive(true);
            }
        }
        else
        {
            //赢了
            win.SetActive(true);
        }

    }

    /// <summary>
    /// 当一关赢了以后，在界面上一颗一颗的展示星星
    /// </summary>
    public void ShowStars() {
        StartCoroutine("show");
    }

    //协程  使星星能够每隔0.3S，依次显示
    //startsNum 表示关卡中星星的数量
    IEnumerator show() {
        for (; starsNum < birds.Count + 1; starsNum++)
        {
            if (starsNum >= stars.Length) {
                break;
            }
            yield return new WaitForSeconds(0.2f);
            stars[starsNum].SetActive(true);
        }

      //  print(starsNum);
    }

    public void Replay() {
        SaveData();
        SceneManager.LoadScene(3);
    }

    public void Home() {
        SaveData();
        SceneManager.LoadScene(2);
    }

    //（1）每一关完成之后的星星数量(int),利用unity自带的PlayerPrefs.SetInt("level+num",星星数)，并且涉及到更新
    //涉及到下一关是否可以开启，以及level界面星星个数的显示

    //用于存储星星数量的方法
    public void SaveData() {
        //如果重新玩的关卡的星星的数量大于之前获得的星星的数量记录，则该关卡的星星个数更新，
        //否则保持之前的最高纪录，不更新
        if (starsNum > PlayerPrefs.GetInt(PlayerPrefs.GetString("nowLevel"))){
            PlayerPrefs.SetInt(PlayerPrefs.GetString("nowLevel"), starsNum);
        }
        //（2)存储所有的星星个数  totalNum 遍历所有的星星数，非0的关卡星星数，然后相加
        int sum = 0;        
        for (int i = 1; i <= totalNum; i++)
        {   //从第一关开始遍历
            sum += PlayerPrefs.GetInt("level" + i.ToString());
        }
       

        PlayerPrefs.SetInt("totalNum",sum);
        print(PlayerPrefs.GetInt("totalNum"));
    }
}


