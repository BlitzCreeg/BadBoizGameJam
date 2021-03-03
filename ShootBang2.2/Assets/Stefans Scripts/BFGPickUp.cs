using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFGPickUp : MonoBehaviour
{
    public PlayerAudioController playerAudioController;

    public GameObject rifle;

    private void Start()
    {
        rifle = GameObject.FindGameObjectWithTag("BFG");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerAudioController.PlayRandomHandling();
            rifle.GetComponent<Rifl>().ammoCount = 30f;
            rifle.gameObject.GetComponent<Rifl>().canUse = true;

            Destroy(this.gameObject, 0.2f);
        }
    }

}
