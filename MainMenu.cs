using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // UI elements for displaying high scores and game information
    public TMP_Text highScoreCount;
    public TMP_Text Information_Panel;

    // Game instructions displayed in the Information Panel
    private string Info = @"How to Play Dibik Fighter:
Learn Controls:
Movement: Use W, A, S, D keys for movement.
Aim: Use the mouse.
Shoot: Left mouse button.

Master Gameplay Basics:
Stay mobile to avoid being an easy target.
Use cover to shield yourself from enemies.

Manage Resources:
Watch your ammo count.
Pick up weapons and health as needed.

Know the Map:
Learn choke points, high ground, and hiding spots.
Stick to areas with good visibility and escape routes.

Practice:
Train in aim trainers or practice modes.
Focus on improving reaction time and accuracy.
Enjoy the game and adapt your strategies as you learn!";

    // Loads the main game scene
    public void Start_Game()
    {
        SceneManager.LoadScene("Main Map - Dibik Industries");
    }

    // Displays game instructions in the information panel
    public void Information()
    {
        Information_Panel.text = Info;
    }

    // Loads and displays the highest wave survived
    public void HighScore()
    {
        int highScore = SaveManager.Instance.LoadHighScore();
        highScoreCount.text = $"Highest Wave Survived: {highScore}";
    }

    // Loads the training map scene
    public void Training()
    {
        SceneManager.LoadScene("Training Map");
    }

    // Exits the game (works in both editor and build)
    public void exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stop play mode in Unity Editor
#else
        Application.Quit(); // Quit application in standalone build
#endif
    }
}
