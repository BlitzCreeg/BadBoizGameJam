using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFGPickUp : MonoBehaviour
{

    public GameObject rifle;

    private void Start()
    {
        rifle = GameObject.Find("BFG");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            rifle.GetComponentInChildren<Rifl>().ammoCount = 30f;
            rifle.gameObject.GetComponentInChildren<Rifl>().canUse = true;

            Destroy(this.gameObject, 1f);
        }
    }

}
