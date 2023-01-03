using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameMenu : MonoBehaviour
{
    public void OnStartGame1()
    {
        SceneManager.LoadScene(1);
    }
    public void OnStartGame2()
    {
        SceneManager.LoadScene(2);
    }
    public void OnStartGame3()
    {
        SceneManager.LoadScene(3);
    }
    
}
