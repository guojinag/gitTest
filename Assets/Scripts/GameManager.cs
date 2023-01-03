using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject endUI;
    public Text endMassage;

    public GameObject panalStop;
    public GameObject panalInformation;

    public static GameManager instance;
    private EnemySpawner enemySpawner;


    void Awake()
    {
        instance = this;
        enemySpawner = GetComponent<EnemySpawner>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            panalStop.SetActive(true);
            Time.timeScale = 0; 
        }
        
    }

    public void OnButtonContinue()
    {
        panalStop.SetActive(false);
        Time.timeScale = 1;
    }
    

    public void Win()
    {
        endUI.SetActive(true);
        endMassage.text = "Ó®£¡";
    }
    public void Failed()
    {
        enemySpawner.Stop();
        endUI.SetActive(true);
        endMassage.text = "²Ë";
    }

    public void OnButtonAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OnButtonMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OnButtonInformation()
    {
        panalInformation.SetActive(true);
    }
    public void OnButtonInformationOver()
    {
        panalInformation.SetActive(false);
    }
    public void OnExitGame()
    {
        Application.Quit();
    }
}
