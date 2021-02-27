using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class FighterController : MonoBehaviour
{
    public NavMeshAgent fighterAI;
    public Vector3 targetPosition;
    public bool canMove;
    public bool foundPlayer;

    public SquadBehavour squadBehavour;

    void Start()
    {
        fighterAI = GetComponent<NavMeshAgent>();
        foundPlayer = false;
        canMove = false;
    }

    void Update()
    {
        //if (canMove && foundPlayer)
        //{
            //transform.LookAt(targetPosition);
            //fighterAI.SetDestination(targetPosition);
        //}

        fighterAI.SetDestination(targetPosition);
    }
}
