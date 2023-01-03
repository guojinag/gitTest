using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapCube : MonoBehaviour
{
    public GameObject turretGo;//��ǰ��������
    public TurretData turretData;
    public GameObject buildEffect;//����������Ч

    private new Renderer renderer;//����
    private Color color;

    public bool isUpgraded = false;

    void Start()//��ȡ�ذ����
    {
        renderer = GetComponent<Renderer>();
        color = GetComponent<MeshRenderer>().material.color;
    }


    public void BuildTurret(TurretData turretData)//��������ʱ������Ч
    {
        this.turretData = turretData;
        isUpgraded = false;
        turretGo = GameObject.Instantiate(turretData.turretPrefab, transform.position, Quaternion.identity);
        GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1);

    }
    public void UpgradeTurret()//��������
    {
        if (isUpgraded == true) return;

        Destroy(turretGo);
        isUpgraded = true;
        turretGo= GameObject.Instantiate(turretData.turretUpgradedPrefab, transform.position, Quaternion.identity);
        GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1);
    }
    public void DestroyTurret()//�������
    {
        Destroy(turretGo);
        isUpgraded= false;
        turretGo = null;
        turretData = null;
        GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1);

    }

    void OnMouseEnter()//��껮��ʱǿ��
    {
        if(turretGo == null && EventSystem.current.IsPointerOverGameObject() == false)
        {
            renderer.material.color = Color.red;
        }
    }
    void OnMouseExit()//����뿪ʱ��ԭ
    {
        renderer.material.color = color;
    }
}
