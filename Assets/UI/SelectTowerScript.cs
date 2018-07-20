using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectTowerScript : ClickSpawnScript
{
    /// <summary>
    /// The Tower that will spawn on click
    /// </summary>
    public Tower Tower;
    /// <summary>
    /// The Tower to use as the Cursor for building
    /// </summary>
    public TowerCursorScript CursorTower;
    public TextMeshProUGUI uiText;

    private bool _selected;
    public bool Selected
    {
        get
        {
            return _selected;
        }
        set
        {
            _selected = value;
            gameObject.GetComponent<Renderer>().material.color = _selected ?  Color.red : Color.grey;
        }
    }

    void Awake()
    {

    }

    // Use this for initialization
    void Start()
    {
        Selected = false;
        SetMesh();
        uiText.SetText($"{Tower.TowerName}\n{Tower.BaseCost} TB");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetMesh()
    {
        MeshFilter[] meshes = Tower.GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combinedMeshes = new CombineInstance[meshes.Length];

        for (int i = 0; i < combinedMeshes.Length; i++)
        {
            combinedMeshes[i].mesh = meshes[i].sharedMesh;
            combinedMeshes[i].transform = meshes[i].transform.localToWorldMatrix;
        }

        gameObject.GetComponent<MeshFilter>().mesh.CombineMeshes(combinedMeshes);
    }

    private void OnMouseDown()
    {
        if (!Selected)
        {
            ResetSelection();
            Selected = true;
            ChangeSpawnParentLayer(2);
            Tower.DeselectTower();
        }
        else
            ResetSelection();

    }

}
