using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBird : Bird {
    //小绿鸟有回旋特技
    public override void ShowSkill()
    {
        base.ShowSkill();
        Vector3 speed = rg.velocity;
        speed.x *= -1;
        rg.velocity = speed;
    }
}
