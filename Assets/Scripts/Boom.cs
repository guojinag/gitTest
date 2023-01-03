using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    public int damage = 50;
    

    void OnTriggerEnter(Collider col)//碰撞敌人并消失
    {
        if (col.tag == "Enemy")
        {
            col.GetComponent<EnemyAction>().TakeDamage(damage);
            Die();
        }
    }
    void Die()//消失
    {
        
        Destroy(this.gameObject);
    }
}
