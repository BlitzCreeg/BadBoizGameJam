using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        transform.Rotate(new Vector3(1 , 1, 0), Space.World);
    }
}
