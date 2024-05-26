using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
public class PlayerAttacks : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;
    private PlayerInputActions playerInputs;
    private bool combatState;
    
    private void Awake() {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        playerInputs = new PlayerInputActions();
        combatState = anim.GetBool("Combat");
    }

    private void OnEnable() {
        playerInputs.Enable();
        playerInputs.PlayerActions.LightAttacks.performed += context => HandleLightAttacks(context);
        playerInputs.PlayerActions.HeavyAttacks.performed += context => HandleHeavyAttacks(context);
        playerInputs.PlayerActions.CombatState.performed += _ => anim.SetBool("Combat", !combatState);
    }

    private void OnDisable() {
        playerInputs.Disable();
    }

    private void HandleLightAttacks(InputAction.CallbackContext context) {
        if (context.interaction is TapInteraction) {
            SingleTapLightAttack();
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
        anim.SetTrigger("LightAttackOne");
    }

    private void HoldLightAttack() {
        anim.SetTrigger("LightAttackTwo");
    }

    private void singleTapHeavyAttack() {
        anim.SetTrigger("HeavyAttackOne");
    }

    private void HoldHeavyAttack() {
        anim.SetTrigger("HeavyAttackTwo");
    }

}


