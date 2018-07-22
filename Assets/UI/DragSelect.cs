using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragSelect : MonoBehaviour
{
    public Image SelectionBox;
    public RectTransform ParentRect;
    public Canvas Canvas;

    private float InitialClickX;
    private float InitialClickY;

    private float _prevXScale;
    private float _prevYScale;
    // Use this for initialization
    void Start ()
    {

    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && ClickSpawnScript.SelectedTower == null)
        {
            SelectionBox.gameObject.SetActive(true);
            InitialClickX = Input.mousePosition.x;
            InitialClickY = Input.mousePosition.y;
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(ParentRect, Input.mousePosition, Canvas.worldCamera, out localPoint);
            SelectionBox.rectTransform.anchoredPosition = new Vector2(localPoint.x, localPoint.y);
        }
        if (Input.GetKey(KeyCode.Mouse0) && ClickSpawnScript.SelectedTower == null)
        {
            float scaleChangeX = (InitialClickX - Input.mousePosition.x) * 0.5f;
            float scaleChangeY = (InitialClickY - Input.mousePosition.y) * 0.5f;


            SelectionBox.rectTransform.localScale = new Vector3(scaleChangeX, scaleChangeY, 1);
            SelectionBox.rectTransform.localPosition = new Vector3(SelectionBox.rectTransform.localPosition.x - (scaleChangeX - _prevXScale) / 2,
                                                                   SelectionBox.rectTransform.localPosition.y - (scaleChangeY - _prevYScale) / 2,
                                                                   SelectionBox.rectTransform.localPosition.z);

            _prevXScale = scaleChangeX;
            _prevYScale = scaleChangeY;
        }
        else
        {
            SelectionBox.gameObject.SetActive(false);
        }
    }
}
