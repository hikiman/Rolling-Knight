using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("Quitting game...");

        // This will close the game in a built application
        Application.Quit();

        // This will stop play mode in the Unity Editor
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
