using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelAsync : MonoBehaviour {

    // Use this for initialization
    void Start() {
        //因为不同的手机分辨率不同，暂且写死分辨率，免得在不同的手机显示不一样
        Screen.SetResolution(1920,1080,false);     //三个参数：宽度，高度，是否全屏
        //因为我的游戏资源比较小，加载时间特别短，为了能让玩家看清楚我的加载界面，所以延时2秒加载
        Invoke("Load",1);
    }

    void Load() {
        //这里利用异步加载游戏的方式，一般大型游戏里面资源比较多，加载的时间比较久，
        //利用一步加载的方式分散用户注意力，可以更好的提高用户体验
        SceneManager.LoadSceneAsync(2);
        }
    }

