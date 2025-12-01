using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool isGameOver = false;



    void Update()
    {
        #region Win Lose Conditions
        if (!isGameOver)
            return;

        if (PlayerStats.Lives <= 0)
        {
            EndGame();   
        }
        #endregion
    }


    void EndGame()
    {
        isGameOver = true;  
        Debug.Log("Game Over!");
    }
}
