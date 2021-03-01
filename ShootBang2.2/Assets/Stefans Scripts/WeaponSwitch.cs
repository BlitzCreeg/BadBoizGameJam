using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public PlayerAudioController playerAudioController;

    public int selectedWeapon = 0;
    public GameObject rifle;

    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        int previousSelectedWeapon = selectedWeapon;

        if(Input.GetAxis("Mouse ScrollWheel") > 0f && rifle.GetComponent<Rifl>().canUse)
        {
            if (selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 0;
            else
            selectedWeapon++;
            playerAudioController.PlayRandomHandling();
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f && rifle.GetComponent<Rifl>().canUse)
        {
            if (selectedWeapon <= 0)
                selectedWeapon = transform.childCount - 1;
            else
                selectedWeapon--;
            playerAudioController.PlayRandomHandling();

        }

        if (rifle.GetComponent<Rifl>().canUse == false)
        {
            selectedWeapon = 0;
        }

        if (previousSelectedWeapon != selectedWeapon)
            SelectWeapon();
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i ++;
        }
    }
}
