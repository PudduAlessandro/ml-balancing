using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class MapGenerator : MonoBehaviour
{
    private Tilemap _tilemap;
    public Vector3Int player1Spawn, player2Spawn;
    public GameObject playerPrefab;

    [SerializeField] private Tile[] tiles;

    private int[][] _mapArray;


    // Start is called before the first frame update
    void Start()
    {
        _tilemap = GameObject.Find("MapTilemap").GetComponent<Tilemap>();

        BoundsInt bounds = _tilemap.cellBounds;
        TileBase[] allTiles = _tilemap.GetTilesBlock(bounds);
        
        CreateTiles();
        LoadMap();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadMap()
    {
        BoundsInt bounds = _tilemap.cellBounds;
        TileBase[] allTiles = _tilemap.GetTilesBlock(bounds);
        
        string mapString = File.ReadAllText("Assets/Ressources/map2.txt");

        int xCoord = 0;
        int yCoord = 0;

        foreach (string y in mapString.Split(","))
        {
            xCoord = 0;
            foreach (string x in y.Split(" "))
            {
                _tilemap.SetTile(new Vector3Int(xCoord, -yCoord), tiles[int.Parse(x)]);
                
                if (int.Parse(x) == 4)
                {
                    player1Spawn = new Vector3Int(xCoord, -yCoord, 0);
                } else if (int.Parse(x) == 5)
                {
                    player2Spawn = new Vector3Int(xCoord, -yCoord, 0);
                }

                xCoord++;
            }

            yCoord++;
        }

        SpawnPlayers();
    }

    private void SpawnPlayers()
    {
        GameObject player1 = Instantiate(playerPrefab, _tilemap.transform.parent.transform);
        player1.transform.position = _tilemap.CellToWorld(player1Spawn) + new Vector3(0.5f, 0.5f, 0);
        
        GameObject player2 = Instantiate(playerPrefab, _tilemap.transform.parent.transform);
        player2.transform.position = _tilemap.CellToWorld(player2Spawn) + new Vector3(0.5f, 0.5f, 0);;
    }

    void CreateTiles()
    {
        
    }
}
