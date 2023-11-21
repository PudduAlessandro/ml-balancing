using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.iOS;
using UnityEngine.Tilemaps;

public class GameController : MonoBehaviour
{
    public GameObject playerPrefab;
    private Player _player1, _player2;
    
    // Map related
    private MapGenerator mapGenerator;
    [SerializeField] private Tilemap _tilemap;

    // Turn related
    private Vector3Int _player1Selection, _player2Selection;
    private bool _player1Confirm = false, _player2Confirm = false;
    
    //Misc. / Testing related
    private Vector2 _cursorLocation;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        mapGenerator = gameObject.GetComponent<MapGenerator>();

        _tilemap = mapGenerator.BuildMap();
        SpawnPlayers();
    }

    // Update is called once per frame
    void Update()
    {
        if (_player1Confirm && _player2Confirm)
        {
            GameLoop(_player1Selection, _player1Selection);
        }
    }

    private void FixedUpdate()
    {
        
    }

    private void GameLoop(Vector3Int player1SelectedTile, Vector3Int player2SelectedTile)
    {
        
    }

    private void SpawnPlayers()
    {
        Transform parentTransform = _tilemap.transform.parent;
        
        // Instantiate
        PlayerInput p1Input = PlayerInput.Instantiate(playerPrefab, 1, "Player1", pairWithDevice: Keyboard.current);
        
        // Set Transform and Position
        p1Input.transform.SetParent(parentTransform);
        p1Input.gameObject.transform.position = _tilemap.CellToWorld(mapGenerator.player1Spawn) + new Vector3(0.5f, 0.5f, 0);
        
        // Update Position in Player class
        _player1 = p1Input.gameObject.GetComponent<Player>();
        _player1.currentPosition = mapGenerator.player1Spawn;
        
        
        PlayerInput p2Input = PlayerInput.Instantiate(playerPrefab, 2, "Player2", pairWithDevice: Keyboard.current);
        
        p2Input.transform.SetParent(parentTransform);
        p2Input.gameObject.transform.position = _tilemap.CellToWorld(mapGenerator.player2Spawn) + new Vector3(0.5f, 0.5f, 0);
        
        _player2 = p2Input.gameObject.GetComponent<Player>();
        _player2.currentPosition = mapGenerator.player2Spawn;
        

    }
}
