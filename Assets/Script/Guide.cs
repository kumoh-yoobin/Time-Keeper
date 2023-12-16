
using UnityEngine;

public class Guide : MonoBehaviour
{
    public GameObject imageObject;
    bool isActive = false;
    void Start()
    {
        imageObject.SetActive(isActive);
    }

    // �̹����� Ȱ��ȭ �Ǵ� ��Ȱ��ȭ�ϴ� �Լ� - ������ �ι� ������ ���� �� ���� �ʿ�
    public void SetImageActive()
    {
        if (imageObject != null)
        {
            isActive = !isActive;
            imageObject.SetActive(isActive);
        }
    }
}
