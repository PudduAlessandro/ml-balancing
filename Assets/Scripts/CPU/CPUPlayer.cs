using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class CPUPlayer : MonoBehaviour
{
    // Core gameplay stats
    [Range(0, 100)] public int currentHealth = 100;
    [Range(0, 100)] public int currentFood = 100;
    [Range(0, 100)] public int currentWater = 100;

    public GameController1P gameController;

    public int collectedFood;
    
    // max values in case range attribute doesnt do its thing
    private int _maxHealth = 100, _maxFood = 100,_maxThirst = 100;
    
    
    private GameObject _playerSprite;
    

    private enum MovementDirection
    {
        UP,
        RIGHT,
        DOWN,
        LEFT
    };

    private MovementDirection _movementDirection = MovementDirection.RIGHT;

    // Map related
    public Vector3Int currentPosition;
    private Tilemap _tilemap;

    //private Tile _selectedTile = null;
    private Vector3Int _tempSelectedPosition = Vector3Int.forward;
    public Vector3Int selectedPosition = Vector3Int.forward;

    // UI stuff
    private StatusBarController _healthBar, _foodBar, _waterBar;
    
    // Bot for smarter movement
    private Queue<Vector3Int> _pathToTarget;
    private AStarBotV2 _aStarBot;
    
    
    public void Setup(string playerName, Tilemap map, Vector3Int spawnPosition, GameController1P gameController1P)
    {
        // Set name and GameController
        gameObject.name = playerName;
        gameController = gameController1P;

        // Set tilemap and spawn position
        _tilemap = map;
        gameObject.transform.position = map.CellToWorld(spawnPosition) + new Vector3(0.5f, 0.5f, 0);
        currentPosition = spawnPosition;

        // Set all gameplay values to max if necessary
        //currentHealth = 100;
        //currentHunger = 100;
        //currentThirst = 100;
        
        // Setup UI bars
        SetupStatusBars();
        
        // Color player sprite
        _playerSprite = gameObject.transform.Find("PlayerSprite").gameObject;
        _playerSprite.GetComponent<SpriteRenderer>().color = Color.yellow;
        
        // Make CPU player smaller to make both players visible on the same tile
        _playerSprite.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);

        _aStarBot = GetComponent<AStarBotV2>();
        _aStarBot.tilemap = _tilemap;
        _aStarBot.SetupBot();
    }

    private void SetupStatusBars()
    {
        Transform statusBarsTransform = gameObject.transform.Find("UI Canvas").Find("StatusBars");

        _healthBar = statusBarsTransform.Find("Health Bar").GetComponent<StatusBarController>();
        _foodBar = statusBarsTransform.Find("Hunger Bar").GetComponent<StatusBarController>();
        _waterBar = statusBarsTransform.Find("Thirst Bar").GetComponent<StatusBarController>();
        
        
        _healthBar.SetMaxStatusValue(_maxHealth);
        _foodBar.SetMaxStatusValue(_maxFood);
        _waterBar.SetMaxStatusValue(_maxThirst);
    }

    public Vector3Int CPUMove()
    {
        _aStarBot.currentPos = currentPosition;
        // If there hasn't been a path yet, or if the last path is finished, calculate a new one and move along one step
        if (_pathToTarget == null || _pathToTarget.Count == 0)
        {
            AStarPathFinding();
            return MoveAlongPath();
        }

        // If not looking for food there should be no problem with going to the water
        if (!_aStarBot.targetTileName.Equals("Food")) return MoveAlongPath();
        
        // Check in case of looking for food if the target is still available
        if (IsTargetedFoodStillAvailable())
        {
            return MoveAlongPath();
        }

        // Targeted food isn't available anymore - switch focus to water for now
        // _aStarBot.targetTileName = "WAdjacent";
        AStarPathFinding();
        return MoveAlongPath();
    }

    private void AStarPathFinding()
    {
        // Start position for the path that the bot is going to calculate
        _aStarBot.currentPos = currentPosition;

        // If water is low - look for water, since it never runs out
        if (currentWater <= 20)
        {
            _aStarBot.lockCurrentTargetType = false;
            _aStarBot.targetTileName = "WAdjacent";
        }
        else
        {
            _aStarBot.lockCurrentTargetType = false;
            _aStarBot.targetTileName = "Food";
        }
        // If both values are the same - choose random and lock the decision
        // until something has been collected
        /*else if(currentFood == currentWater)
        {
            if (!_aStarBot.lockCurrentTargetType)
            { 
                var random = Random.Range(0, 2);
                _aStarBot.targetTileName = (random == 1 ? "Food" : "WAdjacent"); 
                _aStarBot.lockCurrentTargetType = true; 
            }
        }*/
        
        _pathToTarget = _aStarBot.FindPath();
    }
    
    // Return the most front Vector3Int of the pathToTarget queue and dequeue it
    private Vector3Int MoveAlongPath()
    {
        return _pathToTarget.Dequeue();
    }
    
    private bool IsTargetedFoodStillAvailable()
    {
        return _tilemap.GetTile(_aStarBot.targetPos).name.Contains("Food") &&
               !_tilemap.GetTile(_aStarBot.targetPos).name.Contains("Used");
    }

    public void UpdateStatus()
    {
        // Update position property after moving within the world
        currentPosition = _tilemap.WorldToCell(transform.position);
        selectedPosition = Vector3Int.zero;
        
        // Using up food and water
        currentFood -= 10;
        currentWater -= 10;
        
        // Keep values between max. value and zero
        currentFood = Mathf.Clamp(currentFood, 0, 100);
        currentWater = Mathf.Clamp(currentWater, 0, 100);
        
        FoodCheck();
        WaterCheck(currentPosition);

        UpdateHealth();

        _healthBar.UpdateStatusBar(currentHealth);
        _foodBar.UpdateStatusBar(currentFood);
        _waterBar.UpdateStatusBar(currentWater);

        TurnPlayerSprite();
    }
    
    private void FoodCheck()
    {
        TileBase currentTile = _tilemap.GetTile(currentPosition);

        if (!currentTile.name.Contains("Food") || currentTile.name.Contains("Used")) return;
        
        currentFood = _maxFood;
        collectedFood++;
    }
    
    private void WaterCheck(Vector3Int tileToCheck)
    {
        List<TileBase> nearbyTiles = new List<TileBase>();

        if (_tilemap.GetTile(tileToCheck + new Vector3Int(0, 1, 0)) != null)
        {
            nearbyTiles.Add(_tilemap.GetTile(tileToCheck + new Vector3Int(0, 1, 0)));
        }
        
        if (_tilemap.GetTile(tileToCheck + new Vector3Int(1, 0, 0)) != null)
        {
            nearbyTiles.Add(_tilemap.GetTile(tileToCheck + new Vector3Int(1, 0, 0)));
        }
        
        if (_tilemap.GetTile(tileToCheck + new Vector3Int(0, -1, 0)) != null)
        {
            nearbyTiles.Add(_tilemap.GetTile(tileToCheck + new Vector3Int(0, -1, 0)));
        }
        
        if (_tilemap.GetTile(tileToCheck + new Vector3Int(-1, 0, 0)) != null)
        {
            nearbyTiles.Add(_tilemap.GetTile(tileToCheck + new Vector3Int(-1, 0, 0)));
        }

        if (nearbyTiles.Any(tile => tile.name.Contains("Water")))
        {
            currentWater = _maxThirst;
        }

    }
    
    private void UpdateHealth()
    {
        // If food or water is empty, take damage per turn - if not regenerate hp
        if (currentFood == 0 || currentWater == 0)
        {
            currentHealth -= 10;
        }
        else
        {
            currentHealth += 10;
        }

        currentHealth = Mathf.Clamp(currentHealth, 0, _maxHealth);
    }
    
    private void TurnPlayerSprite()
    {
        _playerSprite.transform.rotation = _movementDirection switch
        {
            MovementDirection.UP => Quaternion.Euler(0, 0, 90f),
            MovementDirection.DOWN => Quaternion.Euler(0, 0, -90f),
            MovementDirection.LEFT => Quaternion.Euler(0, 0, 180f),
            MovementDirection.RIGHT => Quaternion.Euler(0, 0, 0f),
            _ => _playerSprite.transform.rotation
        };
    }
}
