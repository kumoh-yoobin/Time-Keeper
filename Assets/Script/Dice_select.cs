using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice_select : MonoBehaviour
{
    private Vector3 initialSetPosition;
    private GameObject[] setObjects;
    private List<GameObject> unfixedDiceList = new List<GameObject>();
    private int currentSetIndex = 0;
    private Vector3 defaultDicePosition = Vector3.zero;

    private cubeMove[] cubeMoveScripts;
    private cubeMove moveComponent;

    void Start()
    {
        setObjects = GameObject.FindGameObjectsWithTag("Set");

        if (setObjects.Length > 0)
        {
            initialSetPosition = setObjects[0].transform.position;
        }

        defaultDicePosition = new Vector3(0, 1, 0);

        cubeMoveScripts = FindObjectsOfType<cubeMove>();
    }

    void ToggleDiceMovement(GameObject diceObject, bool isEnabled)
    {
        moveComponent = diceObject.GetComponent<cubeMove>();
        if (moveComponent != null)
        {
            if (!isEnabled)
            {
                moveComponent.IsRolling = false;
                moveComponent.IsFix = true;
            }
            else
            {
                moveComponent.IsFix = false;
            }
        }
    }

    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
        // PC������ �Է� ó��
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag("Dice"))
            {
                GameObject clickedDice = hit.transform.gameObject;

                bool isClickedDiceFixed = unfixedDiceList.Contains(clickedDice);

                if (!isClickedDiceFixed)
                {
                    if (setObjects.Length > 0)
                    {
                        MoveDiceToNextSet(clickedDice);

                        // �߰� �κ�: ��� �ֻ����� isFix�� true�̸� ��� �ֻ����� RollCount�� 3���� ����
                        if (AreAllDiceFixed())
                        {
                            SetAllDiceRollCountTo3();
                        }
                    }
                }
                else
                {
                    ResetDicePosition(clickedDice);
                }
            }
        }
#elif UNITY_ANDROID || UNITY_IOS
        // ����Ͽ����� �Է� ó��
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag("Dice"))
            {
                GameObject clickedDice = hit.transform.gameObject;

                bool isClickedDiceFixed = unfixedDiceList.Contains(clickedDice);

                if (!isClickedDiceFixed)
                {
                    if (setObjects.Length > 0)
                    {
                        MoveDiceToNextSet(clickedDice);

                        // �߰� �κ�: ��� �ֻ����� isFix�� true�̸� ��� �ֻ����� RollCount�� 3���� ����
                        if (AreAllDiceFixed())
                        {
                            SetAllDiceRollCountTo3();
                        }
                    }
                }
                else
                {
                    ResetDicePosition(clickedDice);
                }
            }
        }
#endif
    }

    void ToggleRigidbody(GameObject diceObject, bool isEnabled)
    {
        Rigidbody rb = diceObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = !isEnabled;
        }
    }

    void MoveDiceToNextSet(GameObject dice)
    {
        dice.transform.position = setObjects[currentSetIndex].transform.position + Vector3.up * 0.3f;
        ToggleDiceMovement(dice, false);
        ToggleRigidbody(dice, false); // Rigidbody ��Ȱ��ȭ
        unfixedDiceList.Add(dice);
        currentSetIndex = (currentSetIndex + 1) % setObjects.Length;
    }

    void ResetDicePosition(GameObject dice)
    {
        dice.transform.position = defaultDicePosition;
        ToggleDiceMovement(dice, true);
        ToggleRigidbody(dice, true); // Rigidbody Ȱ��ȭ
        unfixedDiceList.Remove(dice);
    }

    // �߰� �κ�: ��� �ֻ����� isFix�� true���� Ȯ��
    bool AreAllDiceFixed()
    {
        foreach (var cubeMoveScript in cubeMoveScripts)
        {
            if (cubeMoveScript != null && !cubeMoveScript.IsFix)
            {
                return false;
            }
        }
        return true;
    }

    // �߰� �κ�: ��� �ֻ����� RollCount�� 3���� ����
    void SetAllDiceRollCountTo3()
    {
        foreach (var cubeMoveScript in cubeMoveScripts)
        {
            if (cubeMoveScript != null)
            {
                cubeMoveScript.RollCount = 3;
            }
        }
    }
}
