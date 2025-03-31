using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potions : MonoBehaviour
{
    public float amountOfHealth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<PlayerHealth>().maxHealth - collision.gameObject.GetComponent<PlayerHealth>().health >= amountOfHealth)
        {
            collision.gameObject.GetComponent<PlayerHealth>().health += amountOfHealth;
            collision.gameObject.GetComponent<PlayerHealth>().currentHealth = collision.gameObject.GetComponent<PlayerHealth>().health;
            Destroy(gameObject);
        }
    }
}
