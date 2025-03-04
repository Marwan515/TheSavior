using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public int currentHealth;
    public int maxHealth;
    public Slider healthBar;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.maxValue = maxHealth;
        currentHealth = maxHealth;
        healthBar.value = currentHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
        if (gameObject.tag == "Player") {
            anim.SetTrigger("hitReaction");
            if (!anim.GetBool("Combat")) {
                anim.SetBool("Combat", true);
            }
            if (currentHealth <= 0) {
                Die();
            }
        }
        
    }

    public void Heal(int amount) {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0 , maxHealth);
        UpdateHealthBar();
    }

    private void Die() {
        anim.SetTrigger("Death");
    }

    private void UpdateHealthBar() {
        healthBar.value = currentHealth;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.K))
        {
            TakeDamage(10);
        }
    }
}
