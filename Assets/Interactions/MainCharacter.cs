using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class MainCharacter : MonoBehaviour
{
    private Animator animator;
    public Animator Animator => animator;
    private int health = 100;
    private UI ui;


    void Start()
    {
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("Animator not found on MainCharacter.");
        }

        ui = FindObjectOfType<UI>();

    }

    void TakeDamage(int damage)
    {
        ui.TakeDamage(damage);
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
