
using UnityEngine;

public class Guide : MonoBehaviour
{
    public GameObject imageObject;
    bool isActive = false;
    void Start()
    {
        imageObject.SetActive(isActive);
    }

    // 이미지를 활성화 또는 비활성화하는 함수 - 각각이 두번 눌러야 반응 함 수정 필요
    public void SetImageActive()
    {
        if (imageObject != null)
        {
            isActive = !isActive;
            imageObject.SetActive(isActive);
        }
    }
}
