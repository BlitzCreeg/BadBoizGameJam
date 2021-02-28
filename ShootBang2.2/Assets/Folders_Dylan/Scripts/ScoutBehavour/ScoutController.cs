using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class ScoutController : MonoBehaviour
{
    public NavMeshAgent scoutAI;
    public GameObject player;
    public bool isScouting;
    GameObject[] allSquads;
    public GameObject squadClosest;
    public GameObject moveTarget;
    public Target moveScout;
    public Vector3 playerFoundPosition;

    // public static object Instance { get; internal set; }

    void Start()
    {
        GetAllSquads();
        scoutAI = GetComponent<NavMeshAgent>();
        moveScout = moveTarget.GetComponent<Target>();
        isScouting = true;

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    void Update()
    {
        Scouting();

        if (isScouting)
        {
            scoutAI.SetDestination(moveTarget.transform.position);
            moveScout.canMove = isScouting;
        }

        // Finding Closest Squad
        if (!isScouting)
        {
            squadClosest = GetClosestSquad(allSquads);

            // Scout Travels To Closest Squad
            AlertSquad();
        }
    }

    void GetAllSquads()
    {
        allSquads = GameObject.FindGameObjectsWithTag("Squad");
    }

    GameObject GetClosestSquad (GameObject[] squads)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach(GameObject potentialTarget in squads)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if(dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget.transform;
            }
        }
     
        return bestTarget.gameObject;
    }

    void Scouting()
    {
        float distBetweenPlayerScout = Vector3.Distance(gameObject.transform.position, player.transform.position);

        if (distBetweenPlayerScout < 2)
        {
            // Set Player Position To Last Known Position
            playerFoundPosition = player.transform.position;

            if (squadClosest == null)
            {
                squadClosest = GetClosestSquad(allSquads);
            }

            isScouting = false;
            GetClosestSquad(allSquads);
        }
    }

    void AlertSquad()
    {
        scoutAI.SetDestination(squadClosest.GetComponent<Transform>().transform.position);
    }
}
