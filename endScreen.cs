using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class endScreen : MonoBehaviour
{
    public TMP_Text highScoreCount; // Text UI element to display the high score

    // Starts a new game by loading the "Main Menu" scene
    public void Start_Game()
    {
        SceneManager.LoadScene("Main Menu"); // Load the main menu
    }

    // Start is called when the script is first initialized
    private void Start()
    {
        // Load the highest wave survived from SaveManager and display it
        int highScore = SaveManager.Instance.LoadHighScore();
        highScoreCount.text = $"Highest Wave Survived: {highScore}";
    }

    // Exits the application or stops play mode in Unity Editor
    public void exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stop play mode in Unity Editor
#else
        Application.Quit(); // Exit the game in a build
#endif
    }
}
