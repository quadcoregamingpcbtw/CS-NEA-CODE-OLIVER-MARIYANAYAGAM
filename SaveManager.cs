using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    // Singleton instance of SaveManager
    public static SaveManager Instance { get; set; }

    private string highScoreKey = "BestWaveValue"; // Key for saving the best wave score

    private void Awake()
    {
        // Ensure only one instance of SaveManager exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy any duplicate instances
        }
        else
        {
            Instance = this; // Set this object as the singleton instance
        }

        DontDestroyOnLoad(this); // Keep the SaveManager across scenes
    }

    // Saves the high score to PlayerPrefs
    public void SaveHighScore(int score)
    {
        PlayerPrefs.SetInt(highScoreKey, score); // Save the score
    }

    // Loads the high score from PlayerPrefs
    public int LoadHighScore()
    {
        if (!PlayerPrefs.HasKey(highScoreKey)) 
        { 
            return 0; // If no high score exists, return 0
        }
        return PlayerPrefs.GetInt(highScoreKey); // Return the saved high score
    }
}
