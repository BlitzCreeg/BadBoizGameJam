using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifl : MonoBehaviour
{
    public PlayerAudioController playerAudioController;

    public float damage = 30f;
    public float range = 100f;
    public float fireRate = 25f;
    public float ammoCount = 30f;

    public AudioClip[] gunshotArray;
    public AudioClip gunEmpty;
    float pitchMin, pitchMax, volumeMin, volumeMax;
    public AudioSource gunSource;
    private int clipIndex = 0;

    private float nextTimeToFire = 0f;

    public GameObject stoneImpact;
    public GameObject metalImpact;
    public GameObject woodImpact;
    public GameObject enemyImpact;
    public GameObject pipeImpact;

    public GameObject rifleDrop;

    public bool canUse = false;

    public Camera playerCam;

    public ParticleSystem muzzleFlash;

    // Update is called once per frame
    void Update()
    {
        if (canUse)
            CheckFire();
    }

    public void CheckFire()
    {
        if (Input.GetKey(KeyCode.Mouse0) && Time.time >= nextTimeToFire) //take down away for fullauto
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            if (ammoCount > 0f)
            {
                ammoCount--;
                Shoot();
                pitchMin = 0.9f;
                pitchMax = 1.2f;
                volumeMin = 0.8f;
                volumeMax = 1.4f;
                gunSource.pitch = Random.Range(pitchMin, pitchMax);
                gunSource.volume = Random.Range(volumeMin, volumeMax);
                clipIndex = RepeatCheck(clipIndex, gunshotArray.Length);
                gunSource.PlayOneShot(gunshotArray[clipIndex]);
            }
            else
            {
                gunSource.PlayOneShot(gunEmpty);
                playerAudioController.PlayRandomHandling();

                Instantiate(rifleDrop, transform.position, transform.rotation);
                canUse = false;
            }
        }
    }

    int RepeatCheck(int previousIndex, int range)
    {
        int index = Random.Range(0, range);

        while (index == previousIndex)
        {
            index = Random.Range(0, range);
        }
        return index;
    }

    public void Shoot()
    {
        muzzleFlash.Play();

        RaycastHit hit;

        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, range))
        {

            FighterHealth fighterHealth = hit.transform.GetComponent<FighterHealth>();

            if (fighterHealth != null)
            {
                fighterHealth.TakeDamage(damage);
            }

            if (hit.transform.gameObject.CompareTag("Stone"))
            {
                Instantiate(stoneImpact, hit.point, Quaternion.LookRotation(hit.normal));
            }
            else if (hit.transform.gameObject.CompareTag("Metal"))
            {
                Instantiate(metalImpact, hit.point, Quaternion.LookRotation(hit.normal));
            }
            else if (hit.transform.gameObject.CompareTag("Wood"))
            {
                Instantiate(woodImpact, hit.point, Quaternion.LookRotation(hit.normal));
            }
            else if (hit.transform.gameObject.CompareTag("Pipe"))
            {
                Instantiate(pipeImpact, hit.point, Quaternion.LookRotation(hit.normal));
            }
            else if (hit.transform.gameObject.CompareTag("Fighter") || hit.transform.gameObject.CompareTag("Protector"))
            {
                Instantiate(enemyImpact, hit.point, Quaternion.LookRotation(hit.normal));
            }
            else
                Instantiate(stoneImpact, hit.point, Quaternion.LookRotation(hit.normal));

        }
    }
}
