using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class metalSoundPlay : MonoBehaviour
{

    public AudioClip[] metalMissArray;
    float pitchMin, pitchMax, volumeMin, volumeMax;
    public AudioSource targetSource;
    private int clipIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        pitchMin = 0.9f;
        pitchMax = 1.2f;
        volumeMin = 0.8f;
        volumeMax = 1.4f;
        targetSource.pitch = Random.Range(pitchMin, pitchMax);
        targetSource.volume = Random.Range(volumeMin, volumeMax);
        clipIndex = RepeatCheck(clipIndex, metalMissArray.Length);
        targetSource.PlayOneShot(metalMissArray[clipIndex]);
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

}
