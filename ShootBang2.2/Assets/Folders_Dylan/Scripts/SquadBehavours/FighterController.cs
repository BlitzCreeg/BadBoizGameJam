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
    public bool canShoot;

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
        canShoot = false;

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
            //Vector3 direction = transform.position - squadBehavour.updatingPlayerPosition;
            //transform.LookAt(new Vector3(direction.x, direction.y, direction.z));
            
            Vector3 direction = squadBehavour.player.transform.position;
            transform.LookAt(direction);

            if (canShoot)
            {
                hSC.isAttackingIdle = true;
            }

            else
            {
                hSC.isAttackingIdle = false;
            }
        }

        else
        {
            hSC.isAttackingIdle = false;
        }
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);

            if (hit.transform.tag == "Player")
            {
                canShoot = true;
                Debug.Log("Can Shoot");
            }

            else
            {
                canShoot = false;
            }
        }
        
        else
        {
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), transform.TransformDirection(Vector3.forward) * 100, Color.black);
            canShoot = false;
            Debug.Log("Cannot Shoot");
        }
    }
}
