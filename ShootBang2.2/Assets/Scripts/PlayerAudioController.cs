using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    public AudioClip[] concreteCrouchArray;
    public AudioClip[] concreteWalkArray;
    public AudioClip[] concreteSprintArray;
    public AudioSource effectSource;
    float pitchMin, pitchMax, volumeMin, volumeMax;
    private int clipIndex = 0;


    void PlayRandomConcreteCrouch()
    {
        pitchMin = 0.9f;
        pitchMax = 1.2f;
        volumeMin = 1.0f;
        volumeMax = 1.4f;
        effectSource.pitch = Random.Range(pitchMin, pitchMax);
        effectSource.volume = Random.Range(volumeMin, volumeMax);
        clipIndex = RepeatCheck(clipIndex, concreteCrouchArray.Length);
        effectSource.PlayOneShot(concreteCrouchArray[clipIndex]);
    }

    void PlayRandomConcreteWalk()
    {
        pitchMin = 0.8f;
        pitchMax = 1.2f;
        volumeMin = 0.8f;
        volumeMax = 1.2f;
        effectSource.pitch = Random.Range(pitchMin, pitchMax);
        effectSource.volume = Random.Range(volumeMin, volumeMax);
        clipIndex = RepeatCheck(clipIndex, concreteWalkArray.Length);
        effectSource.PlayOneShot(concreteWalkArray[clipIndex]);
    }

    void PlayRandomConcreteSprint()
    {
        pitchMin = 0.8f;
        pitchMax = 1.4f;
        volumeMin = 0.8f;
        volumeMax = 1.4f;
        effectSource.pitch = Random.Range(pitchMin, pitchMax);
        effectSource.volume = Random.Range(volumeMin, volumeMax);
        clipIndex = RepeatCheck(clipIndex, concreteSprintArray.Length);
        effectSource.PlayOneShot(concreteSprintArray[clipIndex]);
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

    void Update()
    {

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
