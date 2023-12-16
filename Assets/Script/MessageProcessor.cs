using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class MessageProcessor : MonoBehaviour
{
    private Turn turnScript;
    private Score_add scoreAddScript;
    private Turn_Controller Turn_ControllerScripts;
    private UnityMainThreadDispatcher unityMainThreadDispatcher;
    private GameObject targetObject;
    private Click_Contorller Click_ContorllerScripts;
    private Score_add Score_addScripts;

    // Start is called before the first frame update
    void Start()
    {
        unityMainThreadDispatcher = FindObjectOfType<UnityMainThreadDispatcher>();
        turnScript = FindObjectOfType<Turn>();
        scoreAddScript = FindObjectOfType<Score_add>();
        Turn_ControllerScripts = FindObjectOfType<Turn_Controller>();
        Click_ContorllerScripts = FindObjectOfType<Click_Contorller>();
        Score_addScripts = FindObjectOfType<Score_add>();

        targetObject = GameObject.Find("Dice_set");
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

        Dictionary<string, string> data = JsonConvert.DeserializeObject<Dictionary<string, string>>(message);

        switch (type) {
            case "dice-value":
                Debug.Log("dice-value: " + data["value"]);
                int score = int.Parse(data["value"]);
                string player = data["player"];
                unityMainThreadDispatcher.Enqueue(() =>
                {
                    Turn_ControllerScripts.Multi_Controll_Enemy(score, player);
                });
                break;

            case "player-set":
                Debug.Log("player set : " + data["player"]);
                if (!turnScript.Player_Set) {
                    if (data["player"] == "true") turnScript.Player_Set = true;
                }

                try{
                    Debug.Log("MultiTurns");

                    
                    unityMainThreadDispatcher.Enqueue(() => {
                        if (data["player"] == "false") turnScript.MultiTurns(targetObject);
                        else
                        {
                            targetObject.GetComponent<Dice_Start>().StartDiceRoll();
                            turnScript.setPreviousPlayerSetState(turnScript.Player_Set);
                            turnScript.turnsRemaining--;
                        }
                        Turn_ControllerScripts.setCubeMoveScript(FindObjectOfType<cubeMove>());
                        Score_addScripts.FindAllDiceTransforms();

                        Click_ContorllerScripts.init();
                    });
                }
                catch (System.Exception ex)
                {
                    Debug.Log(ex);
                }

                break;

            case "enemy-score":
                Debug.Log("enemy-score: " + data["score"]);
                unityMainThreadDispatcher.Enqueue(() => {
                    this.turnEnd(data["score"]);
                });
                break;
        }

        
    }

    private void turnEnd(string score)
    {

        scoreAddScript.totalScore += Int32.Parse(score);
        Turn_ControllerScripts.Multi_Controll();

    }

}
