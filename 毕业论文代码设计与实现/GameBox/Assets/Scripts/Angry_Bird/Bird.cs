using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 这个脚本用来控制小鸟的移动，即用鼠标拖拽小鸟发射的距离
/// </summary>
public class Bird : MonoBehaviour {
    //检测鼠标舒服按下
    private bool isClick = false;

    //最远距离3米
    public float maxDis = 3;
    [HideInInspector]   //表示sp 变量虽然是public类型，但是在面板中不可见
    public SpringJoint2D sp;
    protected Rigidbody2D rg;   //便于子类可以调用

    //划线做弹弓
    public LineRenderer right;
    public Transform rightPos;
    public LineRenderer left;
    public Transform leftPos;

    //小鸟的特效
    public GameObject boom;

    //小鸟的拖尾特效
    protected TestMyTrail myTrail;
    //控制小鸟在飞出去以后便不能移动了
    [HideInInspector]
    public bool canMove = false;
    //设定相机跟随小鸟移动的平滑度的值
    public float smooth = 3;

    //设置小鸟的音效
    public AudioClip select;
    public AudioClip fly;
    //用来控制小黄鸟在飞出之后，碰撞之前，假如点击鼠标左键，可以加速,调用飞出函数以后变为true
    private bool isFly = false;
    [HideInInspector]
    public bool isReleased = false;  //是否释放了小鸟

    //受伤以后的小鸟
    public Sprite hurt;
    protected SpriteRenderer render;

    private void Awake()
    {
        sp = GetComponent<SpringJoint2D>();
        rg = GetComponent<Rigidbody2D>();
        myTrail = GetComponent<TestMyTrail>();
        render = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()//鼠标按下
    {
        if (canMove)
        {
            //当鼠标按下一下，播放小鸟被选中的音乐
            AudioPlay(select);
            //检测鼠标是否被按下
            isClick = true;
            //开启动力学，不受物理控制，(但是飞出的角度和力度都是可以控制的)否则小鸟飞出时的感觉有种横冲直撞的感觉
            //(但是要稍微延迟一下，如果计算的时间太短了，小鸟飞不出去)
            rg.isKinematic = true;
        }
    }


    private void OnMouseUp() //鼠标抬起
    {
        if (canMove)
        {
            isClick = false;
            rg.isKinematic = false;
            //延时的时间不能太长，否则小鸟就会被弹回来
            Invoke("Fly", 0.1f);
            //禁用划线组件
            right.enabled = false;
            left.enabled = false;
            canMove = false;
        }

    }


    private void Update()
    {
        //判断是否点击到了UI
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (isClick)
        { //如果鼠标按下，就让小鸟跟随鼠标移动
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //transform.position += new Vector3(0, 0, 10);
            transform.position += new Vector3(0,0,-Camera.main.transform.position.z);
           
            // Vector3.Distance()  用来计算两个向量之间的位移（距离）
            //if  用来对位置限定
            if (Vector3.Distance(transform.position, rightPos.position) > maxDis)
            { //进行位置限定
                //向量单位化
                Vector3 pos = (transform.position - rightPos.position).normalized;
                //设置向量的最大长度
                pos *= maxDis;
                transform.position = pos + rightPos.position;
            }
            //划线
            Line();
        }


        //相机跟随小鸟平滑移动
        float posX = transform.position.x;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position,new Vector3(Mathf.Clamp(posX,0,15),Camera.main.transform.position.y, 
            Camera.main.transform.position.z),smooth*Time.deltaTime);


        if (isFly) {
            if (Input.GetMouseButtonDown(0)) {
                //调用炫技函数
                ShowSkill();
            }
        }
    }

    void Fly()
    {
        isReleased = true;
        isFly = true;
        //当小鸟飞行时，播放小鸟飞行的音乐
        AudioPlay(fly);
        myTrail.StartTrails();
        //当鼠标抬起时，小鸟就要飞出
        sp.enabled = false;
        Invoke("Next", 5);
    }

    /// <summary>
    /// 划线做弹弓线操作
    /// </summary>
    void Line()
    {
        //启用划线
        right.enabled = true;
        left.enabled = true;

        //画右边树枝和小鸟之间的线
        right.SetPosition(0, rightPos.position);
        right.SetPosition(1, transform.position);

        //画左边树枝和小鸟之间的线
        left.SetPosition(0, leftPos.position);
        left.SetPosition(1, transform.position);
    }

    /// <summary>
    /// 处理下一只小鸟的飞出（上一只小鸟飞出后就要消失）
    /// </summary>
    /// 
    protected virtual void Next()
    {
        GameManager._instance.birds.Remove(this);
        Destroy(gameObject);
        Instantiate(boom, transform.position, Quaternion.identity);
        GameManager._instance.NextBird();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //小黄鸟的加速炫技必须在碰撞物体之前
        isFly = false;
        myTrail.ClearTrails();
    }

    //播放音乐的方法
    public void AudioPlay(AudioClip clip) {
        AudioSource.PlayClipAtPoint(clip,transform.position);
    }


    /// <summary>
    /// 小黄鸟在飞出之后碰撞之前可以加速的炫技操作
    /// </summary>
    public virtual void ShowSkill() {
        isFly = false;
    }

    //小鸟碰撞到木块或小猪就受伤了
    public void Hurt() {
        
        render.sprite = hurt;
    }
}
