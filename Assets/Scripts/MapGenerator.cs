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
    //[SerializeField] private Sprite[] tilemapSprites;
    //private List<Tile> _tiles = new List<Tile>();
    [SerializeField] private Tile[] _tiles;

    private int[][] _mapArray;


    // Start is called before the first frame update
    void Start()
    {
        _tilemap = GameObject.Find("Tilemap").GetComponent<Tilemap>();

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
                _tilemap.SetTile(new Vector3Int(xCoord, -yCoord), _tiles[int.Parse(x)]);

                xCoord++;
            }

            yCoord++;
        }
    }

    void CreateTiles()
    {
        
    }
}
