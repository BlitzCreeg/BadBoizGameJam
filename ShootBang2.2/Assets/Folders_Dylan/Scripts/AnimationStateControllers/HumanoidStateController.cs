using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidStateController : MonoBehaviour
{
    Animator animator;

    // Walking
    int isWalkingHash;
    public bool isWalking;

    // Attacking Idle
    public bool isAttackingIdle;
    int isAttackingIdleHash;

    // Attacking Walking
    public bool isAttackingWalking;
    int isAttackingWalkingHash;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isAttackingIdleHash = Animator.StringToHash("isAttackingIdle");
        isAttackingWalkingHash = Animator.StringToHash("isAttackingWalking");
    }

    // Update is called once per frame
    void Update()
    {
        // WALKING/IDLE
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


        // ATTACKING IDLE
        if (isAttackingIdle)
        {
            // Start Attacking
            animator.SetBool(isAttackingIdleHash, true);
        }

        if (!isAttackingIdle)
        {
            // Should Not Be Attacking
            animator.SetBool(isAttackingIdleHash, false);
        }


        // ATTACKING WALKING
        if (isAttackingWalking)
        {
            // Start Attacking
            animator.SetBool(isAttackingWalkingHash, true);
        }

        if (!isAttackingWalking)
        {
            // Should Not Be Attacking
            animator.SetBool(isAttackingWalkingHash, false);
        }
    }
}
