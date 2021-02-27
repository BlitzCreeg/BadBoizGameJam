using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGun : MonoBehaviour
{
    public Material gunUI;
    public int ammoCount;

    string reference = "UI_Tile";

    void Update()
    {
        gunUI.SetInt(reference, ammoCount);
    }
}
