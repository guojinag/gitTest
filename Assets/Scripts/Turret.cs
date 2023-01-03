using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public List<GameObject> enemys = new List<GameObject>();
    void OnTriggerEnter(Collider col)//���������������
    {
        if (col.tag == "Enemy")
        {
            enemys.Add(col.gameObject);
        }
    }
    void OnTriggerExit(Collider col)//�����뿪ʱ�߳�����
    {
        if (col.tag == "Enemy")
        {
            enemys.Remove(col.gameObject);
        }
    }

    public float attackRateTime;//����Ƶ��
    private float timer = 1;

    public bool useLaser = false;
    public float laserDamageRate=50;
    //public float speedDown=0.8f;
    public LineRenderer laserRenderer;
    public GameObject laserEffect;

    public GameObject bulletPrefab;//�ӵ�
    public Transform firePosition;//�ӵ��ٻ���
    public Transform head;

    void Update()//��ͨ���������ڣ�
    {
        if (enemys.Count > 0 && enemys[0] != null)
        {
            Vector3 targetPosition = enemys[0].transform.position;
            targetPosition.y = head.position.y;
            head.LookAt(targetPosition);
        }

        if (useLaser == false)
        {
            timer += Time.deltaTime;//�������
            if (enemys.Count > 0 && timer > attackRateTime)
            {
                timer = 0;
                Attack();
            }
        }
        else if(enemys.Count > 0)
        {
            if(laserRenderer.enabled== false)
            {
                laserRenderer.enabled = true;
            }
            laserEffect.SetActive(true);
            if (enemys[0] == null)
            {
                UpdateEnemys();
            }
            if (enemys.Count > 0)
            {
                laserRenderer.SetPositions(new Vector3[] { firePosition.position, enemys[0].transform.position });
                enemys[0].GetComponent<EnemyAction>().TakeDamage(laserDamageRate * Time.deltaTime);
                enemys[0].GetComponent<EnemyAction>().SpeedChange();
                //speedDown = 0;
                laserEffect.transform.position=enemys[0].transform.position;

            }
        }
        else
        {
            laserEffect.SetActive(false);
            laserRenderer.enabled = false;
        }
        
        
    }

    void Attack()//�ٻ��ӵ�������
    {
        if (enemys[0] == null)
        {
            UpdateEnemys();
        }
        if (enemys.Count > 0)
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
            bullet.GetComponent<Bullet>().SetTarget(enemys[0].transform);
        }
        else
        {
            timer=attackRateTime;

        }
        

    }

    void UpdateEnemys()//ˢ�µ����б�
    {
        //enemys.RemoveAll(null);���󷽷�
        List<int> emptyIndex = new List<int>();
        for (int index = 0; index < enemys.Count; index++)
        {
            if (enemys[index] == null)
            {
                emptyIndex.Add(index);
            }
        }

        for (int i = 0; i < emptyIndex.Count; i++)
        {
            enemys.RemoveAt(emptyIndex[i]-i);
        }//��Ƶ������淽��
        //enemys.RemoveAll(item=>item==null);//��Ļ��ĳ��򵥷���
    }
}
