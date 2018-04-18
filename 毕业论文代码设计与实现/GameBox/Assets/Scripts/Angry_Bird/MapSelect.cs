using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 本脚本用于对关卡的设置的选择
/// </summary>
public class MapSelect : MonoBehaviour {
    //用于存储每个关卡可以解锁星星的数量
    public int starsNum = 0;
    //用于判断该关卡是否可以解锁
    private bool isSelect = false;
    //每个关卡下面的的两个图片
    public GameObject locks;
    public GameObject stars;

    // 点击每个map关卡，进入关卡详情（小关卡的选择）小关卡放在关卡Panel中
    public GameObject panel;
    public GameObject map;
    //用来显示每个关卡  获得的星星 / 总共需要的星星
    public Text starsText;
    //每个小关卡名目前共制作了三个map,9关
    public int startNum = 1;
    public int endNum = 3;


    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        //PlayerPrefs 是unity自带的存储数据的方法，是用键值对来存储，一个键对应一个值，利用键来找值
        if (PlayerPrefs.GetInt("totalNum", 0) >= starsNum) {
            isSelect = true;
        }

        if (isSelect) {
            locks.SetActive(false);
            stars.SetActive(true);

            //TODo:text显示
            int counts = 0; 
            for (int i = startNum; i <= endNum; i++) {
                counts += PlayerPrefs.GetInt("level"+i.ToString(),0);
            }
            starsText.text = counts.ToString() + "/60";
        }
    }
    /// <summary>
    /// 鼠标点击
    /// </summary>
    public void Selected()
    {
        if (isSelect) {
            panel.SetActive(true);
            map.SetActive(false);
        }
    }

    public void panelSelect() {
        panel.SetActive(false);
        map.SetActive(true);
    }
}
