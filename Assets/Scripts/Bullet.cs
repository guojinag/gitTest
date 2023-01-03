using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 50;

    public float speed = 200;

    private Transform target;

    public GameObject explosionEffectProfab;

    public GameObject boom;//爆炸
    public bool isBoom = false;

    public void SetTarget(Transform target)//定位敌人
    {
        this.target = target;
    }

    void Update()
    {
        if(target == null)//若目标消失则消失
        {
            Die();
            return;
        }
        transform.LookAt(target.position);//面向敌人
        transform.Translate(Vector3.forward * speed * Time.deltaTime);//向敌人飞去
    }

    void OnTriggerEnter(Collider col)//碰撞敌人并消失
    {
        if (col.tag == "Enemy")
        {
            col.GetComponent<EnemyAction>().TakeDamage(damage);
            Die();
        }
    }

    void Die()//子弹消失
    {
        GameObject effect = GameObject.Instantiate(explosionEffectProfab, transform.position, transform.rotation);
        Destroy(effect, 1);
        if (isBoom)
        {
            GameObject bullet = GameObject.Instantiate(boom, this.transform.position, this.transform.rotation);
        }
        Destroy(this.gameObject);
    }
}
