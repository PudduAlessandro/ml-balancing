using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    // Core gameplay stats
    [Range(0, 100)] public int currentHealth = 100, currentHunger = 100, currentThirst = 100;
    
    // max values in case range attribute doesnt do its thing
    private int _maxHealth = 100, _maxHunger = 100,_maxThirst = 100;

    // Misc stats (movement, interactions)
    [SerializeField] private readonly float _moveSpeed = 3;
    [FormerlySerializedAs("_rotationSpeed")] [SerializeField] private float rotationSpeed = 3;
    private Vector2 _movementDirection;

    // Controls related
    public bool isPlayerTwo;
    private PlayerInput _playerInput;
    //private PlayerControls _playerControls = null;

    // Map related
    public Vector3Int currentPosition;
    private Tilemap _tilemap;
    public Tilemap overlayMap;
    private int _foodRestoreAmount = 20, _thirstRestoreAmount = 20, _healthRestoreAmount = 10;

    //private Tile _selectedTile = null;
    private Vector3Int _tempSelectedPosition = Vector3Int.zero;
    public Vector3Int selectedPosition = Vector3Int.zero;
    private Tile _selectionTile;
    public bool turnConfirmed = false;
    
    // UI stuff
    private StatusBarController healthBar, hungerBar, thirstBar;


    // Start is called before the first frame update
    void Start()
    {
        _tilemap = GameObject.Find("MapTilemap").GetComponent<Tilemap>();
        //_overlayMap = GameObject.Find("Overlay").GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetupPlayer(Transform parentTransform, Tilemap overlayTilemap, Tilemap map, Vector3Int spawnPosition, Tile playerSelectionTile)
    {
        gameObject.transform.parent = parentTransform;
        gameObject.transform.position = map.CellToWorld(spawnPosition) + new Vector3(0.5f, 0.5f, 0);
        currentPosition = spawnPosition;
        overlayMap = overlayTilemap;
        _selectionTile = playerSelectionTile;
        
        currentHealth = 100;
        currentHunger = 100;
        currentThirst = 100;

        SetupStatusBars();
    }

    public void Selection(InputAction.CallbackContext value)
    {
        if (!value.performed) return;
        switch (value.action.name)
        {
            case "Select Above Tile":
            {
                _tempSelectedPosition = currentPosition + new Vector3Int(0, 1, 0);
                break;
            }
            case "Select Below Tile":
            {
                _tempSelectedPosition = currentPosition + new Vector3Int(0, -1, 0);
                break;
            } 
            case "Select Left Tile":
            {
                _tempSelectedPosition = currentPosition + new Vector3Int(-1, 0, 0);
                break;
            } 
            case "Select Right Tile":
            {
                _tempSelectedPosition = currentPosition + new Vector3Int(1, 0, 0);
                break;
            }
            case "Confirm":
            {
                if (selectedPosition != Vector3Int.forward)
                {
                    turnConfirmed = true;
                }
                return;
            }
            case "Cancel":
            {
                if (turnConfirmed)
                {
                    turnConfirmed = false;
                }
                return;
            }
        }

        if (CheckTile(_tempSelectedPosition) != null)
        {
            overlayMap.ClearAllTiles();
            selectedPosition = _tempSelectedPosition;
            overlayMap.SetTile(selectedPosition, _selectionTile);
        };
    }

    private TileBase CheckTile(Vector3Int tileToCheckPos)
    {

        if (!CheckForOutOfBounds(tileToCheckPos))
        {
           TileBase tileBase = _tilemap.GetTile(tileToCheckPos);
           if (tileBase.name.Equals("1_GRASS") || tileBase.name.Equals("3_FOREST") || tileBase.name.Equals("5_P1SPAWN") || tileBase.name.Equals("6_P2SPAWN") || tileBase.name.Equals("7_FORESTUSED"))
           {
               return tileBase;
           }
        }
        return null;
    }

    private bool CheckForOutOfBounds(Vector3Int tileToCheckPos)
    {
        return (tileToCheckPos.x is > 5 or < 0 || tileToCheckPos.y is > 0 or < -5);
    }

    public void FinishTurn(Vector3Int newPosition)
    {
        turnConfirmed = false;
        currentPosition = newPosition;
        selectedPosition = Vector3Int.forward;
        overlayMap.ClearAllTiles();

        currentHunger -= 10;
        currentThirst -= 10;
        
        FoodCheck();
        WaterCheck(currentPosition);

        currentHunger = Mathf.Clamp(currentHunger, 0, 100);
        currentThirst = Mathf.Clamp(currentThirst, 0, 100);

        UpdateHealth();
        
        healthBar.UpdateStatusBar(currentHealth);
        hungerBar.UpdateStatusBar(currentHunger);
        thirstBar.UpdateStatusBar(currentThirst);
        
    }

    private void UpdateHealth()
    {
        if (currentHunger == 0 || currentThirst == 0)
        {
            currentHealth -= 10;
        }
        else
        {
            currentHealth += _healthRestoreAmount;
        }

        currentHealth = Mathf.Clamp(currentHealth, 0, _maxHealth);
        

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

        if (nearbyTiles.Any(tile => tile.name.Equals("4_WATER")))
        {
            currentThirst += _thirstRestoreAmount;
        }

    }

    private void FoodCheck()
    {
        TileBase currentTile = CheckTile(currentPosition);

        if (currentTile.name.Equals("3_FOREST"))
        {
            Eat();
        }
    }

    private void Eat()
    {
        if (currentHunger + _foodRestoreAmount >= _maxHunger)
        {
            currentHunger = _maxHunger;
        }
        else
        {
            currentHunger += _foodRestoreAmount;
        }
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
}
