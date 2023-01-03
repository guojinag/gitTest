using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    public int damage = 50;
    

    void OnTriggerEnter(Collider col)//��ײ���˲���ʧ
    {
        if (col.tag == "Enemy")
        {
            col.GetComponent<EnemyAction>().TakeDamage(damage);
            Die();
        }
    }
    void Die()//��ʧ
    {
        
        Destroy(this.gameObject);
    }
}
