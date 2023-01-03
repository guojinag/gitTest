using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurret : MonoBehaviour
{
    public float attackRateTime;//����Ƶ��
    private float timer = 1;

    public bool useLaser = false;
    public float laserDamageRate = 50;

    public GameObject bulletPrefab;//�ӵ�
    public Transform firePosition;//�ӵ��ٻ���
    public Transform head;
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
    void UpdateEnemys()//ˢ�¼���
    {
        enemys.RemoveAll(item => item == null);
    }
    void Update()//��ͨ�����������ڣ�
    {
        timer += Time.deltaTime;//�������
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
