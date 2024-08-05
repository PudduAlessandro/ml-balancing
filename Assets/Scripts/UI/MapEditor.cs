using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class MapEditor : MonoBehaviour
{
    private Tilemap _tilemap;
    private Vector3Int _player1Spawn, _player2Spawn;
    [SerializeField] public Tile[] tiles;
    private Camera mainCamera;
    private int selectedTile = 0;
    public TextMeshProUGUI selectedTileText;
    public TMP_InputField mapCodeField;

    public GameObject playerPrefab;
    public GameObject cpuPrefab;
    private GameObject playerObject, cpuObject;
    public Button copyMapStringButton;
    public Button copyURLButton;
    
    
    private bool _validPathExists = false;
    private bool _waterExists = false;
    private bool _foodExists = false;
    private bool _mapIsValid = false;
    private bool _onePlayer = true;
    private bool _oneOpponent = true;
    
    
    void Start()
    {
        mainCamera = Camera.main;
        BuildMap();
    }
    
    private void Update()
    {
        if (!Mouse.current.leftButton.wasPressedThisFrame) return;
        
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector3Int cellPos = _tilemap.WorldToCell(mouseWorldPos);
        
        if (_tilemap.cellBounds.Contains(cellPos))
        {
            TileBase clickedTile = _tilemap.GetTile(cellPos);
            if (clickedTile != null)
            {
                _tilemap.SetTile(cellPos, tiles[selectedTile]);

                ValidateMap();
            }
        }
    }

    private void ValidateMap()
    {
        BoundsInt bounds = _tilemap.cellBounds;
        TileBase[] tiles = _tilemap.GetTilesBlock(bounds);

        _waterExists = false;
        _foodExists = false;
        _onePlayer = false;
        _oneOpponent = false;

        int playerTiles = 0;
        int opponentTiles = 0;

        foreach (var position in bounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(position.x, position.y, position.z);
            TileBase tile = _tilemap.GetTile(localPlace);
            
            switch (tile.name)
            {
                case "Water":
                    _waterExists = true;
                    break;
                case "Food":
                    _foodExists = true;
                    break;
                case "Player_Tile":
                    playerTiles++;
                    _player1Spawn = localPlace;
                    break;
                case "Opponent_Tile":
                    opponentTiles++;
                    _player2Spawn = localPlace;
                    break;
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
            _validPathExists = FindPath(_player1Spawn, _player2Spawn);
        }

        if (_onePlayer)
        {
            if (_oneOpponent)
            {
                if (_waterExists)
                {
                    if (_foodExists)
                    {
                        if (_validPathExists)
                        {
                            var mapStringOutput = "";
            
                            for (int y = bounds.yMax - 1; y >= bounds.yMin; y--)
                            {
                                for (int x = bounds.xMin; x < bounds.xMax; x++)
                                {
                                    Vector3Int currentPos = new Vector3Int(x, y);
                                    TileBase currentTile = _tilemap.GetTile(currentPos);

                                    int tileValue = 0;

                                    switch (currentTile.name)
                                    {
                                        case "Grass":
                                        {
                                            tileValue = 0;
                                            break;
                                        }
                                        case "Food":
                                        {
                                            tileValue = 1;
                                            break;
                                        }
                                        case "Wall":
                                        {
                                            tileValue = 2;
                                            break;
                                        }
                                        case "Water":
                                        {
                                            tileValue = 3;
                                            break;
                                        }
                                        case "Player_Tile":
                                        {
                                            tileValue = 4;
                                            break;
                                        }
                                        case "Opponent_Tile":
                                        {
                                            tileValue = 5;
                                            break;
                                        }
                                    }
                                    mapStringOutput += tileValue;
                                }

                            }
                            UpdateMapString(mapStringOutput, true);
                        }
                        else
                        {
                            UpdateMapString("No valid path between both players!", false);
                        }
                    }
                    else
                    {
                        UpdateMapString("No food tiles present!", false);
                    }
                }
                else
                {
                    UpdateMapString("No water tiles present!", false);
                }
            }
            else
            {
                UpdateMapString("Too many or no opponent tiles placed!", false);
            }
        }
        else
        {
            UpdateMapString("Too many or no player tiles placed!", false);
        }
    }

    private void UpdateMapString(string text, bool isValid)
    {
        mapCodeField.text = text;

        if (isValid)
        {
            mapCodeField.textComponent.color = Color.black;
            copyMapStringButton.interactable = true;
            copyURLButton.interactable = true;
        }
        else
        {
            mapCodeField.textComponent.color = Color.red;
            copyMapStringButton.interactable = false;
            copyURLButton.interactable = false;
        }
        
    }


    public void SelectTile(InputAction.CallbackContext value)
    {
        if (!value.performed) return;
        selectedTile = value.action.name switch
        {
            "SelectGrass" => 0,
            "SelectWater" => 1,
            "SelectWall" => 2,
            "SelectFood" => 3,
            "SelectPlayer" => 4,
            "SelectOpponent" => 5,
            _ => selectedTile
        };

        string tileText = (selectedTile + 1).ToString();
        selectedTileText.text = tileText;
    }

    void BuildMap()
    {

        string defaultMapString = "4 0 0 0 0 2,0 0 0 0 0 0,0 0 1 1 0 0,0 0 1 1 0 0,0 0 0 0 0 0,3 0 0 0 0 5";
        
        _tilemap = GameObject.Find("MapTilemap").GetComponent<Tilemap>();
    
        BoundsInt bounds = _tilemap.cellBounds;
        TileBase[] allTiles = _tilemap.GetTilesBlock(bounds);
    
        int xCoord = 0;
        int yCoord = 0;
    
        //string map = mapStrings[(int)selectedMap];
    
        foreach (string y in defaultMapString.Split(","))
        {
            xCoord = 0;
            foreach (string x in y.Split(" "))
            {
                _tilemap.SetTile(new Vector3Int(xCoord, -yCoord), tiles[int.Parse(x)]);
                    

                if (int.Parse(x) == 4)
                {
                    _player1Spawn = new Vector3Int(xCoord, -yCoord, 0);
                } else if (int.Parse(x) == 5)
                {
                    _player2Spawn = new Vector3Int(xCoord, -yCoord, 0);
                }
    
                xCoord++;
            }
    
            yCoord++;
        }
    
        _tilemap.CompressBounds();
        ValidateMap();
    
    
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
                if (closedSet.Contains(neighbor) || !IsTileWalkable(neighbor, _tilemap))
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
    
    static List<Vector3Int> GetNeighbors(Vector3Int current)
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
    
    static bool IsTileWalkable(Vector3Int position, Tilemap tilemap)
    {
        if (tilemap.GetTile(position) == null) return false;
        
        return !tilemap.GetTile(position).name.Contains("Wall") && !tilemap.GetTile(position).name.Contains("Water");
    }

    public void SetSelectedTile(int value)
    {
        selectedTile = value;

        selectedTileText.text = selectedTile switch
        {
            0 => "Grass",
            1 => "Wall",
            2 => "Food",
            3 => "Water",
            4 => "Player",
            5 => "Opponent",
            _ => selectedTileText.text
        };
    }

}
