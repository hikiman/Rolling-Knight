using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHealth : MonoBehaviour
{
    public float health = 200f;
    public float currentHealth;
    public float maxHealth = 200f;

    public bool isInvulnerable = false;
    private Animator animator;

    [SerializeField] FloatingHealthBar healthBar;

    private void Awake()
    {
        healthBar = GetComponentInChildren<FloatingHealthBar>();
        animator = GetComponent<Animator>();
        currentHealth = health;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (health < currentHealth)
        {
            currentHealth = health;

            if (healthBar != null)
            {
                healthBar.UpdateHealthBar(health, maxHealth);
            }
        }

        if (health <= maxHealth * 0.4f)
        {
            if (animator != null)
            {
                animator.SetBool("IsEnraged", true);
            }
        }

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("LevelSelect");
    }
}
