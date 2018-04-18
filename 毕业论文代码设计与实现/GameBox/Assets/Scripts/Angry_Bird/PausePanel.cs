using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    //获取面板自身的动画状态集
    private Animator anim;
    //当点击了暂停按钮后，游戏暂停，暂停按钮也要隐藏
    public GameObject button;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }


    public void Retry() {

        Time.timeScale = 1;    //在继续状态
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
    /// <summary>
    /// 点击了pause按钮
    /// </summary>
    public void Pause()
    {
        //1、播放pause动画  
        anim.SetBool("isPause", true);
        button.SetActive(false);

        if (GameManager._instance.birds.Count > 0)
        {
            if (GameManager._instance.birds[0].isReleased == false)  //小鸟没有飞出，点击暂停按钮小鸟不可以飞出
            {
                GameManager._instance.birds[0].canMove = false;
            }
        }
    }

 
    /// <summary>
    /// 点击了继续按钮
    /// </summary>
    public void Resume() {
        //1、播放resume动画
        Time.timeScale = 1;
        anim.SetBool("isPause", false);

        if (GameManager._instance.birds.Count > 0)
        {
            if (GameManager._instance.birds[0].isReleased == false)  //小鸟没有飞出，点击暂停按钮小鸟不可以飞出
            {
                GameManager._instance.birds[0].canMove = true;
            }
        }
    }

    public void Home()
    {
        Time.timeScale = 1;    //在继续状态
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }


    /// <summary>
    /// pause动画播放完调用
    /// </summary>
    public void PauseAnimEnd() {
        Time.timeScale = 0;   //暂停
    }
    /// <summary>
    /// resume动画播放完调用
    /// </summary>
    public void ResumeAnimEnd() {
        button.SetActive(true);
    }
   
}
