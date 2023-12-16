using UnityEngine;

public class Camera_Change : MonoBehaviour
{
    public Camera mainCamera;
    public Camera otherCamera;
    public Canvas ruleCanvas;
    public Canvas buttonCanvas;
    public Canvas toggleCanvas;

    private cubeMove[] cubeMoveScripts; // ���� ���� cubeMove ��ũ��Ʈ�� ������ �迭

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


            // Scene���� ��� cubeMove ��ũ��Ʈ�� ã�� �迭�� ����
            cubeMoveScripts = FindObjectsOfType<cubeMove>();
    }

    void Update()
    {
        // �����̵� �̺�Ʈ ����
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 dragDelta = Input.mousePosition - dragOrigin;
            float dragThreshold = 150f; // �����̵�� �Ǵ��� �ּ� �̵� �Ÿ�

            // �����̵�� �Ǵ��ϴ� ����
            if (dragDelta.magnitude > dragThreshold)
            {
                // �����̵� ���⿡ ���� ī�޶� ��ȯ
                if (dragDelta.x > 0)
                {
                    SwitchCamera();
                    ToggleCanvas();
                }

                // �����̵� ���� �� �ʱ�ȭ
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

    // Rule �������� ����ϴ� �Լ�
    void ToggleCanvas()
    {
        if (ruleCanvas != null)
        {
            ruleCanvas.enabled = !ruleCanvas.enabled;
            buttonCanvas.enabled = !buttonCanvas.enabled;
            toggleCanvas.enabled = !toggleCanvas.enabled;

            // ��� cubeMove ��ũ��Ʈ�� ã�Ƽ� Ȱ��/��Ȱ��ȭ
            foreach (cubeMove cubeMoveScript in cubeMoveScripts)
            {
                cubeMoveScript.enabled = !cubeMoveScript.enabled;
            }
        }
    }
}
