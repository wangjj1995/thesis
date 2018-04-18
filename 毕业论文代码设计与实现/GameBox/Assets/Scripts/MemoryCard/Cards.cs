using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cards : MonoBehaviour
{

   // get和set是对象属性特有的两个方法，属性是对字段的封装，这里是为了程序数据的安全性考虑的
    public int ID
    {
        get { return id; }
    }

    //卡牌需有唯一标识id
    //利用同种卡牌id相同，不同卡牌id不同的原理对卡牌配对是否成功做出判断
    private int id;
    //卡牌会有三种显示情况：未被翻开状态，被翻开状态，配对成功状态。
    //这三种状态用不同的图片来区分，均为Sprite类型；
    private Sprite frontImg;
    private Sprite backImg;
    private Sprite successImg;

    //卡牌有不同状态图片，都需要指定给卡牌的Image组件才能显示
    //所以我们需要声明Image类型的字段来获取Image组件对象；
    private Image showImg;

    //根据游戏机制，卡牌存在不能点击的时刻
    //此时我们需要获取到卡牌Button组件中的属性来设置卡牌能否被点击
    public Button cardBtn;

    public void InitCard(int ID, Sprite FrontImg, Sprite BackImg,Sprite SuccessImg)
    {
        this.id = ID;
        this.frontImg = FrontImg;
        this.backImg = BackImg;
        this.successImg = SuccessImg;

        showImg = GetComponent<Image>();
        //初始化时，令牌均展示backImg
        showImg.sprite = this.backImg;
        cardBtn = GetComponent<Button>();
    }

    //一张牌自身有三种操作
    public void SetTurnCard()
    {
        showImg.sprite = frontImg;
        cardBtn.interactable = false;
    }

    public void SetSuccess()
    {
        showImg.sprite = successImg;
       // cardBtn.interactable = false;
    }

    public void SetRecover()
    {
        showImg.sprite = backImg;
        cardBtn.interactable = true;
    }
}
