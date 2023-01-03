using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 保存每波敌人生成属性
[System.Serializable]
public class Wave
{
    public GameObject enemyPrefab;
    public int count;
    public float rate;
}
