using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Toggle();
        }
    }

    // Active or Deactive Pause Menu a.k.a Continue game
    public void Toggle()
    {
        pauseMenuUI.SetActive(!pauseMenuUI.activeSelf);


        if (pauseMenuUI.activeSelf)
        {
            Time.timeScale = 0f; // Pause the game
        }
        else 
        {
            Time.timeScale = 1f; // Resume the game
        }
    }

    public void Retry()
    {
        Toggle();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }

    public void Menu()
    {
        Debug.Log("Loading Menu...");
    }
}
