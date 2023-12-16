using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Text;


public class CardWebSocketClient : MonoBehaviour
{
    private string IP = "localhost";
    private string PORT = "8080";

    public WebSocket m_Socket = null;

    private string uid = "";
    private string roomCode = "";

    public CardMessageProcessor messageProcessor;

    IEnumerator<object> delay_join()
    {
        yield return new WaitForSeconds(1f);

        // 1초 딜레이 후에 수행할 동작 추가
        Debug.Log("Delayed action after 1 second");
        this.JoinRoom();
    }

    private void Start()
    {
        try
        {
            Debug.Log("Start");
            m_Socket = new WebSocket("ws://" + IP + ":" + PORT);
            m_Socket.OnMessage += Recv;
            m_Socket.OnClose += CloseConnect;
            this.Connect();

            StartCoroutine(delay_join());
        }
        catch (Exception e)
        { 
            Debug.Log(e.ToString());
        }
       
    }

    //서버 연결함수
    public void Connect()
    {
        try
        {
           if(m_Socket == null || !m_Socket.IsAlive)
                m_Socket.Connect();

        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    private void CloseConnect(object sender, CloseEventArgs e)
    {
        Debug.Log("CloseConnect");
        DisconncectServer();
    }
    //연결 해제 함수
    public void DisconncectServer()
    {
        try
        {
            if (m_Socket == null)
                return;

            if (m_Socket.IsAlive)
                m_Socket.Close();

        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }
    

    //서버로 부터 받은 데이터 처리
    public void Recv(object sender, MessageEventArgs e)
    {
        //string 데이터
        Debug.Log(e.Data);

        Data d = JsonConvert.DeserializeObject<Data>(e.Data);
        if(d.msg == "alert-uid") {
            Debug.Log("connect Socket Server");
            this.uid = d.data;
        }
        else if (d.msg == "join-room") {
            Debug.Log("join Room");
            this.roomCode = d.data;
        }
        else if (d.msg == "room-msg") {
            Debug.Log("room-msg");
            messageProcessor.processMessage(d.type, d.data);
        }
    }
   
    private void OnApplicationQuit()
    {
        DisconncectServer();
    }

    public string getRoomCode() {
        return this.roomCode;
    }

    public void JoinRoom() {
        if (!m_Socket.IsAlive)
            return;

        try{
            var data = new
            {
                type = "join",
                roomType = "card",
                user = this.uid
            };

            this.sendMessage(data);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }

    public void SendMessageTest() {
        if (!m_Socket.IsAlive)
            return;

        try
        {
            var data = new
            {
                type="roomMsg",
                roomType = "yacht",
                roomCode = this.roomCode,
                msg = "test"
            };

            sendMessage(data);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }

    public void sendMessage(object data) {
        if (!m_Socket.IsAlive)
            return;
        try
        {
            string json = JsonConvert.SerializeObject(data);
            Debug.Log(json);

            m_Socket.Send(Encoding.UTF8.GetBytes(json));
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }
}