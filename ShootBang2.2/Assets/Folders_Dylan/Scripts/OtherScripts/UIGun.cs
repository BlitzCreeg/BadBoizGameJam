using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGun : MonoBehaviour
{
    public Material gunUI;
    public int ammoCount;

    int ammoReference;

    string reference = "UI_Tile";

    void Start()
    {
        //ammoReference = GameObject.FindGameObjectWithTag("PlayerGun").GetComponent<Rifl>().ammoCount;
        ammoCount = ammoReference;
    }

    void Update()
    {

        if (ammoCount > 14)
        {
            ammoCount = 0;
        }

        gunUI.SetInt(reference, ammoCount);
    }
}
