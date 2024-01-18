using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    private Tilemap _tilemap;
    public Vector3Int player1Spawn, player2Spawn;

    [SerializeField] public Tile[] tiles;

    private int[][] _mapArray;

    private string mapString;
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
        "0 0 0 5 0 1,2 2 1 1 3 1,0 0 0 1 2 3,0 1 0 2 0 3,0 0 2 4 2 1,3 0 0 0 0 0", //MOUNTAINBLOCK2_0_5
        "0 0 0 5 2 1,2 2 1 1 3 1,0 0 0 1 2 3,0 1 0 0 0 3,0 0 2 4 2 1,3 0 0 0 0 0", //MOUNTAINBLOCK2_0_0
        "2 3 0 2 0 2,0 0 0 2 2 0,2 5 0 2 0 2,2 0 0 1 1 0,2 0 1 1 1 4,2 1 2 0 2 0"  // TEST MAP
    };

    // TODO: Add parameter for map file
    public Tilemap BuildMap()
    {
        _tilemap = GameObject.Find("MapTilemap").GetComponent<Tilemap>();

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

        string map = mapStrings[(int)selectedMap];

        foreach (string y in map.Split(","))
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

        _tilemap.CompressBounds();
        return _tilemap;
    }

    private IEnumerator GetFile(string path)
    {
        var webRequest = UnityWebRequest.Get(path);

        yield return webRequest.SendWebRequest();

        if (webRequest.result != UnityWebRequest.Result.Success) yield break;
        Debug.Log(webRequest.downloadHandler.text);
        mapString = webRequest.downloadHandler.text;
    }


    void CreateTiles()
    {
        
    }
}
