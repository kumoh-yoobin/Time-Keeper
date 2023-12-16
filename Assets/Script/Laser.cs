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

    // 레이저의 방향을 설정하는 메서드
    public void SetDirection(Vector2 direction)
    {
        laserDirection = direction;
    }

    // 레이저에 맞은 오브젝트를 설정하는 메서드
    /*public void SetHitObject(GameObject objectHit)
    {
        hitObject = objectHit;
    }*/

    // 다른 필요한 메서드들을 추가로 구현할 수 있습니다.
}
