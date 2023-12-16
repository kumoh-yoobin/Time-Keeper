using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiBoard : MonoBehaviour
{
    public GameObject cardPrefab;
    public Sprite[] cardSprites;

    private List<int> cardIDList = new List<int>();
    private List<MultiCard> cardList = new List<MultiCard>();

    // Start is called before the first frame update
    void Start()
    {
    }

    private List<int> ConvertStringToList(string str)
    {
        List<int> resultList = new List<int>();
        // 문자열에서 괄호를 제거하고, 쉼표로 분리
        string[] numbers = str.Trim(new char[] { '[', ']' }).Split(',');

        foreach (string number in numbers)
        {
            // 공백 제거 후 숫자로 변환
            if (int.TryParse(number.Trim(), out int parsedNumber))
            {
                resultList.Add(parsedNumber);
            }
        }

        return resultList;
    }

    public void GenerateCardID(string str)
    {
        Debug.Log("GenerateCardID: " + str);
        cardIDList = ConvertStringToList(str);
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
                MultiCard card = cardObject.GetComponent<MultiCard>();
                int cardID = cardIDList[cardIndex++];
                card.SetCardID(cardID);
                card.SetCardIndex(cardIndex);
                card.SetCardSprite(cardSprites[cardID]);
                cardList.Add(card);
            }
        }
    }

    public List<MultiCard> GetCards()
    {
        return cardList;
    }
}
