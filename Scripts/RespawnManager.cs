using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager instance;
    private Vector3 respawnPosition;
    private bool shouldRespawn = false;

    void Awake()
    {
        // Singleton pattern to maintain this object across scene loads
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Called when a scene is loaded
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (shouldRespawn)
        {
            // Find the player in the new scene
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                // Reposition the player
                player.transform.position = respawnPosition;
                shouldRespawn = false;
            }
        }
    }

    // Call this method to set respawn position and trigger a respawn
    public void RespawnPlayerAt(Vector3 position)
    {
        respawnPosition = position;
        shouldRespawn = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}