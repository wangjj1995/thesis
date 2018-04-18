using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 该脚本放在02-Game场景下，用来加载各个关卡的场景（各个关卡的场景做成了Prefabs,放在Resources文件夹下面）
/// </summary>
public class Loadlevel : MonoBehaviour {

    private void Awake()
    {
        Instantiate(Resources.Load(PlayerPrefs.GetString("nowLevel"))); 
    }
}
