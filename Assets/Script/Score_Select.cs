using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class Score_Select : MonoBehaviour
{
    private Score_add scoreAddScript;
    private Turn turnScript;

    public int ruleNum;
    public int Select_Score;

    public UnityEngine.UI.Toggle toggle1;
    public UnityEngine.UI.Toggle toggle2;

    void Start()
    {
        scoreAddScript = FindObjectOfType<Score_add>();
        turnScript = FindObjectOfType<Turn>();
    }

    public void Selected_Rule()
    {
        Select_Score = scoreAddScript.CalculateTotalScore(ruleNum);
    }

    public void Toggle_Player()
    {
        if (turnScript.turnsRemaining > 0)
        {
            if ((turnScript.turnsRemaining % 2) == 0)
            {
                toggle1.isOn = true;
            }
            else
            {
                toggle2.isOn = true;
            }
        }
    }

}
