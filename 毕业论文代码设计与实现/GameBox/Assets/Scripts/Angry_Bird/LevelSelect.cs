using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// 该脚本用于控制每个大场景关卡下，有无数小关卡，当第一个小关卡的星星数大于一个，
/// 才能解锁下一个小关卡，以此类推
/// 算法思想，通过父物体遍历下面的子关卡
/// </summary>
public class LevelSelect : MonoBehaviour
{
    //用于判断是否解锁子关卡
    public bool isSelect = false;
    public Sprite levelBG;
    private Image image;

    //装星星的数组
    public GameObject[] stars;

    private void Awake()
    {
        image = GetComponent<Image>();
    }


    private void Start()
    {
        if (transform.parent.GetChild(0).name == gameObject.name)
        {
            isSelect = true;
        }
        else
        {// 判断当前关卡是否可以选择
            //获得前一个关卡的名称，并把字符串转为数值
            int beforeNum = int.Parse(gameObject.name) - 1;
            //假如上一个关卡获得的星星数量大于1颗，解锁下一个关卡
            if (PlayerPrefs.GetInt("level" + beforeNum.ToString()) > 0)
            {
                isSelect = true;
            }
        }


        if (isSelect)
        {
            image.overrideSprite = levelBG;
            transform.Find("num").gameObject.SetActive(true);

            //获取当前关卡对应的名字，然后获取对应的星星的个数
            int count = PlayerPrefs.GetInt("level" + gameObject.name);//获取现在关卡对应的名字，然后获得对应的星星个数
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    stars[i].SetActive(true);
                }
            }
        }
    }

    //注册鼠标点击事件
    public void Selected()
    {
        if (isSelect)
        {
            ////存储当前究竟是在那一关
            PlayerPrefs.SetString("nowLevel", "level" + gameObject.name);
            //场景跳转,由01-level 跳转到02-Game
            SceneManager.LoadScene(3);
        }
    }

}
