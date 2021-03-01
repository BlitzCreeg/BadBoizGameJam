﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class SquadBehavour : MonoBehaviour
{
    public Target moveScout;

    // Positioning
    public GameObject moveTarget;
    public Vector3 currentPlayerPosition;
    public Vector3 updatingPlayerPosition;
    public float stoppingDistanceProtectors = 2.5f;
    public float stoppingDistanceFighters = 5.5f;

    // GameObject/Object References
    public GameObject[] fighters;
    public GameObject[] protectors;
    public GameObject player;

    // Max/Min Distances For Enemies
    public float minFighterDistance;
    public float maxFighterDistance;
    public float maxProtectorDistance;
    public float maxSquadDistance;

    // Booleans
    public bool foundPlayer;
    public bool followPlayerPos;

    public void Start()
    {
        currentPlayerPosition = GameObject.Find("Player").transform.position;
        player = GameObject.FindGameObjectWithTag("PlayerPosition");

        foundPlayer = false;
        followPlayerPos = false;
        GetSquadCharacters();
        moveScout = moveTarget.GetComponent<Target>();
    }

    public void AgressivePositions()
    {
        
    }

    public void DefensivePositions()
    {
        
    }

    public void FollowPlayer()
    {
        foreach (GameObject f in fighters)
        {
            f.GetComponent<FighterController>().targetPosition = updatingPlayerPosition;
            f.GetComponent<FighterController>().canMove = true;
            f.GetComponent<FighterController>().foundPlayer = true;
            Debug.Log("Found Player");
        }

        foreach (GameObject p in protectors)
        {
            p.GetComponent<ProtectorController>().targetPosition = updatingPlayerPosition;
            p.GetComponent<ProtectorController>().foundPlayer = true;
        }
    }

    // So Enemies Know Which Squad They Belong To
    void GetSquadCharacters()
    {
        foreach (GameObject p in protectors)
        {
            p.GetComponent<ProtectorController>().squadBehavour = this;
        }

        foreach (GameObject f in fighters)
        {
            f.GetComponent<FighterController>().squadBehavour = this;
        }
    }

    // When player is spotted they should look at the player
    void LookAtPlayer()
    {
        foreach (GameObject p in protectors)
        {
            p.GetComponent<ProtectorController>().targetPosition = PlayerPosition;
        }

        foreach (GameObject f in fighters)
        {
            f.GetComponent<FighterController>().targetPosition = PlayerPosition;
        }
    }

    void SetFormationProtectors(float distance)
    {
        foreach (GameObject p in protectors)
        {
            p.GetComponent<NavMeshAgent>().stoppingDistance = distance;
        }
    }

    void SetFormationFighters(float distance)
    {
        foreach (GameObject f in fighters)
        {
            f.GetComponent<NavMeshAgent>().stoppingDistance = distance;
        }
    }

    // Protector first | Fighters second
    public void OneTwoFormation()
    {
        // Look at player
        LookAtPlayer();

        // Formation
        SetFormationProtectors(StoppingDistanceProtectors);
        SetFormationFighters(StoppingDistanceFighters);

        float distBetweenProtectorAndPlayer = Vector3.Distance(protectors[0].transform.position, PlayerPosition);

        // Testing
        //Debug.Log(distBetweenProtectorAndPlayer);

        FighterDistance();
        ProtectorDistance();
        EnemyDistances();
    }

    public void FoundPlayer(Vector3 playerLastPosition)
    {
        Debug.Log("Found Player");

        foundPlayer = true;
        currentPlayerPosition = playerLastPosition;

        //Debug.Log("current Player Position = " + currentPlayerPosition + "PlayerPosition Get Method = " + PlayerPosition);

        foreach (GameObject f in fighters)
        {
            f.GetComponent<FighterController>().foundPlayer = true;
            f.GetComponent<FighterController>().targetPosition = PlayerPosition;
        }

        foreach (GameObject p in protectors)
        {
            p.GetComponent<ProtectorController>().foundPlayer = true;
            p.GetComponent<ProtectorController>().targetPosition = PlayerPosition;
        }
    }

    void FighterDistance()
    {
        if (foundPlayer)
        {
            float distBetweenFighters = Vector3.Distance(fighters[0].transform.position, fighters[1].transform.position);

            // Too close
            if (distBetweenFighters < minFighterDistance)
            {
                fighters[0].GetComponent<FighterController>().canMove = false;
                //Debug.Log("Too Close");
            }

            // Too far
            else if (distBetweenFighters > maxFighterDistance)
            {
                fighters[0].GetComponent<FighterController>().canMove = true;
                //Debug.Log("Too Far");
            }

            // Fighter distances
            float distFighterOneToPlayer = Vector3.Distance(fighters[0].transform.position, PlayerPosition);
            float distFighterTwoToPlayer = Vector3.Distance(fighters[1].transform.position, PlayerPosition);

            // Protector distance
            float distProtectorToPlayer = Vector3.Distance(protectors[0].transform.position, PlayerPosition);

            // If either of the fighters are closer to the player than the protectors
            if (distFighterOneToPlayer < distProtectorToPlayer || distFighterTwoToPlayer < distProtectorToPlayer)
            {
                foreach (GameObject f in fighters)
                {
                    f.GetComponent<FighterController>().canMove = false;
                    //Debug.Log("Fighters closer to players");
                }
            }

            else
            {
                foreach (GameObject f in fighters)
                {
                    f.GetComponent<FighterController>().canMove = true;
                    //Debug.Log("Fighters to far from players");
                }
            }
        }
    }

    // If protector is to far from the player it cannot protect the fighters so fighters wait until the protector is closer
    void ProtectorDistance()
    {
        float distBetweenProtectorAndPlayer = Vector3.Distance(protectors[0].transform.position, PlayerPosition);

        // Protector is too far to protect
        if (distBetweenProtectorAndPlayer > maxProtectorDistance)
        {
            foreach (GameObject f in fighters)
            {
                f.GetComponent<FighterController>().canMove = false;
                //followPlayerPos = false;
                //Debug.Log("Dont Move");
            }
        }

        else
        {
            foreach (GameObject f in fighters)
            {
                f.GetComponent<FighterController>().canMove = true;
                //Debug.Log("Move");
            }
        }
    }

    void EnemyDistances()
    {
        float distBetweenEnemies = Vector3.Distance(protectors[0].transform.position, fighters[0].transform.position);

        // Squad is too far apart
        if (distBetweenEnemies >= maxSquadDistance)
        {
            foreach (GameObject f in fighters)
            {
                f.GetComponent<FighterController>().canMove = true;
                //Debug.Log("Enemy Distances");
            }
        }

        else
        {
            foreach (GameObject f in fighters)
            {
                f.GetComponent<FighterController>().canMove = false;
                //Debug.Log("Enemy Distances Dont Move");
            }
        }
    }

    void Update()
    {
        UpdatePlayerPosition();

        OneTwoFormation();

        if (followPlayerPos)
        {
           FollowPlayer();
        }

        if (!foundPlayer && !followPlayerPos)
        {
            moveScout.canMove = true;
            //Debug.Log("Move Fighter");

            foreach (GameObject f in fighters)
            {
                f.GetComponent<FighterController>().targetPosition = MoveTargetPosition;
                //Debug.Log(MoveTargetPosition);
            }

            foreach (GameObject p in protectors)
            {
                p.GetComponent<ProtectorController>().targetPosition = MoveTargetPosition;
            }
        }
    }

    // Update player position
    void UpdatePlayerPosition()
    {
        updatingPlayerPosition = GameObject.Find("Player").transform.position;
    }

    // Get player position | Stopping distances
    public Vector3 PlayerPosition
    {
        get
        {
            return currentPlayerPosition;
        }
    }

    public Vector3 MoveTargetPosition
    {
        get
        {
            return moveTarget.transform.position;
        }
    }

    public float StoppingDistanceProtectors
    {
        get
        {
            return stoppingDistanceProtectors;
        }
    }

    public float StoppingDistanceFighters
    {
        get
        {
            return stoppingDistanceFighters;
        }
    }
}
