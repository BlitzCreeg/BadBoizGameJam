using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScout : MonoBehaviour
{
    private Transform target;
    private int currentWaypoint = 0;
    public float speed = 10f;
    public bool canMove;

    void Start()
    {
        target = WaypointNodesController.waypointNodes[0];
    }
    
    void Update()
    {
        if (canMove)
        {
            Vector3 direction = target.position - transform.position;
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

            transform.LookAt(target);

            if (Vector3.Distance(transform.position, target.position) <= 0.4f)
            {
                NextWaypointNode();
            }
        }
    }

    void NextWaypointNode()
    {
        if (currentWaypoint >= WaypointNodesController.waypointNodes.Length - 1)
        {
            currentWaypoint = 0;
        }

        currentWaypoint++;
        target = WaypointNodesController.waypointNodes[currentWaypoint];
    }
}
