using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class ProtectorController : MonoBehaviour
{
    public Vector3 targetPosition;
    public Vector3 previousPosition;

    public bool foundPlayer;
    public bool isMoving;

    public float defendRange;
    public float distance;

    public NavMeshAgent protectorAI;
    public SquadBehavour squadBehavour;
    public HumanoidStateController hSC;
    
    void Start()
    {
        protectorAI = GetComponent<NavMeshAgent>();

        isMoving = false;
        foundPlayer = false;
    }

    void Update()
    {
        protectorAI.SetDestination(targetPosition);

        if (previousPosition != transform.position)
        {
            hSC.isWalking = true;
            isMoving = true;
        }

        if (previousPosition == transform.position)
        {
            hSC.isWalking = false;
            isMoving = false;
        }

        PressurePlayer();

        previousPosition = transform.position;
    }

    void PressurePlayer()
    {
        if(!isMoving && foundPlayer && distance < defendRange)
        {
            hSC.isAttackingIdle = true;
            //Vector3 direction = squadBehavour.updatingPlayerPosition - transform.position;
            //transform.LookAt(new Vector3(direction.x, direction.y - 1, direction.z));

            Vector3 direction = squadBehavour.player.transform.position;
            transform.LookAt(direction);
        }

        else
        {
            hSC.isAttackingIdle = false;
        }
    }
    void CheckDistance()
    {
        distance = Vector3.Distance(transform.position, squadBehavour.PlayerPosition);
    }
}
