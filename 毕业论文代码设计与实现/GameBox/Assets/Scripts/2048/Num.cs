using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Num : MonoBehaviour {

    //NumIndex 用来控制新产生的Num图片上面具体的数值
    public int NumIndex=2;
    //4*4宫格里面的坐标从（0，0）--（3，3）
    public int InitX,InitY;
    public Sprite[] ImageSouce;
    private Image NumImage;
    private Transform Mark;
    //确定是否要销毁Num图片，比如合并以后删除多余的图片
    public bool isDestory=false;
    //Num移动所需要的时间
    public float moveTime = 0.4f;

    void Awake() {
        NumImage = this.GetComponent<Image>();       
    }
    void Start() {
        Mark = this.transform.parent.Find("Mark").transform;
        CreateNum();
        InitPos();
    }
    void Update() {
        UpdateImage();
    }

    /// <summary>
    /// iTween这个类库，它主要的功能就是处理模型从起始点到结束点之间运动的轨迹。（移动，旋转，音频，路径，摄像机等）
    /// 它是一个开源的项目并且完全免费，它们的官网在这里 http://itween.pixelplacement.com/index.php 打开网之后点击右上角Get iTween图标即可，
    /// 或者在AssetStores商店中直接下载。 
    /// 我把iTween的源码仔细读了一遍，我感觉与其说它是处理动画的类，不如说它是处理数学的类
    /// </summary>
    #region 生成动画-CreateNum
    private void CreateNum()
    {
        //ScaleTo:改变游戏对象的比例大小到提供的值。
        iTween.ScaleTo(this.gameObject, new Vector3(1, 1, 1), 0.2f);
    }
    #endregion

    #region 初始化位置-InitPos
    private void InitPos() {
        this.transform.localPosition = new Vector3(Mark.localPosition.x +143 * InitX, Mark.localPosition.y - 123 * InitY, Mark.localPosition.z);
    }
    # endregion

    #region 移动位置-UpdatePos(int x,int y)
    public void UpdatePos(int x,int y)
    {
        //通过itween进行移动，但是由于itween只能通过position来移动，会出现适配问题，故尝试编写运动代码
        // Hashtable args  是以键值对儿的形式保存iTween所用到的参数
        Hashtable args = new Hashtable();
        args.Add("time", moveTime);
        args.Add("islocal", true);
        //表示示移动的位置。
        args.Add("position", new Vector3(Mark.localPosition.x + 143 * x, Mark.localPosition.y - 123 * y, Mark.localPosition.z));
        //处理移动过程中的事件。例如：开始发生移动：onstart  移动结束：oncomplete  移动中:onupdate
        //移动结束时调用 SetUpXAndY方法
        args.Add("oncomplete", "SetUpXAndY");

        //以上只是准备工作，下面最终让该对象开始移动
        iTween.MoveTo(this.gameObject, args);

        InitX = x; InitY = y;
    }
    #endregion

    #region 移动完成后设置属性/删除-SetUpXAndY  合并以后删除多余的Num
    private void SetUpXAndY() {
        //print("运动完成");
        //InitPos();
        //如果要合并删除，就进行删除
        if (isDestory) {
            Destroy(this.gameObject);
        }
    }
    #endregion

    #region 根据NumIndex改变NumImage-UpdateImage
    private void UpdateImage()
    {
        NumImage.sprite = ImageSouce[CorrespondImageAndIndex(NumIndex)];
    }
    #endregion

    #region 将NumIndex与ImageSouce一一对应-CorrespondImageAndIndex(int NumIndex )
    private int CorrespondImageAndIndex(int NumIndex ){
        switch (NumIndex) { 
            case 2:
                return 0;
            case 4:
                return 1;
            case 8:
                return 2;
            case 16:
                return 3;
            case 32:
                return 4;
            case 64:
                return 5;
            case 128:
                return 6;
            case 256:
                return 7;
            case 512:
                return 8;
            case 1024:
                return 9;
            case 2048:
                return 10;
            default:
                return 0;
        }
    }
    #endregion
}
