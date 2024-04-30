using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIControllerV2 : MonoBehaviour
{
    public int collectionGoal;
    
    [Header("Player1")] 
    [SerializeField] private TextMeshProUGUI p1Health;
    [SerializeField] private TextMeshProUGUI p1Hunger;
    [SerializeField] private TextMeshProUGUI p1Thirst;
    [SerializeField] private TextMeshProUGUI p1Collected;
        
        
    [Header("Player2")] 
    [SerializeField] private TextMeshProUGUI p2Health;
    [SerializeField] private TextMeshProUGUI p2Hunger;
    [SerializeField] private TextMeshProUGUI p2Thirst;
    [SerializeField] private TextMeshProUGUI p2Collected;


    public PlayerV2 player;
    public CPUPlayer cpuPlayer;

    public TextMeshProUGUI winnerText;

    public void UpdateUI()
    {
        p1Health.text = player.currentHealth.ToString();
        p1Hunger.text = player.currentFood.ToString();
        p1Thirst.text = player.currentWater.ToString();
        p1Collected.text = player.collectedFood + " / " + collectionGoal;
        
        p2Health.text = cpuPlayer.currentHealth.ToString();
        p2Hunger.text = cpuPlayer.currentFood.ToString();
        p2Thirst.text = cpuPlayer.currentWater.ToString();
        p2Collected.text = cpuPlayer.collectedFood + " / " + collectionGoal;
    }

    public void UpdateWinner(int winner)
    {
        winnerText.gameObject.SetActive(true);
        
        switch (winner)
        {
            case 1: 
                winnerText.text = "Spieler gewinnt!";
                break;
            case 2:
                winnerText.text = "Gegner gewinnt!";
                break;
            case 3:
                winnerText.text = "Unentschieden!";
                break;
        }
    }
}
