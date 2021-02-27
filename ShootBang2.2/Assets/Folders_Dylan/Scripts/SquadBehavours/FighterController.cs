using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class FighterController : MonoBehaviour
{
    public Vector3 targetPosition;
    public Vector3 previousPosition;

    public bool canMove;
    public bool foundPlayer;
    public bool isMoving;

    public NavMeshAgent fighterAI;
    public SquadBehavour squadBehavour;
    public HumanoidStateController hSC;

    void Start()
    {
        fighterAI = GetComponent<NavMeshAgent>();
        foundPlayer = false;
        canMove = false;
        isMoving = false;
        previousPosition = gameObject.transform.position;
    }

    void Update()
    {
        fighterAI.SetDestination(targetPosition);

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
