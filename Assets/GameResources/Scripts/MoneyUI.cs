using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MoneyUI : MonoBehaviour
{
    public TextMeshProUGUI moneyText;


    // Update is called once per frame
    void Update()
    {
        moneyText.text = "Money: " + PlayerStats.Money + "$";
    }
}
