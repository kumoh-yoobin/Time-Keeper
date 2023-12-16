using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{

    private void OnEnable()
    {
        // SceneManager.sceneLoaded �̺�Ʈ�� OnSceneLoaded �޼��带 ����
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // ��ũ��Ʈ�� ��Ȱ��ȭ�ǰų� �ı��� �� �̺�Ʈ ���� ����
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ���� �ε�Ǿ��� �� ȣ��Ǵ� �޼���

        // �ε�� ���� �̸��� Ȯ��
        string loadedSceneName = scene.name;

        // Ư�� ���� ������ ���
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
