using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragSelect : MonoBehaviour
{
    public Image SelectionBox;
    public RectTransform ParentRect;

    private float InitialClickX;
    private float InitialClickY;

    // Use this for initialization
    void Start ()
    {
        SelectionBox.rectTransform.anchoredPosition = new Vector2(0, 0);
        //SelectionBox.rectTransform.anchoredPosition = new Vector2(Input.mousePosition.x, (Screen.height - Input.mousePosition.y) * -1);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && ClickSpawnScript.SelectedTower == null)
        {
            InitialClickX = Input.mousePosition.x;
            InitialClickY = Input.mousePosition.y;
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(ParentRect, Input.mousePosition, GetComponentInParent<Canvas>().worldCamera, out localPoint);
            SelectionBox.rectTransform.anchoredPosition = new Vector2(localPoint.x, (Screen.height - localPoint.y) * -1);
        }
        if (Input.GetKey(KeyCode.Mouse0) && ClickSpawnScript.SelectedTower == null)
            SelectionBox.rectTransform.localScale = new Vector3((InitialClickX - Input.mousePosition.x), (InitialClickY - Input.mousePosition.y), 1);
    }
}
