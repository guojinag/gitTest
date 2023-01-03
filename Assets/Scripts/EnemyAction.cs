using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAction : MonoBehaviour
{
    public float speed = 10;
    private float downSpeed;
    private bool isSpeedDown=false;
    private Transform[] positions;//路径点

    private int index = 0;
    public float hp = 100;
    private float totalHp;

    public GameObject explosionEffectProfab;//死亡特效
    private Slider hpSlider;//血条

    public GameObject moneyManager;
    public int money;

    void Start()//初始目标点
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
    void Move()//移动
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

    void ReachDestination()//到达终点
    {

        GameManager.instance.Failed();
        GameObject.Destroy(this.gameObject);
    }

    void OnDestroy()//在孵化器中消除
    {
        EnemySpawner.countEnemyAlive--;
    }

    public void TakeDamage(float damage)//受伤
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
    void Die()//死亡
    {
        GameObject effect = GameObject.Instantiate(explosionEffectProfab, transform.position, transform.rotation);
        Destroy(effect, 1);
        GiveMoney();
        Destroy(this.gameObject);
    }
}
