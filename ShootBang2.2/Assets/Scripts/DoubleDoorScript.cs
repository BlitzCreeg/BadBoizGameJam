using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDoorScript : MonoBehaviour
{
    public Animator anim;
    public AudioSource aud;

    public bool canOpen = false;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && canOpen)
        {
            anim.Play("LiftDoorsOpenClose");
            aud.Play();
        }
    }
}
