using System.Collections;
using UnityEngine;

public class cubeMove : MonoBehaviour
{
    public int speed = 10;
    public bool isRolling = false; 
    public bool isFix = false;
    public int rollCount = 0;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void CubemoveClick()
    {
        if (!isRolling && rollCount < 3 && !isFix)
        {
            isRolling = true;
            RollDice();
            rollCount++;
        }
    }

    public void RollDice()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.AddForce(Vector3.up * 2000);

        Vector3 randomTorque = new Vector3(Random.Range(6000, 7000), Random.Range(6000, 7000), Random.Range(6000, 7000));
        rb.AddTorque(randomTorque);

        StartCoroutine(CheckAndUpdateRollCount());

    }

    IEnumerator CheckAndUpdateRollCount()
    {
        // 주사위가 굴러가는 동안 대기
        while (isRolling)
        {
            yield return null;
        }

        // 모든 주사위 객체들을 찾아옴
        cubeMove[] allDiceMoves = FindObjectsOfType<cubeMove>();

        // 주사위 중 하나의 롤 카운트가 3이 되면 모든 주사위의 롤 카운트를 3으로 설정
        if (RollCount == 3)
        {
            foreach (var otherDiceMove in allDiceMoves)
            {
                if (otherDiceMove != null && otherDiceMove != this)
                {
                    otherDiceMove.RollCount = 3;
                }
            }
        }
        else
        {
            // 현재 주사위보다 높은 RollCount를 가진 주사위에 대해서만 비교 및 업데이트
            foreach (var otherDiceMove in allDiceMoves)
            {
                if (otherDiceMove != null && otherDiceMove != this && otherDiceMove.RollCount > RollCount)
                {
                    Debug.Log(otherDiceMove.RollCount);
                    RollCount = otherDiceMove.RollCount;
                }
            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            isRolling = false; 
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (isRolling)
        {
            return;
        }
    }

    public int RollCount
    {
        get { return rollCount; }
        set { rollCount = value; }
    }

    public bool IsRolling
    {
        get { return isRolling; }
        set { isRolling = value; }
    }

    public bool IsFix
    {
        get { return isFix; }
        set { isFix = value; }
    }

    public void ResetRollCount()
    {
        rollCount = 0;
    }
}
