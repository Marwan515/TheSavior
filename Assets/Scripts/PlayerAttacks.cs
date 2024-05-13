using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
public class SwordAttacks : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;
    private PlayerInputActions playerInputs;
    private float multiTapThresholde = 0.4f;
    private float lastTapTime = 0;
    
    private void Awake() {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        playerInputs = new PlayerInputActions();
    }

    private void OnEnable() {
        playerInputs.Enable();
        playerInputs.PlayerActions.LightAttacks.performed += context => HandleLightAttacks(context);
        playerInputs.PlayerActions.HeavyAttacks.performed += context => HandleHeavyAttacks(context);
    }

    private void OnDisable() {
        playerInputs.Disable();
    }

    private void HandleLightAttacks(InputAction.CallbackContext context) {
        if (context.interaction is TapInteraction) {
            float currentTime = Time.time;
            float timeSinceLastTap = currentTime - lastTapTime;
            if (timeSinceLastTap <= multiTapThresholde) {
                DoubleTapLightAttack();
            } else {
                SingleTapLightAttack();
            }
            lastTapTime = currentTime;
        } else if (context.interaction is HoldInteraction) {
            HoldLightAttack();
        }
    }

    private void HandleHeavyAttacks(InputAction.CallbackContext context) {
        if (context.interaction is TapInteraction) {
            singleTapHeavyAttack();
        } else if (context.interaction is HoldInteraction) {
            HoldHeavyAttack();
        }
    }

    private void SingleTapLightAttack() {
        anim.SetTrigger("swordSlashLightOne");
    }

    private void DoubleTapLightAttack() {
        anim.SetTrigger("swordSlashLightTwo");
    }

    private void HoldLightAttack() {
        anim.SetTrigger("swordSlashLightThree");
    }

    private void singleTapHeavyAttack() {
        anim.SetTrigger("swordSlashHeavyOne");
    }

    private void HoldHeavyAttack() {
        anim.SetTrigger("swordSlashHeavyTwo");
    }
}
