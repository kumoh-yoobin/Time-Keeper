using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class CardMessageProcessor : MonoBehaviour
{

    private MultiGameManager multiGameManager;
    private MultiBoard multiBoard;
    private UnityMainThreadDispatcher unityMainThreadDispatcher;
    // Start is called before the first frame update
    void Start()
    {
        unityMainThreadDispatcher = FindObjectOfType<UnityMainThreadDispatcher>();

        StartCoroutine(InitializePiecesAfterDelay(1f));
    }

    IEnumerator InitializePiecesAfterDelay(float delay) {
        yield return new WaitForSeconds(delay); // 딜레이
        multiGameManager = FindObjectOfType<MultiGameManager>();
        multiBoard = FindObjectOfType<MultiBoard>();
    }

    // Update is called once per frame
    void Update()
    {
        
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

        if (unityMainThreadDispatcher == null) {
            Debug.Log("unityMainThreadDispatcher is null");
        }
        if (multiGameManager == null) {
            Debug.Log("multiGameManager is null");
        }
        if (multiBoard == null) {
            Debug.Log("multiBoard is null");
        }



        Dictionary<string, string> data = JsonConvert.DeserializeObject<Dictionary<string, string>>(message);

        Debug.Log("parsed Data: " + data);

        switch (type) {
            case "player-set":
                try{
                    multiGameManager.setPlayer(data["player"] == "true");
                }
                catch(System.Exception e){
                    Debug.Log(e);
                }
                break;
            case "card-set":
                try{
                    unityMainThreadDispatcher.Enqueue(() => {
                        multiBoard.GenerateCardID(data["cards"]);
                        multiBoard.InitBoard();
                        multiGameManager.Waiting();
                    });
                }
                catch(System.Exception e){
                    Debug.Log(e);
                }
                break;
            case "card-click":
                try{
                    unityMainThreadDispatcher.Enqueue(() => {
                        multiGameManager.CardClicked(int.Parse(data["cardIndex"]));
                    });
                }
                catch(System.Exception e){
                    Debug.Log(e);
                }
                break;
        }

        
    }

    // 필요한 함수 작성...
}
