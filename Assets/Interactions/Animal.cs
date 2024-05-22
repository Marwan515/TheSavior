using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public enum AnimalState { Idle, Friendly, Aggressive }
    public AnimalState currentState = AnimalState.Idle;
    private bool isFed = false;

    private Animator animator;

    public float eatingAnimationLength;


    void Start()
    {
        animator = GetComponent<Animator>();

        eatingAnimationLength = GetAnimationLength("Eating");

        if (animator == null)
        {
            Debug.LogError("Animator not found on Animal.");
        }

    }


    private float GetAnimationLength(string animationName)
    {
        if (animator == null || string.IsNullOrEmpty(animationName))
        {
            return 0f;
        }

        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == animationName)
            {
                return clip.length;
            }
        }

        return 0f;
    }

    public void PlayAggressiveAnimation()
    {
        animator.SetTrigger("Aggressive");
        Debug.Log("Playing aggressive animation");
        
        
    }

    public void PlayEatingAnimation()
    {
        animator.SetTrigger("Eating");
        Debug.Log("Playing eating animation");
    }



    public void PlayFriendlyAnimation()
    {
        animator.SetTrigger("Friendly");
        Debug.Log("Playing friendly animation");
        currentState = AnimalState.Friendly;

    }


    public void Befriend()
    {
        if(!isFed)
        {
            isFed = true;
            PlayFriendlyAnimation();
            Debug.Log("Animal is now your friend");
        }
    }

    public void AggressiveInteraction()
    {
        if (!isFed)
        {
            PlayAggressiveAnimation();
           // UI.Instance.ShowMessage("The animal is aggressive! Prepare for a fight!");
        }
    }
}
