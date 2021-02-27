using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class ProtectorController : MonoBehaviour
{
    public Vector3 targetPosition;
    public Vector3 previousPosition;

    public bool foundPlayer;

    public NavMeshAgent protectorAI;
    public SquadBehavour squadBehavour;
    public HumanoidStateController hSC;
    
    void Start()
    {
        protectorAI = GetComponent<NavMeshAgent>();
        foundPlayer = false;
    }

    void Update()
    {
        protectorAI.SetDestination(targetPosition);

        if(previousPosition != transform.position)
        {
            hSC.isWalking = true;
        }

        if(previousPosition == transform.position)
        {
            hSC.isWalking = false;
        }

        previousPosition = transform.position;
    }
}
