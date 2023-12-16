using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Score_add : MonoBehaviour
{
    private List<Transform> diceTransforms = new List<Transform>();
    private List<int> diceValues = new List<int>();  // 각 주사위의 값 저장
    public int totalScore = 0;

    public int Player_1 = 0;
    public int Player_2 = 0;

    void Start()
    {
        FindAllDiceTransforms();
    }

    public void FindAllDiceTransforms()
    {
        GameObject[] diceObjects = GameObject.FindGameObjectsWithTag("Dice");
        foreach (GameObject diceObject in diceObjects)
        {
            diceTransforms.Add(diceObject.transform);
        }
    }

    public int CalculateTotalScore(int ruleNum)
    {
        totalScore = 0;
        diceValues.Clear();  // 주사위 값 리스트 초기화

        foreach (Transform diceTransform in diceTransforms)
        {
            Diceface topDiceface = FindTopDiceface(diceTransform);

            if (topDiceface != null)
            {
                int faceValue = topDiceface.faceValue;
                diceValues.Add(faceValue);  // 주사위 값 저장
            }
        }

        // 각 조건에 따라 점수 계산
        totalScore += CalculateScoreByRule(ruleNum);
        return totalScore;
    }

    private int CalculateScoreByRule(int ruleNum)
    {
        int score = 0;

        switch (ruleNum)
        {
            case 1:
                // Ones
                score += CountDiceWithValue(1) * 1;
                break;
            case 2:
                // Twos
                score += CountDiceWithValue(2) * 2;
                break;
            case 3:
                // Threes
                score += CountDiceWithValue(3) * 3;
                break;
            case 4:
                // Fours
                score += CountDiceWithValue(4) * 4;
                break;
            case 5:
                // Fives
                score += CountDiceWithValue(5) * 5;
                break;
            case 6:
                // Sixes
                score += CountDiceWithValue(6) * 6;
                break;
            case 7:
                // Four of a Kind
                if (CheckNOfAKind(4))
                {
                    score += GetDiceValueWithCount(4) * 4;
                }
                break;
            case 8:
                // Full House
                if (CheckFullHouse())
                {
                    score += GetDiceValueWithCount(3) * 3;
                    score += GetDiceValueWithCount(2) * 2;
                }
                break;
            case 9:
                // Little Straight
                if (CheckLittleStraight())
                {
                    score += 15;
                }
                break;
            case 10:
                // Big Straight
                if (CheckBigStraight())
                {
                    score += 30;
                }
                break;
            case 11:
                // Yacht
                if (CheckNOfAKind(5))
                {
                    score += 50;
                }
                break;
            case 12:
                // Choice
                score += diceValues.Sum();
                break;
            default:
                break;
        }

        return score;
    }


    private int CountDiceWithValue(int value)
    {
        return diceValues.Count(v => v == value);
    }

    private int GetDiceValueWithCount(int count)
    {
        return diceValues.FirstOrDefault(v => CountDiceWithValue(v) == count);
    }

    private bool CheckNOfAKind(int n)
    {
        return diceValues.Any(value => CountDiceWithValue(value) >= n);
    }

    private bool CheckFullHouse()
    {
        return CheckNOfAKind(3) && CheckNOfAKind(2);
    }

    private bool CheckLittleStraight()
    {
        List<int> uniqueValues = diceValues.Distinct().OrderBy(v => v).ToList();
        return (uniqueValues.Count == 4 && uniqueValues.SequenceEqual(new List<int> { 1, 2, 3, 4 })) ||
               (uniqueValues.Count == 4 && uniqueValues.SequenceEqual(new List<int> { 2, 3, 4, 5 })) ||
               (uniqueValues.Count == 4 && uniqueValues.SequenceEqual(new List<int> { 3, 4, 5, 6 }));
    }

    private bool CheckBigStraight()
    {
        List<int> uniqueValues = diceValues.Distinct().OrderBy(v => v).ToList();
        return (uniqueValues.Count == 5 && uniqueValues.SequenceEqual(new List<int> { 2, 3, 4, 5, 6 })) ||
               (uniqueValues.Count == 5 && uniqueValues.SequenceEqual(new List<int> { 1, 2, 3, 4, 5 }));
    }


    private Diceface FindTopDiceface(Transform diceTransform)
    {
        Diceface[] dicefaces = diceTransform.GetComponentsInChildren<Diceface>();

        if (dicefaces.Length > 0)
        {
            Diceface topDiceface = dicefaces[0];
            float maxY = topDiceface.transform.position.y;

            foreach (Diceface diceface in dicefaces)
            {
                if (diceface.transform.position.y > maxY)
                {
                    maxY = diceface.transform.position.y;
                    topDiceface = diceface;
                }
            }

            return topDiceface;
        }

        return null;
    }
}
