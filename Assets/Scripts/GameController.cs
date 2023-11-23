using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.iOS;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class GameController : MonoBehaviour
{
    public GameObject playerPrefab;
    private Player _player1, _player2;

    // Map related
    private MapGenerator _mapGenerator;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tilemap p1Overlay, p2Overlay;
    [SerializeField] private Tile p1SelectionTile, p2SelectionTile;

    // Turn related
    private Vector3Int _player1Selection, _player2Selection;
    [SerializeField] private bool _player1Confirm = false, _player2Confirm = false;
    private int _playerMoveSpeed = 1;
    public int turnCount = 0;

    
    
    
    // Start is called before the first frame update
    void Start()
    {
        _mapGenerator = gameObject.GetComponent<MapGenerator>();

        tilemap = _mapGenerator.BuildMap();
        SpawnPlayers();
    }

    // Update is called once per frame
    void Update()
    {
        _player1Selection = _player1.selectedPosition;
        _player2Selection = _player2.selectedPosition;

        _player1Confirm = _player1.turnConfirmed;
        _player2Confirm = _player2.turnConfirmed;
        
        if (_player1Confirm && _player2Confirm)
        {
            PlayTurn(_player1Selection, _player2Selection);
        }
    }

    private void FixedUpdate()
    {
        
    }

    private void PlayTurn(Vector3Int player1SelectedTile, Vector3Int player2SelectedTile)
    {
        
        _player1.gameObject.transform.position = getTileCenterPosition(player1SelectedTile);
        _player1.turnConfirmed = false;
        _player1.currentPosition = player1SelectedTile;
        _player1.selectedPosition = Vector3Int.zero;
        _player1.overlayMap.ClearAllTiles();
        
        _player2.gameObject.transform.position = getTileCenterPosition(player2SelectedTile);
        _player2.turnConfirmed = false;
        _player2.currentPosition = player2SelectedTile;
        _player2.selectedPosition = Vector3Int.zero;
        _player2.overlayMap.ClearAllTiles();

        turnCount++;
    }

    private void SpawnPlayers()
    {
        Transform parentTransform = tilemap.transform.parent;
        
        // Instantiate
        PlayerInput p1Input = PlayerInput.Instantiate(playerPrefab, 1, "Player1", pairWithDevice: Keyboard.current);
        p1Input.gameObject.name = "Player 1";
        p1Input.GetComponent<Player>().SetupPlayer(parentTransform, p1Overlay, tilemap, _mapGenerator.player1Spawn, p1SelectionTile);
        _player1 = p1Input.gameObject.GetComponent<Player>();
        
        // Set Transform and Position
        //p1Input.transform.SetParent(parentTransform);
        //p1Input.gameObject.transform.position = tilemap.CellToWorld(_mapGenerator.player1Spawn) + new Vector3(0.5f, 0.5f, 0);
        
        // Update Position in Player class
        
        //_player1.currentPosition = _mapGenerator.player1Spawn;
        //_player1._overlayMap = GameObject.Find("P1Overlay").GetComponent<Tilemap>();


        PlayerInput p2Input = PlayerInput.Instantiate(playerPrefab, 2, "Player2", pairWithDevice: Keyboard.current);
        p2Input.gameObject.name = "Player 2";
        p2Input.GetComponent<Player>().SetupPlayer(parentTransform, p2Overlay, tilemap, _mapGenerator.player2Spawn, p2SelectionTile);
        _player2 = p2Input.gameObject.GetComponent<Player>();
        
        //p2Input.transform.SetParent(parentTransform);
        //p2Input.gameObject.transform.position = tilemap.CellToWorld(_mapGenerator.player2Spawn) + new Vector3(0.5f, 0.5f, 0);
        

        //_player2.currentPosition = _mapGenerator.player2Spawn;
        //_player2._overlayMap = GameObject.Find("P2Overlay").GetComponent<Tilemap>();

    }

    private Vector3 getTileCenterPosition(Vector3Int tileCoordinate)
    {
        return tilemap.CellToWorld(tileCoordinate) + new Vector3(0.5f, 0.5f, 0);
    }
}
