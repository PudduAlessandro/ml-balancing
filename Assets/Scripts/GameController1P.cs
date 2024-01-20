using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class GameController1P : MonoBehaviour
{
    // Player related
    public GameObject playerPrefab;
    public GameObject cpuPrefab;
    private PlayerV2 _player;
    private CPUPlayer _cpuPlayer;

    // Map related
    public int selectedMapIndex;
    private MapGenerator _mapGenerator;
    [SerializeField] private Tilemap tilemap;
    private int _foodRespawnChance = 25; // 1 equals 0.1%

    // Turn related
    private Vector3Int _player1Selection, _player2Selection;
    [SerializeField] private bool player1Confirm = false;
    [SerializeField] private bool player2Confirm = false;
    [SerializeField] private int _collectionGoal = 5;

    // UI
    private UIController _uiController;
    
    
    // Set up the map, players, etc.
    public void SetupGame()
    {
        // MapGenerator is on the same object
        _mapGenerator = gameObject.GetComponent<MapGenerator>();
        _mapGenerator.selectedMap = (MapGenerator.Map)selectedMapIndex;
        
        // Currently on seperate object but can be moved
        _uiController = GameObject.Find("UIController").GetComponent<UIController>();
        _uiController.collectionGoal = _collectionGoal;
        
        // Generate the map, using a string depending on selectedMapIndex
        tilemap = _mapGenerator.BuildMap();
        
        // Create the players and position them on their spawn points
        SpawnPlayers();
    }

    private void SpawnPlayers()
    {
        Transform parentTransform = tilemap.transform.parent;

        PlayerInput playerInput = PlayerInput.Instantiate(playerPrefab, 1, pairWithDevice: Keyboard.current);
        _player = playerInput.GetComponent<PlayerV2>();
        
        _player.Setup("Player 1", parentTransform, tilemap, _mapGenerator.player1Spawn, this)

    }
}
