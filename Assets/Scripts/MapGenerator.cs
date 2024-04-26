using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    private Tilemap _tilemap;
    public Vector3Int player1Spawn, player2Spawn;

    [SerializeField] public Tile[] tiles;
    private TileBase waterAdjacentGrass, waterAdjacentFood, waterAdjacentFoodUsed;

    private int[][] _mapArray;

    public string mapString;
    private string tempMapString = "";

    [SerializeField] public Map selectedMap;
    
    public enum Map {
        WATERSWAP_UNBALANCED,
        WATERSWAP_BALANCED,
        MOUNTAINBLOCK_UNBALANCED,
        MOUNTAINBLOCK_BALANCED,
        EQUALIZELINE_0_2,
        EQUALIZELINE_0_5,
        MOUNTAINBLOCK2_0_5,
        MOUNTAINBLOCK2_0_0,
        TEST_MAP
    }
    
    // MAPS
    private string[] mapStrings =
    {
        "0 0 3 0 2 1,0 0 0 2 0 2,0 2 0 0 1 0,2 0 0 5 0 0,1 0 0 2 1 0,1 0 0 4 1 0", //WATERSWAP_UNBALANCED
        "0 0 0 0 2 1,0 0 0 2 0 2,0 2 0 0 1 0,2 0 0 5 3 0,1 0 0 2 1 0,1 0 0 4 1 0", //WATERSWAP_BALANCED
        
        "0 0 0 5 0 1,2 2 1 1 3 1,0 0 0 1 2 3,0 1 0 2 0 3,0 0 2 4 2 1,3 0 0 0 0 0", //MOUNTAINBLOCK_UNBALANCED
        "0 0 0 5 2 1,2 2 1 1 3 1,0 0 0 1 2 3,0 1 0 0 0 3,0 0 2 4 2 1,3 0 0 0 0 0", //MOUNTAINBLOCK_BALANCED
        
        "0 2 2 0 1 0,0 0 0 1 3 0,1 1 2 2 3 3,3 2 0 2 5 0,0 1 1 2 1 1,0 3 0 0 4 1", //EQUALIZELINE_0_2
        "1 2 2 0 1 2,0 0 0 1 3 0,1 0 2 2 3 3,3 2 0 0 5 0,1 1 0 1 1 1,2 3 0 0 4 0", //EQUALIZELINE_0_5
        
        "2 1 0 5 1 1,0 2 1 0 3 1,0 2 0 1 0 3,0 1 0 0 0 4,0 0 2 3 2 0,3 0 0 0 0 2", //MOUNTAINBLOCK2_UNBALANCED
        "0 0 0 5 2 1,2 2 1 1 3 1,0 0 0 1 2 3,0 1 0 0 0 3,0 0 2 4 2 1,3 0 0 0 0 0", //MOUNTAINBLOCK2_BALANCED = MOUNTAINBLOCK_BALANCED
        
        "2 3 0 2 0 2,0 0 0 2 2 0,2 5 0 2 0 2,2 0 0 1 1 0,2 0 1 1 1 4,2 1 2 0 2 0"  // TEST MAP
    };

    // TODO: Add parameter for map file
    public Tilemap BuildMap()
    {
        GameObject tilemapObject = GameObject.Find("MapTilemap");
        _tilemap = tilemapObject.GetComponent<Tilemap>();

        BoundsInt bounds = _tilemap.cellBounds;
        TileBase[] allTiles = _tilemap.GetTilesBlock(bounds);

        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            //StartCoroutine(GetFile(Application.streamingAssetsPath + "/Maps/map2.txt"));

        }
        else
        {
           //mapString = File.ReadAllText(Application.streamingAssetsPath + "/Maps/map2.txt"); 
        }
        
        

        int xCoord = 0;
        int yCoord = 0;

        //string map = mapStrings[(int)selectedMap];

        foreach (string y in mapString.Split(","))
        {
            xCoord = 0;
            foreach (string x in y.Split(" "))
            {
                _tilemap.SetTile(new Vector3Int(xCoord, -yCoord), tiles[int.Parse(x)]);
                //UpdateTileName(_tilemap, new Vector3Int(xCoord, -yCoord), int.Parse(x));
                
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

        _tilemap.CompressBounds();
        MarkWaterAdjacentTiles(_tilemap);
        
        return _tilemap;
    }
    

    private void UpdateTileName(Tilemap tilemap, Vector3Int pos, int tileType)
    {
        string newName = "";

        switch (tileType)
        {
            case 0: 
            case 4:
            case 5: newName = "Grass";
                break;
            case 1: newName = "Food";
                break;
            case 2: newName = "Wall";
                break;
            case 3: newName = "Water";
                break;
            case 6: newName = "FoodUsed";
                break;
        }

        tilemap.GetTile(pos).name = newName;
    }

    private void MarkWaterAdjacentTiles(Tilemap tilemap)
    {
        foreach (Vector3Int pos in tilemap.cellBounds.allPositionsWithin)
        {
            if (!tilemap.GetTile(pos).name.Contains("Water")) continue;
            
            foreach (Vector3Int neighbor in GetNeighbors(tilemap, pos))
            {
                if (tilemap.GetTile(neighbor) != null && !tilemap.GetTile(neighbor).name.Contains("WAdjacent") && !tilemap.GetTile(neighbor).name.Contains("Wall"))
                {
                    switch (tilemap.GetTile(neighbor).name)
                    {
                        case "Grass": tilemap.SetTile(neighbor, tiles[7]);
                            break;
                        case "Food": tilemap.SetTile(neighbor, tiles[8]);
                            break;
                        case "FoodUsed": tilemap.SetTile(neighbor, tiles[9]);
                            break;
                    }
                    
                }
            }
        }
    }


    Vector3Int[] GetNeighbors(Tilemap tilemap, Vector3Int pos)
    {
        Vector3Int above = pos + Vector3Int.up;
        Vector3Int right = pos + Vector3Int.right;
        Vector3Int below = pos + Vector3Int.down;
        Vector3Int left = pos + Vector3Int.left;
        
        Vector3Int[] neighborArray = new []{above, right, below, left};

        return neighborArray;
    }
}
