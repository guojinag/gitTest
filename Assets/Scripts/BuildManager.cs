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

    //��ǰѡ������(��Ҫ����)
    public TurretData selectedTurretData;
    //��ǰѡ��������������ѡ��Ϊ����׼������
    private MapCube selectedMapCube;

    public Text moneyText;
    public int money = 1000;
    public Animator moneyAnimator;

    //��������ش���
    public GameObject upgradeCanvas;
    public Button buttonUpgrade;

    //����
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
    void Update()//��������
    {
        if (Input.GetMouseButtonDown(0))//������
        {
            if (EventSystem.current.IsPointerOverGameObject() == false)//δ���UI
            {
                //��̨����
                //�����ⷽ��
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool isCollider= Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("MapCube"));
                if (isCollider)
                {
                    MapCube mapCube = hit.collider.GetComponent<MapCube>();//�õ������mapCube
                    if (selectedTurretData != null && mapCube.turretGo == null)
                    {
                        //���Դ���
                        if (money >= selectedTurretData.cost)
                        {
                            ChangeMoney(selectedTurretData.cost);
                            mapCube.BuildTurret(selectedTurretData);
                        }
                        else
                        {
                            //��ʾǮ����
                            moneyAnimator.SetTrigger("Flicker");
                        }
                    }
                    else if(mapCube.turretGo != null)
                    {
                        //��������
                        //ShowUpgradeUI(mapCube.transform.position,mapCube.isUpgraded);
                        
                        if (mapCube == selectedMapCube && upgradeCanvas.activeInHierarchy)//���ѡ��ͬһ����
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

    //ѡ����������
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

    void ShowUpgradeUI(Vector3 pos,bool isDisableUpgrade=false)//��ʾ����ui
    {
        StopCoroutine("HideUpgradeUI");
        upgradeCanvas.SetActive(false);
        upgradeCanvas.SetActive(true);
        upgradeCanvas.transform.position = pos;
        buttonUpgrade.interactable=!isDisableUpgrade;
    }
    IEnumerator HideUpgradeUI()//��������ui
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
