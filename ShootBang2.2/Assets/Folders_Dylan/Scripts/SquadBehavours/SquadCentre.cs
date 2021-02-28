using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadCentre : MonoBehaviour
{
    SquadBehavour squadBehavour;

    GameObject[] fighterGameObjects;
    GameObject[] protectorGameObjects;

    public Vector3 averageSquadPosition;

    int averageDivider;

    void Start()
    {
        squadBehavour = GetComponent<SquadBehavour>();

        fighterGameObjects = squadBehavour.fighters;
        protectorGameObjects = squadBehavour.protectors;

        //Debug.Log(fighterGameObjects.Length);
        //Debug.Log(protectorGameObjects.Length);

        averageDivider = fighterGameObjects.Length + protectorGameObjects.Length;
    }

    void LateUpdate()
    {
        //averageSquadPosition = new Vector3(1,0,0) + new Vector3(2,0,0) + new Vector3(3,0,0);
        averageSquadPosition = fighterGameObjects[0].transform.position + fighterGameObjects[1].transform.position + protectorGameObjects[0].transform.position;
        gameObject.transform.position = averageSquadPosition / averageDivider;
    }
}
