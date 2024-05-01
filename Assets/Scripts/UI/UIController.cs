using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public int collectionGoal;

    [Header("Player1")] 
    [SerializeField] private TextMeshProUGUI playerHealth;
    [SerializeField] private TextMeshProUGUI playerHunger;
    [SerializeField] private TextMeshProUGUI playerThirst;
    [SerializeField] private TextMeshProUGUI playerCollected;
        
        
    [Header("Player2")] 
    [SerializeField] private TextMeshProUGUI opponentHealth;
    [SerializeField] private TextMeshProUGUI opponentHunger;
    [SerializeField] private TextMeshProUGUI opponentThirst;
    [SerializeField] private TextMeshProUGUI opponentCollected;


    public Player player;
    public CPUPlayer cpuPlayer;

    public TextMeshProUGUI winnerText;

    public void UpdateUI()
    {
        playerHealth.text = player.currentHealth.ToString();
        playerHunger.text = player.currentFood.ToString();
        playerThirst.text = player.currentWater.ToString();
        playerCollected.text = player.collectedFood + " / " + collectionGoal;
        
        opponentHealth.text = cpuPlayer.currentHealth.ToString();
        opponentHunger.text = cpuPlayer.currentFood.ToString();
        opponentThirst.text = cpuPlayer.currentWater.ToString();
        opponentCollected.text = cpuPlayer.collectedFood + " / " + collectionGoal;
    }

    public void UpdateWinner(int winner)
    {
        winnerText.gameObject.SetActive(true);
        
        switch (winner)
        {
            case 1: 
                winnerText.text = "Player wins!";
                break;
            case 2:
                winnerText.text = "Opponent wins!";
                break;
            case 3:
                winnerText.text = "Draw!";
                break;
        }
    }
}
