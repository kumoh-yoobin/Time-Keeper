using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public GameObject hitObject;
    private Vector2 laserDirection;
    
   

    public GameObject GetHitObject()
    {
        return hitObject;
    }

    /*public Vector2 GetDirection()
    {
        return laserDirection;
    }*/

    // �������� ������ �����ϴ� �޼���
    public void SetDirection(Vector2 direction)
    {
        laserDirection = direction;
    }

    // �������� ���� ������Ʈ�� �����ϴ� �޼���
    /*public void SetHitObject(GameObject objectHit)
    {
        hitObject = objectHit;
    }*/

    // �ٸ� �ʿ��� �޼������ �߰��� ������ �� �ֽ��ϴ�.
}
