using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comove : MonoBehaviour
{
    public GameObject controller;

    GameObject reference = null;
    //보드의 좌표 
    int matrixX;
    int matrixY;


    // 공격 인식 인데..이건 나중에 해야함 false 일때 움직임 아니면 공격



    public void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");


        controller.GetComponent<CoGame>().SetPositionEmpty(reference.GetComponent<CoLaserMan>().GetXBoard(),
            reference.GetComponent<CoLaserMan>().GetYBoard());

        reference.GetComponent<CoLaserMan>().SetXBoard(matrixX);
        reference.GetComponent<CoLaserMan>().SetYBoard(matrixY);
        reference.GetComponent<CoLaserMan>().SetCoords();

        controller.GetComponent<CoGame>().SetPosition(reference);

        //여기 발사 넣어야함
        
        controller.GetComponent<CoGame>().NextTurn();
        
        reference.GetComponent<CoLaserMan>().DestoryMove();
        //controller.GetComponent<Game>().Shot();

    }

    public void SetCoords(int x,int y)
    {
        matrixX = x;
        matrixY = y;

    }

    public void SetReference(GameObject obj)
    {
        reference = obj;
    }
    /*public GameObject GeReference()
    {
        return reference;
    }*/


}
