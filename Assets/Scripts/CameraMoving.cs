using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMoving : MonoBehaviour
{
    public Transform place1;
    public float speed = 100;
    public Transform cameraPosition;
    private Transform[] positions;//路径点
    private int index = 0;
    private bool isMove = false;
    
    //private bool isBack = false;


    void Start()//初始目标点
    {
        positions = WayPoints.positions;
        //place1 = this.transform.position;
    }

    void Update()
    {
        //cameraPosition.position = place1.position;
        /*if (isMove)
        {
            if (index > positions.Length - 1) return;
            transform.Translate((positions[index].position - cameraPosition.position).normalized * Time.deltaTime * speed);

            if (Vector3.Distance(positions[index].position, transform.position) < 0.2f)
            {
                index++;
            }
        }*/
        
        
    }
    public void Moving()
    {
        isMove = true;
        this.transform.position = place1.position;
    }
    public void Back()
    {
        isMove = false;
        SceneManager.LoadScene(1);
    }

    
}
