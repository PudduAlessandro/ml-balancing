using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tile[] tiles;
    [SerializeField] private TextMeshProUGUI errorLabel;
    
    bool _waterExists;
    bool _foodExists;
    bool _onePlayer;
    bool _oneOpponent;
    bool _validPathExists;
    
    private void Awake()
    {
        errorLabel.enabled = false;
        if (string.IsNullOrEmpty(Application.absoluteURL))
        {
            return;
        }

        var uri = new Uri(Application.absoluteURL);

        if (uri.AbsoluteUri.Split("/").Length > 1)
        {
            string level = uri.GetComponents(UriComponents.Query, UriFormat.SafeUnescaped);

            if (!string.IsNullOrEmpty(level))
            {
                var value = level.Split('=')[1];

                if (Regex.IsMatch(value, @"^[0-5]{36}$"))
                {
                    Debug.Log($"Parameter: {value}) is valid");

                    string mapString = DecodeMapString(value);

                    if (BuildAndValidateMap(mapString))
                    {
                       PlayerPrefs.SetString("map", mapString);
                       SceneManager.LoadScene("GameScene"); 
                    }
                    else
                    {
                        errorLabel.text = "The entered level was invalid!";
                        errorLabel.enabled = true;
                    }
                    
                    
                }
                else
                {
                    errorLabel.text = "The entered level was invalid!";
                    errorLabel.enabled = true;
                }
            }
        }
    }

    private bool BuildAndValidateMap(string mapString)
    {
        int xCoord = 0;
        int yCoord = 0;
        
        foreach (string y in mapString.Split(","))
        {
            xCoord = 0;
            foreach (string x in y.Split(" "))
            {
                tilemap.SetTile(new Vector3Int(xCoord, -yCoord), tiles[int.Parse(x)]);
                xCoord++;
            }

            yCoord++;
        }
        tilemap.CompressBounds();

        return ValidateMap();
    }

    private string DecodeMapString(string mapString)
    {
        return Regex.Replace(mapString, @"(\d{1})(\d{1})(\d{1})(\d{1})(\d{1})(\d{1})", "$1 $2 $3 $4 $5 $6,").TrimEnd(',');
    }
    
    private bool ValidateMap()
    {
        BoundsInt bounds = tilemap.cellBounds;

        _waterExists = false;
        _foodExists = false;
        _onePlayer = false;
        _oneOpponent = false;
        _validPathExists = false;
        
        Vector3Int player1Spawn = Vector3Int.zero;
        Vector3Int player2Spawn = Vector3Int.zero;

        var playerTiles = 0;
        var opponentTiles = 0;

        foreach (var position in bounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(position.x, position.y, position.z);
            TileBase tile = tilemap.GetTile(localPlace);
            
            if (tile.name.Equals("Water"))
            {
                _waterExists = true;
            } 
            if (tile.name.Equals("Food"))
            {
                _foodExists = true;
            }

            if (tile.name.Equals("Player_Tile"))
            {
                player1Spawn = localPlace;
                playerTiles++;
            }
            
            if (tile.name.Equals("Opponent_Tile"))
            {
                player2Spawn = localPlace;
                opponentTiles++;
            }
        }

        if (playerTiles == 1)
        {
            _onePlayer = true;
            
        }

        if (opponentTiles == 1)
        {
            _oneOpponent = true;
        }

        if (_onePlayer && _oneOpponent)
        {
            _validPathExists = FindPath(player1Spawn, player2Spawn);
        }

        return _onePlayer && _oneOpponent && _waterExists && _foodExists && _validPathExists;
        
    }

    private bool FindPath(Vector3Int startPosition, Vector3Int targetTilePosition)
    {
        HashSet<Vector3Int> closedSet = new HashSet<Vector3Int>();
        HashSet<Vector3Int> openSet = new HashSet<Vector3Int>() { startPosition };
        Dictionary<Vector3Int, Vector3Int> cameFrom = new Dictionary<Vector3Int, Vector3Int>();
        Dictionary<Vector3Int, float> gScore = new Dictionary<Vector3Int, float> { { startPosition, 0 } };
        Dictionary<Vector3Int, float> fScore = new Dictionary<Vector3Int, float> { { startPosition, HeuristicCostEstimate(startPosition, targetTilePosition) } };

        while (openSet.Count > 0)
        {
            Vector3Int current = GetLowestFScore(openSet, fScore);
            if (current == targetTilePosition)
                return true;

            openSet.Remove(current);
            closedSet.Add(current);

            foreach (Vector3Int neighbor in GetNeighbors(current))
            {
                if (closedSet.Contains(neighbor) || !IsTileWalkable(neighbor, tilemap))
                    continue;
                
                float tentativeGScore = gScore[current] + 1; // Assuming all tile costs are the same (1)
                if (!openSet.Contains(neighbor) || tentativeGScore < gScore[neighbor])
                {
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeGScore;
                    fScore[neighbor] = gScore[neighbor] + HeuristicCostEstimate(neighbor, targetTilePosition);

                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
        }

        return false;
    }
    
    static float HeuristicCostEstimate(Vector3Int start, Vector3Int goal)
    {
        return Vector3Int.Distance(start, goal);
    }

    static Vector3Int GetLowestFScore(HashSet<Vector3Int> openSet, Dictionary<Vector3Int, float> fScore)
    {
        float lowestFScore = float.MaxValue;
        Vector3Int lowestFScoreNode = Vector3Int.zero;

        foreach (Vector3Int node in openSet)
        {
            if (fScore.ContainsKey(node) && fScore[node] < lowestFScore)
            {
                lowestFScore = fScore[node];
                lowestFScoreNode = node;
            }
        }

        return lowestFScoreNode;
    }
    
    static Queue<Vector3Int> ReconstructPath(Dictionary<Vector3Int, Vector3Int> cameFrom, Vector3Int current)
    {
        Queue<Vector3Int> path = new Queue<Vector3Int>();
        path.Enqueue(current);

        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            path.Enqueue(current);
        }

        Queue<Vector3Int> reversedPath= new Queue<Vector3Int>(path.Reverse());

        return new Queue<Vector3Int>(reversedPath);
    }
    
    private List<Vector3Int> GetNeighbors(Vector3Int current)
    {
        List<Vector3Int> neighbors = new List<Vector3Int>
        {
            current + new Vector3Int(1, 0, 0), // Right
            current + new Vector3Int(-1, 0, 0), // Left
            current + new Vector3Int(0, 1, 0), // Up
            current + new Vector3Int(0, -1, 0) // Down
        };

        return neighbors;
    }
    
    private bool IsTileWalkable(Vector3Int position, Tilemap tilemap)
    {
        if (tilemap.GetTile(position) == null) return false;
        
        return !tilemap.GetTile(position).name.Contains("Wall") && !tilemap.GetTile(position).name.Contains("Water");
    }

    public void MenuButtonInput(int button)
    {
        switch (button)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
        }
    }
    
    
    
}
