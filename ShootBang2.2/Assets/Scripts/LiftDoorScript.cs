using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftDoorScript : MonoBehaviour
{
    public Animator anim;
    public AudioSource aud;
    public AudioClip[] doorArray;
    float pitchMin, pitchMax, volumeMin, volumeMax;
    private int clipIndex = 0;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pitchMin = 0.9f;
            pitchMax = 1.2f;
            volumeMin = 0.2f;
            volumeMax = 0.6f;
            aud.pitch = Random.Range(pitchMin, pitchMax);
            aud.volume = Random.Range(volumeMin, volumeMax);
            clipIndex = RepeatCheck(clipIndex, doorArray.Length);
            aud.PlayOneShot(doorArray[clipIndex]);
            anim.Play("LiftDoorsOpenClose");
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
}
