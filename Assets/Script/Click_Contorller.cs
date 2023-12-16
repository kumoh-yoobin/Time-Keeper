using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Click_Contorller : MonoBehaviour
{
    private cubeMove[] cubeMoveScripts; // 여러 개의 cubeMove 스크립트를 저장할 배열

    [SerializeField]
    private Turn_Controller Turn_ControllerScripts;
    public Canvas DoneCanvas;

    void Start()
    {
        // Scene에서 모든 cubeMove 스크립트를 찾아 배열에 저장
        cubeMoveScripts = FindObjectsOfType<cubeMove>();
        Turn_ControllerScripts = FindObjectOfType<Turn_Controller>();
    }

    public void init()
    {
        cubeMoveScripts = FindObjectsOfType<cubeMove>();
    }

    public void Shuffle_Click()
    {
        // 각각의 cubeMove 스크립트에 대해 CubemoveClick 함수 호출
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