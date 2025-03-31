using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    public float currentHealth;
    public float maxHealth;
    private Animator animator;
    [SerializeField] FloatingHealthBar healthBar;

    private void Awake()
    {
        healthBar = GetComponentInChildren<FloatingHealthBar>();
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        if (health < currentHealth)
        {
            currentHealth = health;
            animator.SetTrigger("Attacked");
            healthBar.UpdateHealthBar(health, maxHealth);
        }
        if (health <= 0)
        {
            Destroy(gameObject);
            Debug.Log("Enemy is dead");

            if (CoinManager.instance != null)
            {
                CoinManager.instance.AddCoins(5);
                Debug.Log("Added 5 coins to player");
            }
        }
    }
}