using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public static bool isGameOver;

    private void Start()
    {
        isGameOver = false;
    }

    void Update()
    {
        #region Win Lose Conditions
        if (isGameOver)
            return;

        if (PlayerStats.Lives <= 0 || Input.GetKey(KeyCode.I))
        {
            EndGame();   
        }
        #endregion
    }


    void EndGame()
    {
        isGameOver = true;  
        gameOverUI.SetActive(true);
    }
}
