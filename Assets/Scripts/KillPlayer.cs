using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillPlayer : MonoBehaviour
{
    public Transform respawnPoint;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (respawnPoint != null)
            {
                // Use the respawn manager to handle respawning
                RespawnManager.instance.RespawnPlayerAt(respawnPoint.position);
            }
            else
            {
                // If no respawn point, just reload the scene
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}