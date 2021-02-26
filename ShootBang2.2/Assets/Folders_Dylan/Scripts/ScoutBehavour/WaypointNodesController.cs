using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNodesController : MonoBehaviour {

    public static Transform[] waypointNodes;

    void Awake()
    {
        waypointNodes = new Transform[transform.childCount];
        for (int i = 0; i < waypointNodes.Length; i++)
        {
            waypointNodes[i] = transform.GetChild(i);
        }
    }
}