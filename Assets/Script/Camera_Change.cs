using UnityEngine;

public class Camera_Change : MonoBehaviour
{
    public Camera mainCamera;
    public Camera otherCamera;
    public Canvas ruleCanvas;
    public Canvas buttonCanvas;
    public Canvas toggleCanvas;

    private cubeMove[] cubeMoveScripts; // 여러 개의 cubeMove 스크립트를 저장할 배열

    private Camera activeCamera;
    private Vector3 dragOrigin;

    void Start()
    {
        mainCamera = Camera.main;

        if (mainCamera != null)
        {
            mainCamera.enabled = true;
            activeCamera = mainCamera;
        }
        else
        {
            Debug.LogError("Main Camera not found!");
        }

        if (otherCamera != null)
        {
            otherCamera.enabled = false;
        }
        else
        {
            Debug.LogError("Other Camera not found!");
        }

        if (ruleCanvas != null)
        {
            ruleCanvas.enabled = false;
        }
        else
        {
            Debug.LogError("Rule Canvas not found!");
        }

        if (buttonCanvas != null)
        {
            buttonCanvas.enabled = true;
        }

        if (toggleCanvas != null)
        {
            toggleCanvas.enabled = false;
        }


            // Scene에서 모든 cubeMove 스크립트를 찾아 배열에 저장
            cubeMoveScripts = FindObjectsOfType<cubeMove>();
    }

    void Update()
    {
        // 슬라이드 이벤트 감지
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 dragDelta = Input.mousePosition - dragOrigin;
            float dragThreshold = 150f; // 슬라이드로 판단할 최소 이동 거리

            // 슬라이드로 판단하는 조건
            if (dragDelta.magnitude > dragThreshold)
            {
                // 슬라이드 방향에 따라 카메라 전환
                if (dragDelta.x > 0)
                {
                    SwitchCamera();
                    ToggleCanvas();
                }

                // 슬라이드 종료 후 초기화
                dragOrigin = Input.mousePosition;
            }
        }
    }

    void SwitchCamera()
    {
        if (activeCamera != null)
        {
            activeCamera.enabled = false;

            if (activeCamera == mainCamera)
            {
                otherCamera.enabled = true;
                activeCamera = otherCamera;
            }
            else
            {
                mainCamera.enabled = true;
                activeCamera = mainCamera;
            }
        }
    }

    // Rule 컨버스를 토글하는 함수
    void ToggleCanvas()
    {
        if (ruleCanvas != null)
        {
            ruleCanvas.enabled = !ruleCanvas.enabled;
            buttonCanvas.enabled = !buttonCanvas.enabled;
            toggleCanvas.enabled = !toggleCanvas.enabled;

            // 모든 cubeMove 스크립트를 찾아서 활성/비활성화
            foreach (cubeMove cubeMoveScript in cubeMoveScripts)
            {
                cubeMoveScript.enabled = !cubeMoveScript.enabled;
            }
        }
    }
}
