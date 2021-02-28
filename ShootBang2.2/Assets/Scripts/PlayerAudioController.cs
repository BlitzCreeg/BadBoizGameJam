using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    public AudioClip[] concreteCrouchArray;
    public AudioClip[] metalCrouchArray;
    public AudioClip[] concreteWalkArray;
    public AudioClip[] metalWalkArray;
    public AudioClip[] concreteSprintArray;
    public AudioClip[] metalSprintArray;

    public AudioClip[] pistolHitArray;
    public AudioClip[] pistolChargeArray;
    public AudioClip pistolShot;
    public AudioClip pistolChirp;
    public AudioClip pistolChirp2;
    public AudioClip pistolWait;

    public AudioClip[] bulletHitArray;
    public AudioClip[] concreteMissArray;

    public AudioClip[] handlingArray;

    public AudioClip jump;

    public AudioSource playerSource;
    public AudioSource pistolSource;
    public AudioSource pistolWSSource;
    public AudioSource handlingSource;

    public PlayerMovement playerMovement;
    public Pistol pistol;

    public string material;
    int lastBeep;
    float pitchMin, pitchMax, volumeMin, volumeMax;
    private int clipIndex = 0;
    int i;
    bool waiting;
    bool falling = false;
    float velocity = 0.0f;

    private void Start()
    {
        material = "concrete";
    }

    void PlayRandomConcreteCrouch()
    {
        pitchMin = 0.9f;
        pitchMax = 1.1f;
        volumeMin = 0.6f;
        volumeMax = .8f;
        playerSource.pitch = Random.Range(pitchMin, pitchMax);
        playerSource.volume = Random.Range(volumeMin, volumeMax);
        clipIndex = RepeatCheck(clipIndex, concreteCrouchArray.Length);
        playerSource.PlayOneShot(concreteCrouchArray[clipIndex]);
    }
    void PlayRandomMetalCrouch()
    {
        pitchMin = 0.9f;
        pitchMax = 1.1f;
        volumeMin = 0.6f;
        volumeMax = .8f;
        playerSource.pitch = Random.Range(pitchMin, pitchMax);
        playerSource.volume = Random.Range(volumeMin, volumeMax);
        clipIndex = RepeatCheck(clipIndex, metalCrouchArray.Length);
        playerSource.PlayOneShot(metalCrouchArray[clipIndex]);
    }
    void PlayRandomConcreteWalk()
    {
        pitchMin = 0.8f;
        pitchMax = 1f;
        volumeMin = 0.25f;
        volumeMax = 0.5f;
        playerSource.pitch = Random.Range(pitchMin, pitchMax);
        playerSource.volume = Random.Range(volumeMin, volumeMax);
        clipIndex = RepeatCheck(clipIndex, concreteWalkArray.Length);
        playerSource.PlayOneShot(concreteWalkArray[clipIndex]);
    }
    void PlayRandomMetalWalk()
    {
        pitchMin = 0.8f;
        pitchMax = 1f;
        volumeMin = 0.25f;
        volumeMax = 0.5f;
        playerSource.pitch = Random.Range(pitchMin, pitchMax);
        playerSource.volume = Random.Range(volumeMin, volumeMax);
        clipIndex = RepeatCheck(clipIndex, metalWalkArray.Length);
        playerSource.PlayOneShot(metalWalkArray[clipIndex]);
    }
    void PlayRandomConcreteSprint()
    {
        pitchMin = 0.9f;
        pitchMax = 1.2f;
        volumeMin = 0.8f;
        volumeMax = 1.4f;
        playerSource.pitch = Random.Range(pitchMin, pitchMax);
        playerSource.volume = Random.Range(volumeMin, volumeMax);
        clipIndex = RepeatCheck(clipIndex, concreteSprintArray.Length);
        playerSource.PlayOneShot(concreteSprintArray[clipIndex]);
    }
    void PlayRandomMetalSprint()
    {
        pitchMin = 0.9f;
        pitchMax = 1.2f;
        volumeMin = 0.8f;
        volumeMax = 1.4f;
        playerSource.pitch = Random.Range(pitchMin, pitchMax);
        playerSource.volume = Random.Range(volumeMin, volumeMax);
        clipIndex = RepeatCheck(clipIndex, metalSprintArray.Length);
        playerSource.PlayOneShot(metalSprintArray[clipIndex]);
    }
    void PlayRandomHandling()
    {
        pitchMin = 0.8f;
        pitchMax = 1.2f;
        volumeMin = 0.8f;
        volumeMax = 1.2f;
        handlingSource.pitch = Random.Range(pitchMin, pitchMax);
        handlingSource.volume = Random.Range(volumeMin, volumeMax);
        clipIndex = RepeatCheck(clipIndex, handlingArray.Length);
        handlingSource.PlayOneShot(handlingArray[clipIndex]);
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

    public void ChargeBeep()
    {
        if (pistol.charge < 10)
        {
            pistolSource.volume = 0.005f;
            pistolSource.PlayOneShot(pistolChargeArray[0]);
            lastBeep = 0;
        }
        else if (pistol.charge >= 10 && pistol.charge < 20)
        {
            if (lastBeep == 0)
            {
                pistolSource.volume = 0.02f;
            }
            else
            {
                pistolSource.volume = 0.005f;
            }
            pistolSource.PlayOneShot(pistolChargeArray[1]);
            lastBeep = 1;
        }
        else if (pistol.charge >= 20 && pistol.charge < 30)
        {
            if (lastBeep == 1)
            {
                pistolSource.volume = 0.02f;
            }
            else
            {
                pistolSource.volume = 0.005f;
            }
            pistolSource.PlayOneShot(pistolChargeArray[2]);
            lastBeep = 2;
        }
        else if (pistol.charge >= 30 && pistol.charge < 40)
        {
            if (lastBeep == 2)
            {
                pistolSource.volume = 0.02f;
            }
            else
            {
                pistolSource.volume = 0.005f;
            }
            pistolSource.PlayOneShot(pistolChargeArray[3]);
            lastBeep = 3;
        }
        else if (pistol.charge >= 40 && pistol.charge < 50)
        {
            if (lastBeep == 3)
            {
                pistolSource.volume = 0.02f;
            }
            else
            {
                pistolSource.volume = 0.005f;
            }
            pistolSource.PlayOneShot(pistolChargeArray[4]);
            lastBeep = 4;
        }
        else if (pistol.charge == 50)
        {
            if (lastBeep == 4)
            {
                pistolSource.volume = 0.02f;
                pistolSource.PlayOneShot(pistolChirp);
            }
            else
            {
                pistolSource.volume = 0.005f;
            }
            pistolSource.PlayOneShot(pistolChargeArray[5]);
            lastBeep = 5;
        }
    }
    public void PistolWait()
    {
        if (waiting == true)
        {
            pistolWSSource.Stop();
        }
        waiting = true;
        pistolWSSource.pitch = 1.0f;
        pistolWSSource.volume = 0.25f;
        pistolWSSource.PlayOneShot(pistolWait);
    }

    public void PistolShot()
    {
        if (pistol.charge <10)
        {
            pistolWSSource.volume = 0.2f;
            pistolWSSource.pitch = 1.0f;
            pistolWSSource.PlayOneShot(pistolChirp2);

        }
        else
        {
            pitchMin = 0.9f;
            pitchMax = 1.2f;
            volumeMin = 0.6f;
            volumeMax = 0.9f;
            pistolWSSource.pitch = Random.Range(pitchMin, pitchMax);
            pistolWSSource.volume = Random.Range(volumeMin, volumeMax);
            pistolWSSource.PlayOneShot(pistolShot);
            waiting = false;
        }
    }

    void FixedUpdate()
    {

        if (playerMovement.isJumping == true)
        {
            print("ganabbabta");
            pitchMin = 0.8f;
            pitchMax = 1.2f;
            volumeMin = 0.2f;
            volumeMax = 0.5f;
            playerSource.pitch = Random.Range(pitchMin, pitchMax);
            playerSource.volume = Random.Range(volumeMin, volumeMax);
            playerSource.PlayOneShot(jump);
        }

        if (playerMovement.isWalking == true)
        {
            if (i >= 35)
            {
                if (material == "concrete")
                {
                    PlayRandomConcreteWalk();
                }
                else if (material == "metal")
                {
                    PlayRandomMetalWalk();
                }
                i = 0;
            }
            else
            {
                i = i + 1;
            }
        }
        else if (playerMovement.isCrouchWalking == true)
        {
            if (i >= 50)
            {
                if (material == "concrete")
                {
                    PlayRandomConcreteCrouch();
                }
                else if (material == "metal")
                {
                    PlayRandomMetalCrouch();
                }
                i = 0;

            }
            else
            {
                i = i + 1;
            }

        }
        else if (playerMovement.isSprinting == true)
        {
            if (i >= 15)
            {
                if (material == "concrete")
                {
                    PlayRandomConcreteSprint();
                }
                else if (material == "metal")
                {
                    PlayRandomMetalSprint();
                }
                i = 0;
            }
            else
            {
                i = i + 1;
            }
        }

        if (playerMovement.isGrounded == false)
        {
            falling = true;
            velocity = playerMovement.currentYVelocity;
        }
        else if (playerMovement.isGrounded == true && falling == true)
        {
            falling = false;
            pitchMin = 0.8f;
            pitchMax = 1.2f;
            playerSource.pitch = Random.Range(pitchMin, pitchMax);
            playerSource.volume = -0.01f*velocity;
            if (material == "concrete")
            {
                clipIndex = RepeatCheck(clipIndex, concreteSprintArray.Length);
                playerSource.PlayOneShot(concreteSprintArray[clipIndex]);
            }
            else if (material == "metal")
            {
                clipIndex = RepeatCheck(clipIndex, metalSprintArray.Length);
                playerSource.PlayOneShot(metalSprintArray[clipIndex]);
            }
        }
    }
}
