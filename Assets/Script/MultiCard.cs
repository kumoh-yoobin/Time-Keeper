using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Newtonsoft.Json;

public class MultiCard : MonoBehaviour
{
    [SerializeField]
    private Sprite cardBack;
    [SerializeField]
    private Sprite cardFront;
    private CardWebSocketClient webSocketManager;

    [SerializeField]
    private SpriteRenderer cardRenderer;

    private bool isMatched = false;
    private bool isFlipped = false;
    private bool isFlipping = false;
    public int cardID;
    public int cardIndex;

    private void Awake() {
        webSocketManager = FindObjectOfType<CardWebSocketClient>();
    }

    public void SetCardID(int id)
    {
        this.cardID = id;
    }
    public void SetCardIndex(int index)
    {
        this.cardIndex = index;
    }
    public void SetMatched()
    {
        isMatched = true;
    }
    public void SetCardSprite(Sprite sprite)
    {
        this.cardFront = sprite;
    }

    public void FilpCard()
    {

        isFlipping = true;

        Vector3 originScale = transform.localScale;
        Vector3 targetScale = new Vector3(0f, originScale.y, originScale.z);    

        transform.DOScale(targetScale, 0.2f).OnComplete(() =>
        {
            isFlipped = !isFlipped;
            //Debug.Log(isFlipped);
            if (isFlipped)
            {
                cardRenderer.sprite = cardFront;
            }
            else
            {
                cardRenderer.sprite = cardBack;
            }
            transform.DOScale(originScale, 0.2f).OnComplete(() =>
            {
                isFlipping = false;
            });
        });

        
    }
    public MultiCard GetCardWithIndex(int index)
    {
        if (cardIndex == index)
        {
            return this;
        }
        else
        {
            return null;
        }
    }

    private void OnMouseDown()
    {

        if (!isFlipping && !isMatched && MultiGameManager.instance.isPlayerTurn())
        {
            var msgData = new
            {
                type = "card-click",
                cardIndex = JsonConvert.SerializeObject(cardIndex),
            };
            string msg = JsonConvert.SerializeObject(msgData);
            var data = new
            {
                type = "roomMsg",
                roomType = "card",
                roomCode = webSocketManager.getRoomCode(),
                msg = msg
            };
            webSocketManager.sendMessage(data);
            // MultiGameManager.instance.CardClicked(this);
        }
    }
}
