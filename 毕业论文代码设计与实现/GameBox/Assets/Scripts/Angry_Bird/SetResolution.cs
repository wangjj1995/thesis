using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 该脚本用来控制（Unity 3D安卓版）对于不同的分辨率，我们只需要设置一个唯一的屏幕显示区域比
/// 即高宽比，这样显示区域就不会随分辨率的改变而拉升了
/// </summary>
public class SetResolution : MonoBehaviour
{

    public Camera mainCamera;
    // Use this for initialization
    void Start()
    {

        Screen.SetResolution(1280, 800, true, 60);
        float screenAspect = 1280 / 720; // 现在android手机的主流分辨。
       // mainCamera.aspect --->  摄像机的长宽比（宽度除以高度）
        mainCamera.aspect = 1.78f;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
