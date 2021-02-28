using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SquadCreator))]
public class AddSquad : Editor
{
    SquadCreator squadCreator;

    float space = 10f;

    Vector3 origin = new Vector3(0, 0, 0);

    GameObject spawnSquad;
    GameObject spawnScout;
    Transform[] childObjects;

    List<Transform> transforms = new List<Transform>();

    void OnEnable()
    {
        squadCreator = (SquadCreator)target;
        spawnSquad = squadCreator.squadPrefab;
        spawnScout = squadCreator.scoutPrefab;
    }

    void AddGameObject(GameObject gameObject)
    {
        GameObject spawn = Instantiate(gameObject, origin, Quaternion.identity);
            
        childObjects = new Transform[spawn.transform.childCount];
        for (int i = 0; i < childObjects.Length; i++)
        {
            childObjects[i] = spawn.transform.GetChild(i);
            transforms.Add(childObjects[i]);
        }

        foreach (Transform t in transforms)
        {
            t.transform.parent = null;
        }

        DestroyImmediate(spawn);
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUILayout.Space(space);

        if(GUILayout.Button("Add Squad"))
        {
            AddGameObject(spawnSquad);
        }

        GUILayout.Space(space);

        if(GUILayout.Button("Add Scout"))
        {
            AddGameObject(spawnScout);
        }
    }
}
