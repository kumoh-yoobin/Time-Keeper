using UnityEngine;
using TMPro;
using Newtonsoft.Json;

public class Turn_Controller : MonoBehaviour
{
    private Turn turnScript;
    private cubeMove cubeMoveScript;
    private Score_add scoreAddScript;
    private WebSocketClient client;
    public Canvas DoneCanvas;


    public TextMeshProUGUI player1ScoreText; 
    public TextMeshProUGUI player2ScoreText; 
    public TextMeshProUGUI WinerScoreText;
    public TextMeshProUGUI DoneScoreText;

    void Start()
    {
        turnScript = FindObjectOfType<Turn>();
        cubeMoveScript = FindObjectOfType<cubeMove>();
        scoreAddScript = FindObjectOfType<Score_add>();
        client = FindObjectOfType<WebSocketClient>();
        WinerScoreText.enabled = false;
        DoneCanvas.enabled = false; 
    }

    void Update()
    {

    }

    public void enableWinnerText()
    {
        WinerScoreText.enabled = true;
    }

    public void setCubeMoveScript(cubeMove cubeMoveScript)
    {
        this.cubeMoveScript = cubeMoveScript;
    }

    public void Turn_Controll()
    {
        if (turnScript.turnsRemaining == 0 && turnScript != null)
        {
            if (scoreAddScript.Player_1 > scoreAddScript.Player_2)
            {
                WinerScoreText.text = "Player_1 Win!";
            }
            else if (scoreAddScript.Player_1 < scoreAddScript.Player_2)
            {
                WinerScoreText.text = "Player_2 Win!";
            }
            else
            {
                WinerScoreText.text = "Mu";
            }
            WinerScoreText.enabled = true;
        }
        if (cubeMoveScript.RollCount == 3 && turnScript != null )
        {
            if (turnScript.turnsRemaining > 0)
            {
                if ((turnScript.turnsRemaining % 2) == 0)
                {
                    scoreAddScript.Player_1 += scoreAddScript.totalScore;
                    player1ScoreText.text = "Player_1 Score: " + scoreAddScript.Player_1;
                }
                else
                {
                    scoreAddScript.Player_2 += scoreAddScript.totalScore;
                    player2ScoreText.text = "Player_2 Score: " + scoreAddScript.Player_2;
                }
            }

            turnScript.Turns(gameObject);
        }
    }


    private void showWinner() {
        if (turnScript.turnsRemaining == 0 && turnScript != null)
        {
            if (scoreAddScript.Player_1 > scoreAddScript.Player_2)
            {
                WinerScoreText.text = "Player_1 Win!";
            }
            else if (scoreAddScript.Player_1 < scoreAddScript.Player_2)
            {
                WinerScoreText.text = "Player_2 Win!";
            }
            else
            {
                WinerScoreText.text = "Mu";
            }
            WinerScoreText.enabled = !WinerScoreText.enabled;
        }
    }

    public void Multi_Controll()
    {

        if (cubeMoveScript.RollCount == 3 && turnScript != null)
        {
            if (turnScript.turnsRemaining > 0)
            {
                var msg = new
                {
                    type = "dice-value",
                    value = scoreAddScript.totalScore.ToString(),
                    player = turnScript.Player_Set.ToString(),
                };
                string msg_string = JsonConvert.SerializeObject(msg);

                var data = new
                {
                    type = "roomMsg",
                    roomType = "yacht",
                    roomCode = client.getRoomCode(),
                    msg = msg_string
                };
                client.sendMessage(data);
            }
        }
    }

    public void Multi_Controll_Enemy(int score, string player)
    {

        Debug.Log("get Score : " + score);

        if (turnScript != null)
        {
            if (turnScript.turnsRemaining > 0)
            {
                if(player == "True"){
                    try
                    {
                        scoreAddScript.Player_1 += score;
                        player1ScoreText.text = "Player_1 Score: " + scoreAddScript.Player_1;
                        DoneScoreText.text = "Player_1 Score: " + score;
                        DoneCanvas.enabled = true;    
                    }
                    catch (System.Exception ex)
                    {
                        Debug.Log(ex);
                    }
                }
                else
                {
                    try
                    {
                        scoreAddScript.Player_2 += score;
                        player2ScoreText.text = "Player_2 Score: " + scoreAddScript.Player_2;
                        DoneScoreText.text = "Player_2 Score: " + score;
                        DoneCanvas.enabled = true;    
                    }
                    catch (System.Exception ex)
                    {
                        
                        Debug.Log(ex);
                    }

    
                }
            }

            Debug.Log("Turn end");

            turnScript.MultiTurns(gameObject);
            this.showWinner();
        }
    }
}
