using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject cardPrefab;
    public Sprite[] cardSprites;

    private List<int> cardIDList = new List<int>();
    private List<Card> cardList = new List<Card>();

    // Start is called before the first frame update
    void Start()
    {
        GenerateCardID();
        ShuffleCardID();
        InitBoard();
    }



    void GenerateCardID()   //[0,0,1,1,2,2,3,3.....9,9]
    {
        for (int i = 0; i < cardSprites.Length; i++)
        {
            cardIDList.Add(i);
            cardIDList.Add(i);
        }
    }
    
    void ShuffleCardID()
    {
        int cardCount = cardIDList.Count;
        for(int i =0;i< cardCount; i++)
        {
            int randomIndex = Random.Range(i, cardCount);
            int temp = cardIDList[randomIndex];
            cardIDList[randomIndex] = cardIDList[i];
            cardIDList[i] = temp;
            //Debug.Log(cardIDList[i]);
        }
    }

    public void InitBoard()
    {
        float spaceX = 1.3f;
        float spaceY = 1.8f;
        int rowCount = 5;
        int colCount = 4;
        int cardIndex = 0;

        for (int row = 0; row < rowCount; row++)
        {
            for (int col = 0; col < colCount; col++)
            {
                float posX = (col - (int)(colCount / 2)) * spaceX + (spaceX / 2);
                float posY = (row - (int)(rowCount / 2)) * spaceY;
                Vector3 pos = new Vector3(posX, posY, 0f);
                GameObject cardObject = Instantiate(cardPrefab, pos, Quaternion.identity);
                Card card = cardObject.GetComponent<Card>();
                int cardID = cardIDList[cardIndex++];
                card.SetCardID(cardID);
                card.SetCardSprite(cardSprites[cardID]);
                cardList.Add(card);
            }
        }
    }

    public List<Card> GetCards()
    {
        return cardList;
    }
}
