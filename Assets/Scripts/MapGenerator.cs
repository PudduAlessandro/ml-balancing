using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    private Tilemap _tilemap;
    public Vector3Int player1Spawn, player2Spawn;

    [SerializeField] public Tile[] tiles;

    private int[][] _mapArray;

    private string mapString;
    private string tempMapString = "2 3 0 2 0 2,0 0 0 2 2 0,2 5 0 2 0 2,2 0 0 1 1 0,2 0 1 1 1 4,2 1 2 0 2 0";


    // Start is called before the first frame update
    private void Start()
    {
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

        foreach (string y in tempMapString.Split(","))
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
