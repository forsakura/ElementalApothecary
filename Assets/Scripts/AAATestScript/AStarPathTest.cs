using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AStarPathFinding;

public class AStarPathTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AStarManager.Instance.InitMap(-50, 50, -50, 50);
    }

    // Update is called once per frame
    void Update()
    {
        AStarManager.Instance.GetPath(new Vector2Int(-10, -10), new Vector2Int(10, 10));
    }
}
