using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(LineRenderer))]
public class TowerCursorScript : MonoBehaviour
{
    private readonly int SEGMENTS = 50;
    private float _radius;

    public Tower Tower;
    public Material GoodPlacement;
    public Material BadPlacement;

    public bool GoodPlace { get { return GetComponent<MeshRenderer>().material != BadPlacement; } }

    LineRenderer line;

    void Awake()
    {
        line = gameObject.GetComponent<LineRenderer>();

        line.positionCount = SEGMENTS + 1;
        line.useWorldSpace = false;
        _radius = Tower.RangeUpgrades[0].Value;

        CreatePoints();
        SetMesh();
    }

    void CreatePoints()
    {
        float x;
        float y = 0.2f;
        float z;

        float angle = 20f;

        for (int i = 0; i < (SEGMENTS + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * _radius;
            z = Mathf.Cos(Mathf.Deg2Rad * angle) * _radius;

            line.SetPosition(i, new Vector3(x, y, z));

            angle += (360f / SEGMENTS);
        }
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
    public void CanPlace(bool canPlace)
    {
        if (canPlace)
        {
            gameObject.GetComponent<MeshRenderer>().material = GoodPlacement;
            line.material = GoodPlacement;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material = BadPlacement;
            line.material = BadPlacement;
        }
    }

}
