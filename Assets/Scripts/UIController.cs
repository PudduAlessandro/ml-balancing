using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIController : MonoBehaviour
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


    public Player player1, player2;

    public TextMeshProUGUI winnerText;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateUI()
    {
        p1Health.text = player1.currentHealth.ToString();
        p1Hunger.text = player1.currentHunger.ToString();
        p1Thirst.text = player1.currentThirst.ToString();
        p1Collected.text = player1.collectedFood + " / " + collectionGoal;
        
        p2Health.text = player2.currentHealth.ToString();
        p2Hunger.text = player2.currentHunger.ToString();
        p2Thirst.text = player2.currentThirst.ToString();
        p2Collected.text = player2.collectedFood + " / " + collectionGoal;
    }

    public void UpdateWinner(int winner)
    {
        winnerText.gameObject.SetActive(true);
        
        switch (winner)
        {
            case 1: 
                winnerText.text = "Player 1 wins!";
                break;
            case 2:
                winnerText.text = "Player 2 wins!";
                break;
            case 3:
                winnerText.text = "Draw!";
                break;
        }
    }
}
