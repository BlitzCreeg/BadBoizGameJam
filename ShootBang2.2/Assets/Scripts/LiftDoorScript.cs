using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftDoorScript : MonoBehaviour
{
    public Animator anim;
    public AudioSource aud;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.Play("LiftDoorsOpenClose");
            aud.Play();
        }
    }
}
