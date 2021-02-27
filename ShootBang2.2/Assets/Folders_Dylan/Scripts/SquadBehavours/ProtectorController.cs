using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class ProtectorController : MonoBehaviour
{
    public NavMeshAgent protectorAI;
    public Vector3 targetPosition;
    public bool foundPlayer;
    public SquadBehavour squadBehavour;


    void Start()
    {
        protectorAI = GetComponent<NavMeshAgent>();
        foundPlayer = false;
    }

    void Update()
    {
        /*
        if (foundPlayer)
        {
            //transform.LookAt(targetPosition);
            protectorAI.SetDestination(targetPosition);
        }

        else
        {
            protectorAI.SetDestination(targetPosition);
        }
        */

        protectorAI.SetDestination(targetPosition);
    }
}
