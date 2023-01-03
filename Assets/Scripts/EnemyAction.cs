using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAction : MonoBehaviour
{
    public float speed = 10;
    private float downSpeed;
    private bool isSpeedDown=false;
    private Transform[] positions;//·����

    private int index = 0;
    public float hp = 100;
    private float totalHp;

    public GameObject explosionEffectProfab;//������Ч
    private Slider hpSlider;//Ѫ��

    public GameObject moneyManager;
    public int money;

    void Start()//��ʼĿ���
    {
        downSpeed = speed * 0.5f;
        moneyManager = GameObject.Find("GameManager");
        positions = WayPoints.positions;
        totalHp = hp;
        hpSlider = GetComponentInChildren<Slider>();
    }

    void Update()
    {
        if (isSpeedDown)
        {
            speed = downSpeed;
        }
        Move();
    }
    void Move()//�ƶ�
    {
        if (index > positions.Length - 1) return;
        transform.Translate((positions[index].position - transform.position).normalized * Time.deltaTime * speed);
        if (Vector3.Distance(positions[index].position, transform.position) < 0.2f)
        {
            index++;
        }
        if(index > positions.Length - 1)
        {
            ReachDestination();
        }
    }

    void ReachDestination()//�����յ�
    {

        GameManager.instance.Failed();
        GameObject.Destroy(this.gameObject);
    }

    void OnDestroy()//�ڷ�����������
    {
        EnemySpawner.countEnemyAlive--;
    }

    public void TakeDamage(float damage)//����
    {
        if (hp <= 0) return;
        hp -= damage;
        hpSlider.value = hp/totalHp;
        if(hp <= 0)
        {
            Die();
        }
        
    }

    public void SpeedChange()
    {
        isSpeedDown = true;
    }
    public void GiveMoney()
    {
        moneyManager.GetComponent<BuildManager>().ChangeMoney(-money);
    }
    void Die()//����
    {
        GameObject effect = GameObject.Instantiate(explosionEffectProfab, transform.position, transform.rotation);
        Destroy(effect, 1);
        GiveMoney();
        Destroy(this.gameObject);
    }
}
