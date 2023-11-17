using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameController : MonoBehaviour
{
    public GameObject playerPrefab;
    //private Player _player1, _player2;
    private MapGenerator mapGenerator;
    [SerializeField] private Tilemap _tilemap;
    private Vector2 _cursorLocation;
    

    private Vector3Int _player1Selection, _player2Selection;
    
    
    
    
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
        
    }

    private void FixedUpdate()
    {
        
    }

    private void GameLoop()
    {
        if(_player1Selection != null && _player2Selection != null)
        {
            //Run player turns


        }
    }

    private void SpawnPlayers()
    {
        GameObject player1 = Instantiate(playerPrefab, _tilemap.transform.parent.transform);
        player1.transform.position = _tilemap.CellToWorld(mapGenerator.player1Spawn) + new Vector3(0.5f, 0.5f, 0);

        GameObject player2 = Instantiate(playerPrefab, _tilemap.transform.parent.transform);
        player2.transform.position = _tilemap.CellToWorld(mapGenerator.player2Spawn) + new Vector3(0.5f, 0.5f, 0); ;
    }
}
