using UnityEngine;

public class Dice_Start : MonoBehaviour
{
    public GameObject dicePrefab; 
    public int maxDiceCount = 5;
    public Vector2 spawnRangeX = new Vector2(-20, 20f); 
    public Vector2 spawnRangeZ = new Vector2(0f, 15f);

    public void StartDiceRoll()
    {
        cubeMove[] cubeMoveScripts = FindObjectsOfType<cubeMove>();

        foreach (cubeMove cubeMoveScript in cubeMoveScripts)
        {
            cubeMoveScript.ResetRollCount();
        }

        int currentDiceCount = GameObject.FindGameObjectsWithTag("Dice").Length;

        int additionalDiceCount = Mathf.Max(0, maxDiceCount - currentDiceCount);

        for (int i = 0; i < additionalDiceCount; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(spawnRangeX.x, spawnRangeX.y), 0f, Random.Range(spawnRangeZ.x, spawnRangeZ.y));
            GameObject newDice = Instantiate(dicePrefab, randomPosition, Quaternion.identity);
            newDice.name = "Dice";  
        }
    }
}
