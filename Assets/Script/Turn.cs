using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Turn : MonoBehaviour
{
    public int turnsRemaining;
    public string targetSceneName;

    public Button[] allButtons;

    public bool Player_Set = false;

    private bool previousPlayerSetState;


    private void Start()
    {

        allButtons = FindObjectsOfType<Button>();

        allButtons = allButtons.Where(button => !ShouldExcludeButton(button)).ToArray();
        

        previousPlayerSetState = false;
        //false
    }

    bool ShouldExcludeButton(Button button)
    {
        return button.name == "Done";
    }

    public void setPreviousPlayerSetState(bool state)
    {
        previousPlayerSetState = state;
    }


    public void Turns(GameObject targetObject)
    {
        Dice_Start diceStartScript = targetObject.GetComponent<Dice_Start>();
        if (diceStartScript != null && turnsRemaining > 0)
        {
            diceStartScript.StartDiceRoll();
            turnsRemaining--;


            Debug.Log("Turns Remaining: " + turnsRemaining);

        }
        else if (diceStartScript == null)
        {
            Debug.Log("Missing Dice_Start script on target object.");
        }
        else
        {
            Debug.Log("No turns remaining.");
        }
    }

    public void MultiTurns(GameObject targetObject)
    {
        Dice_Start diceStartScript = targetObject.GetComponent<Dice_Start>();
        if (diceStartScript != null && turnsRemaining > 0)
        {
            diceStartScript.StartDiceRoll();
            turnsRemaining--;

           // Player_Set = !Player_Set;
            // 1. true  2. false 3. true

            // 이전 상태와 현재 상태가 다를 때에만 버튼 상태 변경
            // if (previousPlayerSetState != Player_Set)
            // {
            //     foreach (Button button in allButtons)
            //     {
            //         button.interactable = !button.interactable; 
            //     }
            //     previousPlayerSetState = Player_Set; 
            //     // 2. false  3. true
            // }

            allButtons = FindObjectsOfType<Button>();

            allButtons = allButtons.Where(button => !ShouldExcludeButton(button)).ToArray();

            foreach (Button button in allButtons)
            {
                if (turnsRemaining <= 0 && button.name == "Dice_set") continue;
                button.interactable = !button.interactable; 
            }
            previousPlayerSetState = Player_Set; 


            Debug.Log("Turns Remaining: " + turnsRemaining);
        }
        else if (diceStartScript == null)
        {
            Debug.Log("Missing Dice_Start script on target object.");
        }
        else
        {
            Debug.Log("No turns remaining.");
        }
    }
}
