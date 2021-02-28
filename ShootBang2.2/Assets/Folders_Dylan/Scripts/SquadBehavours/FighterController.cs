using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class FighterController : MonoBehaviour
{
    public Vector3 targetPosition;
    public Vector3 previousPosition;
    private Vector3 playerPosition;

    public bool canMove;
    public bool foundPlayer;
    public bool isMoving;

    public NavMeshAgent fighterAI;
    public SquadBehavour squadBehavour;
    public HumanoidStateController hSC;

    public float attackRange;
    public float distance;

    void Start()
    {
        fighterAI = GetComponent<NavMeshAgent>();
        foundPlayer = false;
        canMove = false;
        isMoving = false;
        previousPosition = gameObject.transform.position;
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    void Update()
    {
        CheckDistance();

        if (canMove)
        {
            fighterAI.SetDestination(targetPosition);

            if(previousPosition != transform.position)
            {
                hSC.isWalking = true;
                isMoving = true;
            }
        }

        if(previousPosition == transform.position)
        {
            hSC.isWalking = false;
            isMoving = false;
        }

        AttackPlayer();
        
        previousPosition = transform.position;
    }

    void CheckDistance()
    {
        distance = Vector3.Distance(transform.position, squadBehavour.PlayerPosition);
    }

    void AttackPlayer()
    {
        if(!isMoving && foundPlayer && distance < attackRange)
        {
            hSC.isAttackingIdle = true;
            transform.LookAt(squadBehavour.updatingPlayerPosition);
        }

        else
        {
            hSC.isAttackingIdle = false;
        }
    }
}
