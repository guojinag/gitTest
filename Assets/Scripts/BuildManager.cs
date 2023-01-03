using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    public TurretData laserData;
    public TurretData missileData;
    public TurretData standardData;

    //当前选择炮塔(将要建造)
    public TurretData selectedTurretData;
    //当前选择炮塔（场景中选择（为升级准备））
    private MapCube selectedMapCube;

    public Text moneyText;
    public int money = 1000;
    public Animator moneyAnimator;

    //升级及相关处理
    public GameObject upgradeCanvas;
    public Button buttonUpgrade;

    //动画
    private Animator upgradeCanvasAnimator;



    void Start()
    {
        upgradeCanvasAnimator = upgradeCanvas.GetComponent<Animator>();
    }

    public void ChangeMoney(int charge=0)
    {
        money -= charge;
        moneyText.text = "$" + money;
    }
    void Update()//建造设置
    {
        if (Input.GetMouseButtonDown(0))//点击鼠标
        {
            if (EventSystem.current.IsPointerOverGameObject() == false)//未点击UI
            {
                //炮台建造
                //激光检测方块
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool isCollider= Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("MapCube"));
                if (isCollider)
                {
                    MapCube mapCube = hit.collider.GetComponent<MapCube>();//得到点击的mapCube
                    if (selectedTurretData != null && mapCube.turretGo == null)
                    {
                        //可以创建
                        if (money >= selectedTurretData.cost)
                        {
                            ChangeMoney(selectedTurretData.cost);
                            mapCube.BuildTurret(selectedTurretData);
                        }
                        else
                        {
                            //提示钱不够
                            moneyAnimator.SetTrigger("Flicker");
                        }
                    }
                    else if(mapCube.turretGo != null)
                    {
                        //升级处理
                        //ShowUpgradeUI(mapCube.transform.position,mapCube.isUpgraded);
                        
                        if (mapCube == selectedMapCube && upgradeCanvas.activeInHierarchy)//多次选中同一方块
                        {
                            StartCoroutine(HideUpgradeUI());
                            //selectedTurretGo=null;
                        }
                        else
                        {
                            ShowUpgradeUI(mapCube.transform.position, mapCube.isUpgraded);
                        }
                        selectedMapCube = mapCube;
                    }
                }
            }
        }
    }

    //选择三种炮塔
    public void OnLanserSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = laserData;
        }
    }
    public void OnMissileSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = missileData;
        }
    }
    public void OnStandardSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = standardData;
        }
    }

    void ShowUpgradeUI(Vector3 pos,bool isDisableUpgrade=false)//显示升级ui
    {
        StopCoroutine("HideUpgradeUI");
        upgradeCanvas.SetActive(false);
        upgradeCanvas.SetActive(true);
        upgradeCanvas.transform.position = pos;
        buttonUpgrade.interactable=!isDisableUpgrade;
    }
    IEnumerator HideUpgradeUI()//隐藏升级ui
    {
        upgradeCanvasAnimator.SetTrigger("HideTrigger");
        yield return new WaitForSeconds(0.2f);
        upgradeCanvas.SetActive(false);
    }

    public void OnUpgradeButtonDown()
    {
        if (money >= selectedMapCube.turretData.costUpgraded)
        {
            ChangeMoney(selectedMapCube.turretData.costUpgraded);
            selectedMapCube.UpgradeTurret();
            StartCoroutine(HideUpgradeUI());
        }
        else
        {
            moneyAnimator.SetTrigger("Flicker");
        }
        
    }
    public void OnDestroyButtonDown()
    {
        selectedMapCube.DestroyTurret();
        StartCoroutine(HideUpgradeUI());
    }
    
}
