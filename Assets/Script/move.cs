using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class move : MonoBehaviour
{
    public GameObject controller;

    public GameObject reference = null;
    //������ ��ǥ 
    int matrixX;
    int matrixY;

    private LaserWebSocketClient client;

    void Start() {
        client = FindObjectOfType<LaserWebSocketClient>();
    }

    private void sendMoveData() {
        var vector = new {
            type = "move-piece",
            matrixX = this.matrixX,
            matrixY = this.matrixY,
            name = reference.name
        };
        string vectorJson = JsonConvert.SerializeObject(vector);


        var data = new {
            type = "roomMsg",
            roomType = "laser",
            roomCode = client.getRoomCode(),
            msg = vectorJson
        };

        client.sendMessage(data);
    }


    // ���� �ν� �ε�..�̰� ���߿� �ؾ��� false �϶� ������ �ƴϸ� ����

    public void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");


        controller.GetComponent<Game>().SetPositionEmpty(reference.GetComponent<LaserMan>().GetXBoard(),
            reference.GetComponent<LaserMan>().GetYBoard());


        this.sendMoveData();
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
