using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private List<Card> allCards;
    private Card flippedCard;   //현재 필드에 뒤집혀 있는 카드가 존재하는가?
    private bool isFlipping = false;

    private bool playerTurn;    //true = 1p, false = 2p
    private bool gamePlayer; // true = 1p, false =2p 
    private int oneScore = 0;
    private int twoScore = 0;
    private int mathcesFound = 0;
    private int totalFound = 10;
    private bool gameResult;    //게임 승패 유무 판단

    [SerializeField]
    private Slider timeoutSlider;
    [SerializeField]
    private float timeLimit = 7f;
    [SerializeField]
    private TextMeshProUGUI oneplayerScore;

    [SerializeField]
    private TextMeshProUGUI twoplayerScore;

    [SerializeField]
    private GameObject gameOverPanel;

    [SerializeField]
    private TextMeshProUGUI gameOverTEXT;

    private float currentTime;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        Board board = FindObjectOfType<Board>();
        allCards = board.GetCards();
        playerTurn = true;
        gameResult = false;
        currentTime = timeLimit;
        SetPlayerSCoreText();
        StartCoroutine("FlipAllCardsRoutine");
    }

    void Update()
    {
        SetPlayerSCoreText();
    }

    void SetPlayerSCoreText()
    {
        oneplayerScore.text = "1P : " + oneScore.ToString();
        twoplayerScore.text = "2P : " + twoScore.ToString();
    }

    IEnumerator FlipAllCardsRoutine()
    {
        isFlipping = true;
        yield return new WaitForSeconds(0.5f);
        FlipAllCards();
        yield return new WaitForSeconds(3f);
        FlipAllCards();
        yield return new WaitForSeconds(0.5f);
        isFlipping = false;

        yield return StartCoroutine("CountDownTimerRoutine");
    }
    
    IEnumerator CountDownTimerRoutine()
    {
        while(currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            timeoutSlider.value = currentTime / timeLimit;
            yield return null;
        }

        SetPlayerTurn();

        if(!gameResult)
        {
            yield return StartCoroutine("CountDownTimerRoutine");
        }
    }

    void FlipAllCards()         
    {
        for(int i = 0; i < allCards.Count; i++)
        {
            allCards[i].FilpCard();
        }
    }

    public void SetPlayerTurn()
    {
        if(playerTurn)
        {
            Debug.Log("2P start");
            playerTurn = false;
        }
        else
        {
            Debug.Log("1P start");
            playerTurn = true;
        }
        currentTime = timeLimit;
    }

    public void SetPlayerScore()
    {
        if(playerTurn)
        {
            oneScore += 1;
        }
        else
        {
            twoScore += 1;
        }
    }

    public void CardClicked(Card card)
    {

        if (isFlipping)
        {
            return;
        }
        card.FilpCard();

        if(flippedCard == null)
        {
            flippedCard = card;
        }
        else
        {
            StartCoroutine(CheckMatchRoutine(flippedCard, card));
        }
    }

    IEnumerator CheckMatchRoutine(Card card1, Card card2)
    {
        isFlipping = true;
        if (card1.cardID == card2.cardID)
        {
            if (card1 == card2)
            {
                Debug.Log("me");
            }
            else
            {
                Debug.Log("same");
                card1.SetMatched();
                card2.SetMatched();
                mathcesFound++;
                SetPlayerScore();
                SetPlayerTurn();
                if(mathcesFound == totalFound)
                {
                    GameOver();
                }
            }
        }
        else
        {
            Debug.Log("different");
            yield return new WaitForSeconds(1f);

            card1.FilpCard();
            card2.FilpCard();
            SetPlayerTurn();
            yield return new WaitForSeconds(1f);
        }
        isFlipping = false;
        flippedCard = null;

        Debug.Log(oneScore);
        Debug.Log(twoScore);
    }

    void GameOver()
    {

        StopCoroutine("CountDownTimerRoutine");

        if(oneScore > twoScore)
        {
            Debug.Log("1P win");
            gameOverTEXT.SetText("1P win");
        }
        else
        {
            Debug.Log("2P win");    //동점인 경우, 후턴 플레이어의 승리
            gameOverTEXT.SetText("2P win");
        }
        gameResult = true;

        Invoke("ShowGameOverPanel", 2f);
    }

    void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    
}
