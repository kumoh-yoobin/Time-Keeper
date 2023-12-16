using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{

    private void OnEnable()
    {
        // SceneManager.sceneLoaded 이벤트에 OnSceneLoaded 메서드를 구독
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // 스크립트가 비활성화되거나 파괴될 때 이벤트 구독 해제
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬이 로드되었을 때 호출되는 메서드

        // 로드된 씬의 이름을 확인
        string loadedSceneName = scene.name;

        // 특정 씬에 진입한 경우
        if (loadedSceneName == "Co-opScene")
        {
            GameObject targetObject = GameObject.Find("Dice_set");
            Turn TurnScript = GetComponent<Turn>();
            if (TurnScript != null)
            {
                TurnScript.Turns(targetObject);
            }
        }

        if (loadedSceneName == "DiceMultiScene")
        {
            // GameObject targetObject = GameObject.Find("Dice_set");
            // Turn TurnScript = GetComponent<Turn>();
            // if (TurnScript != null)
            // {
            //     TurnScript.MultiTurns(targetObject);
            // }
        }

    }
}
