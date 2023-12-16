using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Click_Contorller : MonoBehaviour
{
    private cubeMove[] cubeMoveScripts; // ���� ���� cubeMove ��ũ��Ʈ�� ������ �迭

    [SerializeField]
    private Turn_Controller Turn_ControllerScripts;
    public Canvas DoneCanvas;

    void Start()
    {
        // Scene���� ��� cubeMove ��ũ��Ʈ�� ã�� �迭�� ����
        cubeMoveScripts = FindObjectsOfType<cubeMove>();
        Turn_ControllerScripts = FindObjectOfType<Turn_Controller>();
    }

    public void init()
    {
        cubeMoveScripts = FindObjectsOfType<cubeMove>();
    }

    public void Shuffle_Click()
    {
        // ������ cubeMove ��ũ��Ʈ�� ���� CubemoveClick �Լ� ȣ��
        foreach (cubeMove cubeMoveScript in cubeMoveScripts)
        {
            cubeMoveScript.CubemoveClick();
        }
    }

    public void Turn_Click()
    {
        Turn_ControllerScripts.Turn_Controll();
    }

    public void Turn_multi()
    {
        Turn_ControllerScripts.Multi_Controll();
    }


    public void Done()
    {
        DoneCanvas.enabled= false;
    }
}