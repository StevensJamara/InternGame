using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Player Attribute")]
    public static int Money;
    public int startMoney = 50;


    [Header("Player Survival")]
    public static int Lives;
    public int startLives = 15;
    public static int Rounds;

    void Start()
    {
        Money = startMoney;
        Lives = startLives;

        Rounds = 0;
    }


}
