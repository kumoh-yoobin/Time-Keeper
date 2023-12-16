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
        // PC에서의 입력 처리
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

                        // 추가 부분: 모든 주사위의 isFix가 true이면 모든 주사위의 RollCount를 3으로 설정
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
        // 모바일에서의 입력 처리
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

                        // 추가 부분: 모든 주사위의 isFix가 true이면 모든 주사위의 RollCount를 3으로 설정
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
        ToggleRigidbody(dice, false); // Rigidbody 비활성화
        unfixedDiceList.Add(dice);
        currentSetIndex = (currentSetIndex + 1) % setObjects.Length;
    }

    void ResetDicePosition(GameObject dice)
    {
        dice.transform.position = defaultDicePosition;
        ToggleDiceMovement(dice, true);
        ToggleRigidbody(dice, true); // Rigidbody 활성화
        unfixedDiceList.Remove(dice);
    }

    // 추가 부분: 모든 주사위의 isFix가 true인지 확인
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

    // 추가 부분: 모든 주사위의 RollCount를 3으로 설정
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
