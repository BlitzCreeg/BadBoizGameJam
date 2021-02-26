using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastLook : MonoBehaviour
{
    FighterController fighterController;
    ProtectorController protectorController;

    public int counter;

    void Start()
    {
        counter = 0;
        fighterController = null;
        protectorController = null;

        if (tag == "Fighter")
        {
            fighterController = GetComponent<FighterController>();
        }

        else if (tag == "Protector")
        {
            protectorController = GetComponent<ProtectorController>();
        }
    }

    void FixedUpdate()
    {
        // For when player looks at enemy
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

            if (hit.transform.tag == "Player")
            {
                Debug.Log("Found Player");
                
                if (tag == "Fighter")
                {
                    fighterController.squadBehavour.followPlayerPos = true;
                }

                else if (tag == "Protector")
                {
                    protectorController.squadBehavour.followPlayerPos = true;
                } 
            }
        }
        
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 100, Color.white);
            if (tag == "Fighter")
            {
                if (fighterController.squadBehavour.followPlayerPos == true)
                {
                    counter++;
                    
                    if (counter > 400)
                    {
                        Debug.Log("Stop Looking For Player");
                        fighterController.squadBehavour.followPlayerPos = false;
                        counter = 0;
                    }
                }
            }

            else if (tag == "Protector")
            {
                if (protectorController.squadBehavour.followPlayerPos == true)
                {
                    counter++;

                    if (counter > 400)
                    {
                        Debug.Log("Stop Looking For Player");
                        protectorController.squadBehavour.followPlayerPos = false;
                        counter = 0;
                    }
                }
            } 

            Debug.Log("Did not Hit");
        }
    }
}
