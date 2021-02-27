using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class SquadBehavour : MonoBehaviour
{
    public MoveScout moveScout;

    // Positioning
    public GameObject moveTarget;
    public Vector3 currentPlayerPosition;
    public Vector3 updatingPlayerPosition;
    public float stoppingDistanceProtectors = 2.5f;
    public float stoppingDistanceFighters = 5.5f;

    // GameObject/Object References
    public GameObject[] fighters;
    public GameObject[] protectors;

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

        foundPlayer = false;
        followPlayerPos = false;
        GetSquadCharacters();
        moveScout = moveTarget.GetComponent<MoveScout>();
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
        //Debug.Log("Players last pos = " + playerLastPosition);

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
            }

            // Too far
            else if (distBetweenFighters > maxFighterDistance)
            {
                fighters[0].GetComponent<FighterController>().canMove = true;
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
                }
            }

            else
            {
                foreach (GameObject f in fighters)
                {
                    f.GetComponent<FighterController>().canMove = true;
                }
            }
        }
    }

    void ProtectorDistance()
    {
        float distBetweenProtectorAndPlayer = Vector3.Distance(protectors[0].transform.position, PlayerPosition);

        // Protector is too far to protect
        if (distBetweenProtectorAndPlayer > maxProtectorDistance)
        {
            foreach (GameObject f in fighters)
            {
                f.GetComponent<FighterController>().canMove = false;
            }
        }

        else
        {
            foreach (GameObject f in fighters)
            {
                f.GetComponent<FighterController>().canMove = true;
            }
        }
    }

    void EnemyDistances()
    {
        float distBetweenEnemies = Vector3.Distance(protectors[0].transform.position, fighters[0].transform.position);

        // Squad is too far apart
        if (distBetweenEnemies > maxSquadDistance)
        {
            foreach (GameObject f in fighters)
            {
                f.GetComponent<FighterController>().canMove = true;
            }
        }

        else
        {
            foreach (GameObject f in fighters)
            {
                f.GetComponent<FighterController>().canMove = false;
            }
        }
    }

    void Update()
    {
        OneTwoFormation();

        if (followPlayerPos)
        {
           FollowPlayer();
        }

        if (!foundPlayer && !followPlayerPos)
        {
            moveScout.canMove = true;

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

    void LateUpdate()
    {
       UpdatePlayerPosition();
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
