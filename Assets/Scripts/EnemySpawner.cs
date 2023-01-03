using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static int countEnemyAlive = 0;
    public Wave[] waves;
    public Transform START;
    public float waveRate;
    public GameObject wayPoint;
    

    void Start()
    {
        StartCoroutine("SpawnEnemy");
    }
    public void Stop()
    {
        StopCoroutine("SpawnEnemy");
    }
    

    IEnumerator SpawnEnemy()//ˢ�µ���
    {
        if(START != null) 
        {
            foreach (Wave wave in waves)
            {
                for (int i = 0; i < wave.count; i++)
                {
                    GameObject.Instantiate(wave.enemyPrefab, START.position, Quaternion.identity);
                    countEnemyAlive++;
                    if (i != wave.count - 1)
                        yield return new WaitForSeconds(wave.rate);
                }
                while (countEnemyAlive > 0)//���ϵ�������ǰ��ˢ����һ��
                {
                    yield return 0;
                }
                yield return new WaitForSeconds(waveRate);
            }
            while (countEnemyAlive > 0)
            {
                yield return 0;
            }
            GameManager.instance.Win();
        }
        
    }
    
}
