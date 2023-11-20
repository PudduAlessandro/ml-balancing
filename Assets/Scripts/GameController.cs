using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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
        //_player1 = GameObject.Find("Player1").GetComponent<Player>();
        //_player2 = GameObject.Find("Player2").GetComponent<Player>();

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
        
        GameObject player1 = Instantiate(playerPrefab, parentTransform);
        player1.transform.position = _tilemap.CellToWorld(mapGenerator.player1Spawn) + new Vector3(0.5f, 0.5f, 0);

        GameObject player2 = Instantiate(playerPrefab, parentTransform);
        player2.transform.position = _tilemap.CellToWorld(mapGenerator.player2Spawn) + new Vector3(0.5f, 0.5f, 0);
        player2.GetComponent<Player>().isPlayerTwo = true;
        player2.GetComponent<PlayerInput>().SwitchCurrentControlScheme("Player2");
    }
}
