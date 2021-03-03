using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPoint : MonoBehaviour
{
    public SquadBehavour squadBehavour;
    public FighterController fighterController;

    // Update is called once per frame
    void Update()
    {
        if(fighterController.canShoot)
        {
            transform.LookAt(squadBehavour.player.transform.position);
        }
    }
}
