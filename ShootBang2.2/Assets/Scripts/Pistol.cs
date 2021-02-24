using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    public float damage = 10f;
    public float range = 50f;
    public float fireRate = 15f;

    private float nextTimeToFire = 0f;

    public float charge = 50f;

    public bool isRunning = false;

    public Camera playerCam;

    // Update is called once per frame
    void Update()
    {
        CheckFire();
    }

    public void CheckFire()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire) //take down away for fullauto
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            if (charge >= 10f)
                Shoot();
        }
    }

    public void Shoot()
    {
        charge -= 10f;

        RaycastHit hit;

        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            FighterHealth fighterHealth = hit.transform.GetComponent<FighterHealth>();

            if (fighterHealth != null)
            {
                fighterHealth.TakeDamage(damage);
            }

        }

        if (!isRunning)
            StartCoroutine(rechargeWait());
    }

    public void Recharge()
    {
        if (charge > 50f)
            charge = 50f;
        else if (charge < 0f)
            charge = 0f;
        else
            charge += 2f;
    }

    IEnumerator rechargeWait()
    {
        isRunning = true;
        yield return new WaitForSeconds(3);

        while (charge < 50f)
        {
            yield return new WaitForSeconds(1f);
            Recharge();
        }
        isRunning = false;
    }
}
