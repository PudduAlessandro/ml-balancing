using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class GameController1P : MonoBehaviour
{
    // Player related
    public GameObject playerPrefab;
    public GameObject cpuPrefab;
    private PlayerV2 _player;
    private CPUPlayer _cpuPlayer;

    // Map related
    public int selectedMapIndex;
    public string mapString;
    private MapGenerator _mapGenerator;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private int foodRespawnChance = 25; // 1 equals 0.1%

    // Turn related
    private Vector3Int _playerTileSelection, _cpuTileSelection;
    [SerializeField] private int collectionGoal = 5;

    // UI
    private UIControllerV2 _uiController;


    public void Start()
    {
        if (PlayerPrefs.HasKey("startByURL"))
        {
            var uri = new Uri(Application.absoluteURL);

            string level = uri.GetComponents(UriComponents.Query, UriFormat.SafeUnescaped);

            if (!string.IsNullOrEmpty(level))
            {
                var value = level.Split('=')[1];

                if (Regex.IsMatch(value, @"\d{36}"))
                {
                    Debug.Log($"Parameter: {value}) ist in Ordnung");
                    PlayerPrefs.SetString("map", DecodeMapString(value));
                }
                else
                {
                    Debug.Log($"Parameter: {value}) ist nicht in Ordnung");
                    SceneManager.LoadScene("MainMenu");
                }
            }
            else
            {
                SceneManager.LoadScene("MainMenu");
            }
        }

        SetupGame();
    }

    // Set up the map, players, etc.
    public void SetupGame()
    {
        if (PlayerPrefs.HasKey("map"))
        {
            mapString = PlayerPrefs.GetString("map");
        }
        
        // MapGenerator is on the same object
        _mapGenerator = gameObject.GetComponent<MapGenerator>();
        _mapGenerator.selectedMap = (MapGenerator.Map)selectedMapIndex;
        _mapGenerator.mapString = mapString;
        
        
        
        // Currently on separate object but can be moved
        _uiController = GameObject.Find("UIController").GetComponent<UIControllerV2>();
        _uiController.collectionGoal = collectionGoal;

        // Generate the map, using a string depending on selectedMapIndex
        tilemap = _mapGenerator.BuildMap();
        
        // Create the players and position them on their spawn points
        SpawnPlayers();
        _uiController.UpdateUI();
    }

    private void SpawnPlayers()
    {
        // Parent transform for both players
        Transform parentTransform = tilemap.transform.parent;

        // Create and setup the player
        PlayerInput playerInput = PlayerInput.Instantiate(playerPrefab, 1, pairWithDevice: Keyboard.current);
        _player = playerInput.GetComponent<PlayerV2>();
        _player.Setup("Player 1", parentTransform, tilemap, _mapGenerator.player1Spawn, this);
        _uiController.player = _player;

        // Create and setup the CPU
        _cpuPlayer = Instantiate(cpuPrefab, parentTransform).GetComponent<CPUPlayer>();
        _cpuPlayer.Setup("Player 2", tilemap, _mapGenerator.player2Spawn, this);
        _uiController.cpuPlayer = _cpuPlayer;
    }

    public void UpdateGameStatus(Vector3Int p1NewPosition)
    {
        _playerTileSelection = p1NewPosition;
        _cpuTileSelection = _cpuPlayer.CPUMove();

        PlayTurn();
        _uiController.UpdateUI();
        
        if (CheckForWinner())
        {
            return;
        }
    }

    private void PlayTurn()
    {
        RegrowFoodTiles();
        
        _player.gameObject.transform.position = GetTileCenterPosition(_playerTileSelection);
        _cpuPlayer.gameObject.transform.position = GetTileCenterPosition(_cpuTileSelection);

        _player.UpdateStatus();
        _cpuPlayer.UpdateStatus();
        
        if (tilemap.GetTile(_player.currentPosition).name.Contains("Food"))
        {
            tilemap.SetTile(_player.currentPosition,
                tilemap.GetTile(_player.currentPosition).name.Contains("WAdjacent")
                    ? _mapGenerator.tiles[6]
                    : _mapGenerator.tiles[9]);
        }
        
        if (tilemap.GetTile(_cpuPlayer.currentPosition).name.Contains("Food"))
        {
            tilemap.SetTile(_cpuPlayer.currentPosition,
                tilemap.GetTile(_cpuPlayer.currentPosition).name.Contains("WAdjacent")
                    ? _mapGenerator.tiles[6]
                    : _mapGenerator.tiles[9]);
        }
        
    }

    

    private void RegrowFoodTiles()
    {
        for (var y = 0; y >= -5; y--)
        {
            for (var x = 0; x <= 5; x++)
            {
                var tilePos = new Vector3Int(x, y, 0);
                
                if (!tilemap.GetTile(tilePos).name.Contains("FoodUsed")) continue;
                
                var randomValue = Random.Range(0, 1000);

                if (randomValue > foodRespawnChance) continue;

                tilemap.SetTile(tilePos,
                    tilemap.GetTile(tilePos).name.Contains("WAdjacent")
                        ? _mapGenerator.tiles[8]
                        : _mapGenerator.tiles[1]);
            }
        }
    }
    
    private Vector3 GetTileCenterPosition(Vector3Int tileCoord)
    {
        return tilemap.CellToWorld(tileCoord) + new Vector3(0.5f, 0.5f, 0);
    }
    
    private bool CheckForWinner()
    {
        // 0: No winner;
        // 1: Player 1 wins
        // 2: Player 2 wins
        // 3: Draw
        
        int winner = 0;
        
        if ((_player.currentHealth == 0 && _cpuPlayer.currentHealth == 0) || (_player.collectedFood == collectionGoal && _cpuPlayer.collectedFood == collectionGoal)) 
        {
            winner = 3;
        } else if (_player.currentHealth == 0 || _cpuPlayer.collectedFood == collectionGoal)
        {
            winner = 2;
        } else if (_cpuPlayer.currentHealth == 0 || _player.collectedFood == collectionGoal)
        {
            winner = 1;
        }

        if (winner == 0) return false;
        
        _player.GetComponent<PlayerInput>().enabled = false;

        _uiController.UpdateWinner(winner);

        return true;
    }
    
    private string DecodeMapString(string mapString)
    {
        return Regex.Replace(mapString, @"(\d{1})(\d{1})(\d{1})(\d{1})(\d{1})(\d{1})", "$1 $2 $3 $4 $5 $6,").TrimEnd(',');
    }
}
