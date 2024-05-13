using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollisionDetection : MonoBehaviour
{
    [Header("Damage")]
    public int swordDamage = 8;
    private string enemyTag = "Enemy";

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag(enemyTag)) {
            PlayerHealth enemyHealth = other.gameObject.GetComponent<PlayerHealth>();
            enemyHealth.TakeDamage(swordDamage);
        }    
    }
}
