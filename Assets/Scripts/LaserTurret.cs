using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurret : MonoBehaviour
{
    public float attackRateTime;//攻击频率
    private float timer = 1;

    public bool useLaser = false;
    public float laserDamageRate = 50;

    public GameObject bulletPrefab;//子弹
    public Transform firePosition;//子弹召唤点
    public Transform head;
    public List<GameObject> enemys = new List<GameObject>();
    void OnTriggerEnter(Collider col)//将敌人收入进集合
    {
        if (col.tag == "Enemy")
        {
            enemys.Add(col.gameObject);
        }
    }
    void OnTriggerExit(Collider col)//敌人离开时踢出集合
    {
        if (col.tag == "Enemy")
        {
            enemys.Remove(col.gameObject);
        }
    }
    void UpdateEnemys()//刷新集合
    {
        enemys.RemoveAll(item => item == null);
    }
    void Update()//普通攻击（无周期）
    {
        timer += Time.deltaTime;//攻击间隔
        if (enemys.Count > 0 && timer > attackRateTime)
        {
            timer = 0;
            
        }
        if (enemys.Count > 0 && enemys[0] != null)
        {
            Vector3 targetPosition = enemys[0].transform.position;
            targetPosition.y = head.position.y;
            head.LookAt(targetPosition);
        }
    }
}
