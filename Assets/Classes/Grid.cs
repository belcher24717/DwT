using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    [SerializeField]
    private float size = 1f;

    public Vector3 GetNearestPointOnGrid (Vector3 buildPosition)
    {
        int xCount = Mathf.RoundToInt(buildPosition.x / size);
        int yCount = Mathf.RoundToInt(buildPosition.y / size);
        int zCount = Mathf.RoundToInt(buildPosition.z / size);

        return new Vector3((float)xCount, (float)yCount, (float)zCount);
    }

}
