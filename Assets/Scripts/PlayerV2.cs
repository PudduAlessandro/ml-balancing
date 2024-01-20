using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerV2 : MonoBehaviour
{
    
    // Core gameplay stats
    [Range(0, 100)] public int currentHealth = 100, currentHunger = 100, currentThirst = 100;

    public GameController1P _GameController;

    public int collectedFood;
    
    // max values in case range attribute doesnt do its thing
    private int _maxHealth = 100, _maxHunger = 100,_maxThirst = 100;
    
    
    private GameObject playerSprite;

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
    public Tilemap overlayMap;
    private int _foodRestoreAmount = 20, _thirstRestoreAmount = 20, _healthRestoreAmount = 10;

    //private Tile _selectedTile = null;
    private Vector3Int _tempSelectedPosition = Vector3Int.forward;
    public Vector3Int selectedPosition = Vector3Int.forward;

    // UI stuff
    private StatusBarController healthBar, hungerBar, thirstBar;

    // Setup the player by setting all necessary properties
    public void Setup(string playerName, Transform parentTransform, Tilemap map, Vector3Int spawnPosition, GameController1P gameController)
    {
        // Set name and GameController
        gameObject.name = playerName;
        _GameController = gameController;

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
        playerSprite = gameObject.transform.Find("PlayerSprite").gameObject;
        playerSprite.GetComponent<SpriteRenderer>().color = Color.red;
    }

    private void SetupStatusBars()
    {
        Transform statusBarsTransform = gameObject.transform.Find("UI Canvas").Find("StatusBars");

        healthBar = statusBarsTransform.Find("Health Bar").GetComponent<StatusBarController>();
        hungerBar = statusBarsTransform.Find("Hunger Bar").GetComponent<StatusBarController>();
        thirstBar = statusBarsTransform.Find("Thirst Bar").GetComponent<StatusBarController>();
        
        
        healthBar.SetMaxStatusValue(_maxHealth);
        hungerBar.SetMaxStatusValue(_maxHunger);
        thirstBar.SetMaxStatusValue(_maxThirst);
    }

    private void HandleInput(InputAction.CallbackContext value)
    {

        if (!value.performed) return;
        switch (value.action.name)
        {
            case "Select Above Tile":
            {
                _tempSelectedPosition = currentPosition + new Vector3Int(0, 1, 0);
                _movementDirection = MovementDirection.UP;
                break;
            }
            case "Select Below Tile":
            {
                _tempSelectedPosition = currentPosition + new Vector3Int(0, -1, 0);
                _movementDirection = MovementDirection.DOWN;
                break;
            }
            case "Select Left Tile":
            {
                _tempSelectedPosition = currentPosition + new Vector3Int(-1, 0, 0);
                _movementDirection = MovementDirection.LEFT;
                break;
            }
            case "Select Right Tile":
            {
                _tempSelectedPosition = currentPosition + new Vector3Int(1, 0, 0);
                _movementDirection = MovementDirection.RIGHT;
                break;
            }

        }
    }

}
