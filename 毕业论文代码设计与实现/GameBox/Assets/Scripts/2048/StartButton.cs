using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour,IPointerDownHandler,IPointerUpHandler{

    private Transform ButtonStart;
    private Image ButtonImage;

    //判断用户是否在拖动滚动条的状态
    private bool isPointerDown;
    private Vector3 InitMousePos;


    void Awake() {
        ButtonStart = this.transform.Find("Start").transform;
        ButtonImage=ButtonStart.GetComponent<Image>();
    }
    void Start() {
        ButtonStart.localPosition = new Vector3(-90f, ButtonStart.localPosition.y, ButtonStart.transform.localPosition.z);
        InitMousePos = Vector3.zero; 
    }
    void Update() {
        UpdateButton();
    }

    //根据X值来改变游戏状态
    private void UpdateButton()
    {
        //当前用户正在拖动滚动条
        if (isPointerDown)
        {
           // Debug.LogError("1");
            //判断如果滚动条已经移出范围了
            if (ButtonStart.localPosition.x >90f || ButtonStart.localPosition.x < -90f)
            {
                float newX = (Mathf.Abs(ButtonStart.localPosition.x) / ButtonStart.localPosition.x) *90f;
                if (newX >= 0) {
                   // Debug.LogError("2");
                    ButtonImage.color = new Color(104, 255, 0, 255);
                }
                ButtonStart.localPosition = new Vector3(newX, ButtonStart.localPosition.y, ButtonStart.transform.localPosition.z);
            }
        }
        //用户没有滑动滚动条时
        else
        {
            //得到x的初始位置，即起点-90   中间0    终点90
          //  Debug.LogError("3");
            float x = ButtonStart.localPosition.x;
            //判断y 是否介于中间到末端
            if (x >= 0)
            {
                //控制最终的终点坐标
                ButtonStart.localPosition = new Vector3(90f, ButtonStart.localPosition.y, ButtonStart.transform.localPosition.z);
               // Debug.LogError("4");
                ButtonImage.color = new Color(104, 255, 0, 255);
            }
            //否则  y介于起点至中间段
            else
            {
                
               // Debug.LogError("5");
                ButtonStart.localPosition = new Vector3(-90f, ButtonStart.localPosition.y, ButtonStart.transform.localPosition.z);
            }
            //如果其x的坐标==90f,则跳转到游戏界面
            if (ButtonStart.localPosition.x == 90f) {
                this.GetComponent<ScrollRect>().enabled = false;
                StartCoroutine(WaitAndSkip());
               // Debug.LogError("6");
            }
            
        }
    }
    
    //控制场景等待、跳转
    private IEnumerator WaitAndSkip() {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(6);
    }


    //检测鼠标按下与抬起
    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.LogError("OnPointerDown");
        isPointerDown = true;
    }
    public void OnPointerUp(PointerEventData eventData) {
      //  Debug.LogError("OnPointerUp");
        isPointerDown = false;
    }
}

