using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridLoader : MonoBehaviour
{
    public string grid_id;
    public Transform parent_transform;
    public RawImage grid_img;

    public void removeTree()
    {
        var child = parent_transform.Find(grid_id);
        Destroy(child.gameObject);
    }
}
