using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Text healthText;
    public Slider healthBar;
    public Text livesText;
    public Text levelText;
    public Slider xpBar;

    public GameObject[] hearts;
    public Animator playerAnimator;
    public Animator animalAnimator;
    public Transform playerTransform;

    private Vector3 initialPosition;

    private int maxHealth = 100;
    private int currentHealth = 100;
    private int lives = 3;
    private int maxLives = 3;
    private int level = 1;
    private int currentXP = 0;
    private int xpToNextLevel = 100;
    private bool canTakeDamage = true;

    void Start()
    {
        initialPosition = playerTransform.position;
        UpdateUI();

    }

    public void UpdateUI()
    {
        healthText.text = "Health: " + currentHealth + "%";
        healthBar.value = (float)currentHealth / maxHealth;
        livesText.text = "Lives: " + lives;
        levelText.text = "Level: " + level;
        xpBar.value = (float)currentXP / xpToNextLevel;

        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(i < lives);
        }

    }

    public void TakeDamage(int damage)
    {
        if (canTakeDamage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                LoseLife();
            }
            UpdateUI();
        }
    }

    private void LoseLife()
    {
        lives--;
        if (lives <= 0)
        {
            lives = 0;
            canTakeDamage = false;
            playerAnimator.SetTrigger("Die");
            Invoke("RestartGame", 5.0f);
        }
        else
        {
            currentHealth = maxHealth;
        }
        UpdateUI();
    }


    public void SetHealth(int health)
    {
        currentHealth = health;
        UpdateUI();
    }

    public void SetLives(int lives)
    {
        this.lives = lives;
        UpdateUI();
    }


    public void SetLevel(int level)
    {
        this.level = level;
        UpdateUI();
    }

    public void GainXP(int xp)
    {
        currentXP += xp;
        if (currentXP >= xpToNextLevel)
        {
            level++;
            currentXP = 0;
            xpToNextLevel += 20; 
        }
        UpdateUI();
    }

    private void RestartGame()
    {
        lives = maxLives;
        currentHealth = maxHealth;
        canTakeDamage = true;
        playerTransform.position = initialPosition;
        playerAnimator.SetTrigger("Idle");
        animalAnimator.SetTrigger("AnimalIdle");
        UpdateUI();
    }
}
