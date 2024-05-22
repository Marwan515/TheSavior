using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class MainCharacter : MonoBehaviour
{
    private Animator animator;


    public Animator Animator => animator;


    void Start()
    {
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("Animator not found on MainCharacter.");
        }

    }


    public void PlayFeedingAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("Feeding");

        }
        else
        {
            Debug.LogError("Animator is not assigned or found.");
        }

    }
}
