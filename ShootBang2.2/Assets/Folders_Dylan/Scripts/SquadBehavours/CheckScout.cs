using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckScout : MonoBehaviour
{
    SquadBehavour squadBehavour;
    Vector3 playerLastKnownPosition;

    void Start()
    {
        squadBehavour = GetComponent<SquadBehavour>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Scout" && collider.GetComponent<ScoutController>().isScouting == false)
        {
            ScoutController sc = collider.GetComponent<ScoutController>();
            
            // Set value of where player was found
            playerLastKnownPosition = sc.playerFoundPosition;
            //Debug.Log(playerLastKnownPosition);

            sc.isScouting = true;
            //playerLastKnownPosition = sc.playerFoundPosition;
            squadBehavour.FoundPlayer(playerLastKnownPosition);
        }
    }
}
