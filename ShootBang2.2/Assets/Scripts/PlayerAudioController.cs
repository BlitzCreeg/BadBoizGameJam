using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    public AudioClip[] concreteCrouchArray;
    public AudioClip[] concreteWalkArray;
    public AudioClip[] concreteSprintArray;
    public AudioClip[] jumpArray;
    public AudioSource effectSource;
    public PlayerMovement playerMovement;
    float pitchMin, pitchMax, volumeMin, volumeMax;
    private int clipIndex = 0;
    int i;

    void PlayRandomConcreteCrouch()
    {
        pitchMin = 0.9f;
        pitchMax = 1.1f;
        volumeMin = 0.6f;
        volumeMax = .8f;
        effectSource.pitch = Random.Range(pitchMin, pitchMax);
        effectSource.volume = Random.Range(volumeMin, volumeMax);
        clipIndex = RepeatCheck(clipIndex, concreteCrouchArray.Length);
        effectSource.PlayOneShot(concreteCrouchArray[clipIndex]);
    }

    void PlayRandomConcreteWalk()
    {
        pitchMin = 0.8f;
        pitchMax = 1f;
        volumeMin = 0.25f;
        volumeMax = 0.5f;
        effectSource.pitch = Random.Range(pitchMin, pitchMax);
        effectSource.volume = Random.Range(volumeMin, volumeMax);
        clipIndex = RepeatCheck(clipIndex, concreteWalkArray.Length);
        effectSource.PlayOneShot(concreteWalkArray[clipIndex]);
    }

    void PlayRandomConcreteSprint()
    {
        pitchMin = 0.9f;
        pitchMax = 1.2f;
        volumeMin = 0.8f;
        volumeMax = 1.4f;
        effectSource.pitch = Random.Range(pitchMin, pitchMax);
        effectSource.volume = Random.Range(volumeMin, volumeMax);
        clipIndex = RepeatCheck(clipIndex, concreteSprintArray.Length);
        effectSource.PlayOneShot(concreteSprintArray[clipIndex]);
    }

    void PlayRandomJump()
    {
        pitchMin = 0.8f;
        pitchMax = 1.2f;
        volumeMin = 0.8f;
        volumeMax = 1.2f;
        clipIndex = RepeatCheck(clipIndex, jumpArray.Length);
        effectSource.PlayOneShot(jumpArray[clipIndex]);
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

    void FixedUpdate()
    {
        if (playerMovement.isWalking == true)
        {
            if (i >= 35)
            {
                PlayRandomConcreteWalk();
                i = 0;
            }
            else
            {
                i = i + 1;
            }
            print("walking");
        }
        else if (playerMovement.isCrouchWalking == true)
        {
            if (i >= 50)
            {
                PlayRandomConcreteCrouch();
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
                PlayRandomConcreteSprint();
                i = 0;
            }
            else
            {
                i = i + 1;
            }
        }

        if (playerMovement.isJumping == true)
        {
            PlayRandomJump();
        }

        if (Input.GetKeyDown("j"))
        {
            PlayRandomConcreteCrouch();
        }
        if (Input.GetKeyDown("k"))
        {
            PlayRandomConcreteWalk();
        }
        if (Input.GetKeyDown("l"))
        {
            PlayRandomConcreteSprint();
        }
    }
}
