using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFGPickUp : MonoBehaviour
{
    public PlayerAudioController playerAudioController;

    public GameObject rifle;

    private void Start()
    {
        rifle = GameObject.Find("BFG");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerAudioController.PlayRandomHandling();
            rifle.GetComponentInChildren<Rifl>().ammoCount = 30;
            rifle.gameObject.GetComponentInChildren<Rifl>().canUse = true;

            Destroy(this.gameObject, 0.2f);
        }
    }

}
