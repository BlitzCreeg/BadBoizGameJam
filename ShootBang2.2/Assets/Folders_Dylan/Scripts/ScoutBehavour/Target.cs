using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Transform target;

    private int currentWaypoint = 0;
    public float speed = 10f;

    public bool canMove;
    public bool idle;

    public WaypointNodesController wNC;

    void Start()
    {
        target = wNC.nodesReference[0];
    }
    
    void Update()
    {
        if (canMove && !idle)
        {
            Vector3 direction = target.position - transform.position;
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

            transform.LookAt(target);

            if (Vector3.Distance(transform.position, target.position) <= 0.1f)
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
