using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winScript : MonoBehaviour
{
    // Automatically called when the script starts
    void Start()
    {
        // Invoke BackToMenu method after 5 seconds
        Invoke("BackToMenu", 5f);
    }

    // Loads the Main Menu scene
    private void BackToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    // Update method is unused but can be implemented if needed
}
