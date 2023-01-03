using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 50;

    public float speed = 200;

    private Transform target;

    public GameObject explosionEffectProfab;

    public GameObject boom;//��ը
    public bool isBoom = false;

    public void SetTarget(Transform target)//��λ����
    {
        this.target = target;
    }

    void Update()
    {
        if(target == null)//��Ŀ����ʧ����ʧ
        {
            Die();
            return;
        }
        transform.LookAt(target.position);//�������
        transform.Translate(Vector3.forward * speed * Time.deltaTime);//����˷�ȥ
    }

    void OnTriggerEnter(Collider col)//��ײ���˲���ʧ
    {
        if (col.tag == "Enemy")
        {
            col.GetComponent<EnemyAction>().TakeDamage(damage);
            Die();
        }
    }

    void Die()//�ӵ���ʧ
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
