using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LivesUI : MonoBehaviour
{
    public TextMeshProUGUI livesText;

    
    void Update()
    {
        if (livesText != null)
        {
            livesText.text = "Health: " + PlayerStats.Lives.ToString();
        }
    }
}
