using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.iOS;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public GameObject playerPrefab;
    private Player _player1, _player2;

    // Map related
    private MapGenerator _mapGenerator;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tilemap p1Overlay, p2Overlay;
    [SerializeField] private Tile p1SelectionTile, p2SelectionTile;
    private int _foodRespawnChance = 25; // 1 equals 0.1%

    // Turn related
    private Vector3Int _player1Selection, _player2Selection;
    [FormerlySerializedAs("_player1Confirm")] [SerializeField] private bool player1Confirm = false;
    [FormerlySerializedAs("_player2Confirm")] [SerializeField] private bool player2Confirm = false;
    //private int _playerMoveSpeed = 1; // Used for smooth movement
    public int turnCount = 0;
    
    // UI
    private UIController _uiController;

    
    
    
    // Start is called before the first frame update
    void Start()
    {
        _mapGenerator = gameObject.GetComponent<MapGenerator>();

        _uiController = GameObject.Find("UIController").GetComponent<UIController>();

        tilemap = _mapGenerator.BuildMap();
        SpawnPlayers();
    }

    // Update is called once per frame
    void Update()
    {
        _player1Selection = _player1.selectedPosition;
        _player2Selection = _player2.selectedPosition;

        player1Confirm = _player1.turnConfirmed;
        player2Confirm = _player2.turnConfirmed;
        
        if (player1Confirm && player2Confirm)
        {
            PlayTurn(_player1Selection, _player2Selection);
            _uiController.UpdateUI();
        }
    }

    private void FixedUpdate()
    {
        
    }

    private void PlayTurn(Vector3Int player1SelectedTile, Vector3Int player2SelectedTile)
    {
        RespawnFood();
        
        _player1.gameObject.transform.position = GetTileCenterPosition(player1SelectedTile);
        _player1.FinishTurn(player1SelectedTile);
        if (tilemap.GetTile(_player1.currentPosition).name.Equals("3_FOREST"))
        {
            tilemap.SetTile(_player1.currentPosition, _mapGenerator.tiles[6]);
        }
        
        _player2.gameObject.transform.position = GetTileCenterPosition(player2SelectedTile);
        _player2.FinishTurn(player2SelectedTile);
        if (tilemap.GetTile(_player2.currentPosition).name.Equals("3_FOREST"))
        {
            tilemap.SetTile(_player2.currentPosition, _mapGenerator.tiles[6]);
        }

        CheckForWinner();
        
        turnCount++;
    }

    private void CheckForWinner()
    {
        int winner = 0;
        
        if (_player1.currentHealth == 0 && _player2.currentHealth == 0)
        {
            winner = 3;
        } else if (_player1.currentHealth == 0)
        {
            winner = 2;
        } else if (_player2.currentHealth == 0)
        {
            winner = 1;
        }

        if (winner == 0) return;
        
        _player1.GetComponent<PlayerInput>().enabled = false;
        _player2.GetComponent<PlayerInput>().enabled = false;
            
        _uiController.UpdateWinner(winner);
    }

    private void RespawnFood()
    {
        int xCoord = 0;
        int yCoord = 0;

        for (int y = 0; y >= -5; y--)
        {
            for (int x = 0; x <= 5; x++)
            {
                Vector3Int tilePos = new Vector3Int(x, y, 0);
                if (tilemap.GetTile(tilePos).name.Equals("7_FORESTUSED"))
                {
                    int randomValue = Random.Range(0, 1001);

                    if (randomValue <= _foodRespawnChance)
                    {
                        tilemap.SetTile(tilePos, _mapGenerator.tiles[2]);
                    }
                }
            }
        }
    }

    private void SpawnPlayers()
    {
        Transform parentTransform = tilemap.transform.parent;
        
        // Instantiate
        PlayerInput p1Input = PlayerInput.Instantiate(playerPrefab, 1, "Player1", pairWithDevice: Keyboard.current);
        p1Input.gameObject.name = "Player 1";
        p1Input.GetComponent<Player>().SetupPlayer(parentTransform, p1Overlay, tilemap, _mapGenerator.player1Spawn, p1SelectionTile);
        _player1 = p1Input.gameObject.GetComponent<Player>();
        _uiController.player1 = _player1;


        PlayerInput p2Input = PlayerInput.Instantiate(playerPrefab, 2, "Player2", pairWithDevice: Keyboard.current);
        p2Input.gameObject.name = "Player 2";
        p2Input.GetComponent<Player>().SetupPlayer(parentTransform, p2Overlay, tilemap, _mapGenerator.player2Spawn, p2SelectionTile);
        _player2 = p2Input.gameObject.GetComponent<Player>();
        _uiController.player2 = _player2;


    }

    private Vector3 GetTileCenterPosition(Vector3Int tileCoordinate)
    {
        return tilemap.CellToWorld(tileCoordinate) + new Vector3(0.5f, 0.5f, 0);
    }
}
