using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class LaserMessageProcessor : MonoBehaviour
{
    private Game game;
    private Dictionary<string, LaserMan> pieceDictionary = new Dictionary<string, LaserMan>();

    private UnityMainThreadDispatcher dispatcher;

    void Start()
    {
        dispatcher = FindObjectOfType<UnityMainThreadDispatcher>();
        // 코루틴 시작
        StartCoroutine(InitializePiecesAfterDelay(1f)); // 1초 후에 InitializePieces를 호출합니다.
    }

    IEnumerator InitializePiecesAfterDelay(float delay) {
        yield return new WaitForSeconds(delay); // 딜레이
        game = FindObjectOfType<Game>();
        InitializePieces();
    }

    private void InitializePieces() {
        foreach (LaserMan laserMan in FindObjectsOfType<LaserMan>()) {
            pieceDictionary[laserMan.gameObject.name] = laserMan;
            // Debug.Log("pieceDictionary: " + laserMan.gameObject.name);
        }
        // 초기화 완료 후 필요한 추가 작업 수행
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void movePiece(string name, int matrixX, int matrixY) {
        try {
            LaserMan laserMan = pieceDictionary[name];
            Debug.Log("movePiece: " + laserMan.gameObject.name + ", " + matrixX + ", " + matrixY);
            game.SetPositionEmpty(laserMan.GetXBoard(), laserMan.GetYBoard());
            laserMan.SetXBoard(matrixX);
            laserMan.SetYBoard(matrixY);
            laserMan.SetCoords();
            game.SetPosition(laserMan.gameObject);
            game.NextTurn();
            laserMan.DestoryMove();
        }
        catch (System.Exception e) {
            Debug.LogError("Error moving piece: " + e.Message);
        }
    }

    private void rotatePiece(string name) {
        try{
            LaserMan laserMan = pieceDictionary[name];
            laserMan.RotatePiece();
        }
        catch (System.Exception e) {
            Debug.LogError("Error rotating piece: " + e.Message);
        }
    }

    private void nextTurn() {
        game.NextTurn();
    }

    public void processMessage(string type, string message)
    {
        Debug.Log("processMessage: " + message);
        // Data data = JsonConvert.DeserializeObject<Data>(message);
        // Debug.Log(data.msg);
        // Debug.Log(data.data);

        // 메세지 처리 함수 호출
        // type과 메세지를 같이 전송받음...
        // message는 JSON의 형태로 전송
        // JSON을 파싱해서 필요한 데이터를 추출

        Dictionary<string, string> data = JsonConvert.DeserializeObject<Dictionary<string, string>>(message);

        switch (type) {
            case "move-piece":
                Debug.Log("move-piece: " + data["matrixX"] + ", " + data["matrixY"]);
                dispatcher.Enqueue(() => movePiece(data["name"], int.Parse(data["matrixX"]), int.Parse(data["matrixY"])));
                // movePiece(data["name"], int.Parse(data["matrixX"]), int.Parse(data["matrixY"]));
                break;

            case "rotate-piece":
                Debug.Log("rotate-piece: " + data["name"]);
                dispatcher.Enqueue(() => rotatePiece(data["name"]));
                // rotatePiece(data["name"]);
                break;

            case "next-turn":
                Debug.Log("next-turn");
                dispatcher.Enqueue(() => nextTurn());
                // nextTurn();
                break;

            case "player-set":
                Debug.Log("player-set: " + data["player"]);
                game.SetPlayer(data["player"]);
                break;
        }

        
    }

    // 필요한 함수 작성...
}
