using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPosition : MonoBehaviour
{
    public Vector3 leaderPosition;
    /*
    void FixedUpdate()
    {
        RaycastHit point;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out point, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * point.distance, Color.green);
        }

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out point, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * point.distance, Color.green);
        }

        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * 1, Color.blue);
            Debug.Log("Did not Hit");
        }
    }
    */
    void LateUpdate()
    {
        leaderPosition = gameObject.transform.position;
    }

    void FixedUpdate()
    {
        RaycastHit point;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out point, 1f))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * point.distance, Color.green);

            // If true | Raycast is visible/green
            // Destination of other fighter = position.transform.x + spacing
        }

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out point, 1f))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * point.distance, Color.green);

            // If true | Raycast is visible/green
            // Destination of other fighter = position.transform.x - spacing
        }
    }
}
