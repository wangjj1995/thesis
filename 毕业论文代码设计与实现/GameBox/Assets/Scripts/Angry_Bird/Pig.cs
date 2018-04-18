using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour {

    public float maxSpeed = 10;
    public float minSpeed = 5;
    private SpriteRenderer render;
    public Sprite hurt;
    public GameObject boom;
    public GameObject score;

    //设置猪的两个音乐受伤和死亡以及小鸟碰撞的音乐
    public AudioClip hurtClip;
    public AudioClip dead;
    public AudioClip birdCollision;

    public bool isPig = false;

    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
    }

    //碰撞检测  要求两个游戏物体都需要rigidBody 2D和碰撞盒
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //print(collision.relativeVelocity.magnitude);
        //如果小鸟碰撞到其他东西则播放小鸟受伤的声音
        if (collision.gameObject.tag == "Player") {
            AudioPlay(birdCollision);
            collision.transform.GetComponent<Bird>().Hurt();
        }

        //如果两者的相对速度> 最大速度，小猪直接死亡（消失了）
        if (collision.relativeVelocity.magnitude > maxSpeed)//直接死亡
        {
            Dead();
        }
        //如果两者的相对速度介于最大和最小速度之间，小猪受伤  （小猪的图片更换成受伤的图片）
        else if (collision.relativeVelocity.magnitude > minSpeed && collision.relativeVelocity.magnitude < maxSpeed) {
            render.sprite = hurt;
            AudioPlay(hurtClip);
        }
    }

    //触发检测  只要求其中一个游戏物体需要rigidBody 2D和碰撞盒（但碰撞盒的is Trigger属性为true）
    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    //处理猪的死亡后事，猪消失，产生爆炸特效
    public void Dead() {
        if (isPig)
        {
            GameManager._instance.pig.Remove(this);
        }
        Destroy(gameObject);
        Instantiate(boom, transform.position, Quaternion.identity);

        //生成分数,并与1.5秒以后分数消失
        GameObject go = Instantiate(score, transform.position + new Vector3(0,0.5f,0), Quaternion.identity);
        Destroy(go, 1.5f);
        //播放猪死亡的音乐
        AudioPlay(dead);
    }

    //播放音乐的方法
    public void AudioPlay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }

}
