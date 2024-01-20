using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class PlayerV2 : MonoBehaviour
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

    // Controls related
    private PlayerInput _playerInput;

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
    public Vector3Int selectedPosition = Vector3Int.forward;

    // UI stuff
    private StatusBarController _healthBar, _foodBar, _waterBar;

    // Setup the player by setting all necessary properties
    public void Setup(string playerName, Transform parentTransform, Tilemap map, Vector3Int spawnPosition, GameController1P gameController1P)
    {
        // Set name and GameController
        gameObject.name = playerName;
        gameController = gameController1P;

        // Set tilemap and spawn position
        _tilemap = map;
        gameObject.transform.parent = parentTransform;
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
        _playerSprite.GetComponent<SpriteRenderer>().color = Color.red;
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

    // Handles player input from the PlayerInput component and chooses a direction based on the pressed key
    public void HandleInput(InputAction.CallbackContext value)
    {

        if (!value.performed) return;
        switch (value.action.name)
        {
            case "Up":
            {
                selectedPosition = currentPosition + new Vector3Int(0, 1, 0);
                _movementDirection = MovementDirection.UP;
                break;
            }
            case "Down":
            {
                selectedPosition = currentPosition + new Vector3Int(0, -1, 0);
                _movementDirection = MovementDirection.DOWN;
                break;
            }
            case "Left":
            {
                selectedPosition = currentPosition + new Vector3Int(-1, 0, 0);
                _movementDirection = MovementDirection.LEFT;
                break;
            }
            case "Right":
            {
                selectedPosition = currentPosition + new Vector3Int(1, 0, 0);
                _movementDirection = MovementDirection.RIGHT;
                break;
            }

        }
        
        if (IsTileValid(selectedPosition))
        {
            gameController.UpdateGameStatus(selectedPosition);
        } 
    }

    // Check if the chosen tile is valid to move on
    private bool IsTileValid(Vector3Int tempSelectedPosition)
    {
        TileBase tileToCheck = _tilemap.GetTile(tempSelectedPosition);

        if (tileToCheck == null) return false;
        
        return !tileToCheck.name.Contains("Wall") && !tileToCheck.name.Contains("Water");
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

    // If standing on food tile fill the food up to the max
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
