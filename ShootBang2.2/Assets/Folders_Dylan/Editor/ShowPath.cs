using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WaypointNodesController))]
public class ShowPath : Editor
{
    WaypointNodesController w;
    public Transform[] nodes;

    void OnEnable()
    {
        w = (WaypointNodesController)target;
        nodes = w.GetComponent<WaypointNodesController>().nodesReference;

        nodes = new Transform[w.transform.childCount];
        for (int i = 0; i < nodes.Length; i++)
        {
            nodes[i] = w.transform.GetChild(i);
        }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        w = (WaypointNodesController)target;
        nodes = w.GetComponent<WaypointNodesController>().nodesReference;

        nodes = new Transform[w.transform.childCount];
        for (int i = 0; i < nodes.Length; i++)
        {
            nodes[i] = w.transform.GetChild(i);
        }
    }

    void OnSceneGUI()
    {
        Handles.color = Color.red;
        for (int i = 0; i < nodes.Length; i++)
        {
            if (i + 1 == nodes.Length)
            {
                Handles.color = Color.blue;
                Handles.DrawLine(nodes[0].transform.position, nodes[i].transform.position);
                return;
            }

            Handles.DrawLine(nodes[i].transform.position, nodes[i + 1].transform.position);
        }
    }
}
