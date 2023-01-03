using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapCube : MonoBehaviour
{
    public GameObject turretGo;//当前炮塔有无
    public TurretData turretData;
    public GameObject buildEffect;//放置炮塔特效

    private new Renderer renderer;//材质
    private Color color;

    public bool isUpgraded = false;

    void Start()//获取地板材质
    {
        renderer = GetComponent<Renderer>();
        color = GetComponent<MeshRenderer>().material.color;
    }


    public void BuildTurret(TurretData turretData)//建造炮塔时播放特效
    {
        this.turretData = turretData;
        isUpgraded = false;
        turretGo = GameObject.Instantiate(turretData.turretPrefab, transform.position, Quaternion.identity);
        GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1);

    }
    public void UpgradeTurret()//升级炮塔
    {
        if (isUpgraded == true) return;

        Destroy(turretGo);
        isUpgraded = true;
        turretGo= GameObject.Instantiate(turretData.turretUpgradedPrefab, transform.position, Quaternion.identity);
        GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1);
    }
    public void DestroyTurret()//拆除炮塔
    {
        Destroy(turretGo);
        isUpgraded= false;
        turretGo = null;
        turretData = null;
        GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1);

    }

    void OnMouseEnter()//鼠标划过时强调
    {
        if(turretGo == null && EventSystem.current.IsPointerOverGameObject() == false)
        {
            renderer.material.color = Color.red;
        }
    }
    void OnMouseExit()//鼠标离开时复原
    {
        renderer.material.color = color;
    }
}
