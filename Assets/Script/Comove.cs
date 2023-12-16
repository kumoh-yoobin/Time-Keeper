using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comove : MonoBehaviour
{
    public GameObject controller;

    GameObject reference = null;
    //������ ��ǥ 
    int matrixX;
    int matrixY;


    // ���� �ν� �ε�..�̰� ���߿� �ؾ��� false �϶� ������ �ƴϸ� ����



    public void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");


        controller.GetComponent<CoGame>().SetPositionEmpty(reference.GetComponent<CoLaserMan>().GetXBoard(),
            reference.GetComponent<CoLaserMan>().GetYBoard());

        reference.GetComponent<CoLaserMan>().SetXBoard(matrixX);
        reference.GetComponent<CoLaserMan>().SetYBoard(matrixY);
        reference.GetComponent<CoLaserMan>().SetCoords();

        controller.GetComponent<CoGame>().SetPosition(reference);

        //���� �߻� �־����
        
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
