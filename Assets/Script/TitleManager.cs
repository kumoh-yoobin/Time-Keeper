using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject TitleCanvas;

    [SerializeField]
    private GameObject SelectCanvas;

    private bool iscanvas = true;
    public void SetCanvas()
    {
        TitleCanvas.SetActive(!iscanvas);
        SelectCanvas.SetActive(iscanvas);
        iscanvas = !iscanvas;
    }
}
