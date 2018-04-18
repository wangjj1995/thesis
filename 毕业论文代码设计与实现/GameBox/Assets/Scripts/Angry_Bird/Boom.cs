using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour {
    //当动画播放完，这个游戏物体就要消失
    public void destroying()
    {

        Destroy(gameObject);
    }
}
