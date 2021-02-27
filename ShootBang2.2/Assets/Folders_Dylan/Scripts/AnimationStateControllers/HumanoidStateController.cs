using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidStateController : MonoBehaviour
{
    Animator animator;

    int isWalkingHash;
    public bool isWalking;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        Debug.Log(isWalkingHash);
    }

    // Update is called once per frame
    void Update()
    {
        if (isWalking)
        {
            // Start Walking
            animator.SetBool(isWalkingHash, true);
        }

        if (!isWalking)
        {
            // Should Not Be Walking
            animator.SetBool(isWalkingHash, false);
        }
    }
}
