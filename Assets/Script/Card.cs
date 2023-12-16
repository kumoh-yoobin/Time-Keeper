using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Card : MonoBehaviour
{
    [SerializeField]
    private Sprite cardBack;
    [SerializeField]
    private Sprite cardFront;

    [SerializeField]
    private SpriteRenderer cardRenderer;

    private bool isMatched = false;
    private bool isFlipped = false;
    private bool isFlipping = false;
    public int cardID;

    public void SetCardID(int id)
    {
        this.cardID = id;
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
    private void OnMouseDown()
    {
        if(!isFlipping && !isMatched)
        {
            GameManager.instance.CardClicked(this);
        }
    }
}
