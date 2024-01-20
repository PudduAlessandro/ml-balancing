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

    public GameController _GameController;

    public int collectedFood;
    
    // max values in case range attribute doesnt do its thing
    private int _maxHealth = 100, _maxHunger = 100,_maxThirst = 100;

    // Misc stats (movement, interactions)
    [SerializeField] private readonly float _moveSpeed = 3;
    [FormerlySerializedAs("_rotationSpeed")] [SerializeField] private float rotationSpeed = 3;
    
    private GameObject playerSprite;

    // Controls related
    public bool isPlayerTwo;
    private PlayerInput _playerInput;
    //private PlayerControls _playerControls = null;

    public enum PlayerType
    {
        HUMAN,
        COMPUTER
    };

    public PlayerType playerType;

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
    private Tile _selectionTile;
    public bool turnConfirmed = false;
    
    // UI stuff
    private StatusBarController healthBar, hungerBar, thirstBar;
    
    // Bot for smarter movement
    public bool CPUSmartMove = true;
    private DjikstraBot _djikstraBot;
    private Queue<Vector3Int> pathToTarget;

    private AStarBotV2 _aStarBot;


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

    public void SetupPlayer(string playerName, Transform parentTransform, Tilemap overlayTilemap, Tilemap map, Vector3Int spawnPosition, Tile playerSelectionTile, bool isHumanPlayer, GameController gameController)
    {
        gameObject.name = playerName;
        playerType = isHumanPlayer ? PlayerType.HUMAN : PlayerType.COMPUTER;
        _GameController = gameController;

        _tilemap = map;
        gameObject.transform.parent = parentTransform;
        gameObject.transform.position = map.CellToWorld(spawnPosition) + new Vector3(0.5f, 0.5f, 0);
        currentPosition = spawnPosition;
        overlayMap = overlayTilemap;
        _selectionTile = playerSelectionTile;
        
        currentHealth = 100;
        currentHunger = 100;
        currentThirst = 100;

        SetupStatusBars();
        
        playerSprite = gameObject.transform.Find("PlayerSprite").gameObject;
        SetPlayerColor(playerName);

        if (playerType != PlayerType.COMPUTER) return;
        GetComponent<PlayerInput>().enabled = false;
        if (CPUSmartMove)
        {
            //_djikstraBot = GetComponent<DjikstraBot>();
            //_djikstraBot.tilemap = _tilemap;
            //_djikstraBot.SetupBot();

            _aStarBot = GetComponent<AStarBotV2>();
            _aStarBot.tilemap = _tilemap;
            _aStarBot.SetupBot();
        }
       
        playerSprite.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
        CPUInput();
    }

    private void HumanInput(InputAction.CallbackContext value)
    {
        float turnDirectionValue = 0f;
        
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
            /*case "Confirm":
            {
                if (selectedPosition != Vector3Int.forward)
                {
                    turnConfirmed = true;
                }
                return;
            }*/
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
            turnConfirmed = true;
        };
    }

    public void CPUInput()
    {
        if (CPUSmartMove)
        {
            if (pathToTarget == null || pathToTarget.Count == 0)
            {
              AStarPathFinding();
              MoveAlongPath();
            }
            else
            {
                if (_aStarBot.targetTileName.Equals("Food"))
                {
                   if (IsTargetStillThere())
                   {
                       MoveAlongPath();
                   }
                   else
                   {
                       _aStarBot.targetTileName = "WAdjacent";
                       AStarPathFinding();
                   }
                }
                else
                {
                   MoveAlongPath(); 
                }
            }
            
            //SmartMovementDjikstra();
        }
        else
        {
            RandomMovement();
        }
        

        turnConfirmed = true;
    }

    private bool IsTargetStillThere()
    {
        return _tilemap.GetTile(_aStarBot.targetPos).name.Contains("Food") &&
               !_tilemap.GetTile(_aStarBot.targetPos).name.Contains("Used");
    }

    private void MoveAlongPath()
    {
        selectedPosition = pathToTarget.Dequeue();
    }

    private void AStarPathFinding()
    {
        _aStarBot.currentPos = currentPosition;
        _aStarBot.targetTileName = "";
        
        if (currentHunger < currentThirst)
        {
            _aStarBot.targetTileName = "Food";
            _aStarBot.lockCurrentTargetType = false;
        } 
        else if(currentHunger > currentThirst)
        {
            _aStarBot.targetTileName = "WAdjacent";
            _aStarBot.lockCurrentTargetType = false;
        } 
        else if(currentHunger == currentThirst)
        {
            if (!_aStarBot.lockCurrentTargetType)
            { 
                var random = Random.Range(0, 2);
                _aStarBot.targetTileName = (random == 1 ? "Food" : "WAdjacent"); 
                _aStarBot.lockCurrentTargetType = true; 
            }
            
        }

        pathToTarget = _aStarBot.FindPath();

    }

    private void RandomMovement()
    {
        bool validTurn = false;
        
        do
        {
            int randomAction = Random.Range(0, 4);

            switch (randomAction)
            {
                case 0: // UP
                {
                    _tempSelectedPosition = currentPosition + new Vector3Int(0, 1, 0);
                    _movementDirection = MovementDirection.UP;
                    break;
                }
                case 1: // DOWN
                {
                    _tempSelectedPosition = currentPosition + new Vector3Int(0, -1, 0);
                    _movementDirection = MovementDirection.DOWN;
                    break;
                }
                case 2: // LEFT
                {
                    _tempSelectedPosition = currentPosition + new Vector3Int(-1, 0, 0);
                    _movementDirection = MovementDirection.LEFT;
                    break;
                }
                case 3: // RIGHT
                {
                    _tempSelectedPosition = currentPosition + new Vector3Int(1, 0, 0);
                    _movementDirection = MovementDirection.RIGHT;
                    break;
                }
            }

            if (CheckTile(_tempSelectedPosition) != null)
            {
                overlayMap.ClearAllTiles();
                selectedPosition = _tempSelectedPosition;
                overlayMap.SetTile(selectedPosition, _selectionTile);
                validTurn = true;
            }
        } while (validTurn == false);
    }

    private TileBase CheckTile(Vector3Int tileToCheckPos)
    {
        if (tileToCheckPos == Vector3Int.forward)
        {
            return null;
        }

        if (CheckForOutOfBounds(tileToCheckPos)) return null;
        
        TileBase tileBase = _tilemap.GetTile(tileToCheckPos);
        if (tileBase.name.Contains("Grass") || tileBase.name.Contains("Food"))
        {
            return tileBase;
        }
        return null;
    }

    private bool CheckForOutOfBounds(Vector3Int tileToCheckPos)
    {
        return (tileToCheckPos.x is > 5 or < 0 || tileToCheckPos.y is > 0 or < -5);
    }

    public void FinishTurn()
    {
        turnConfirmed = false;
        currentPosition = _tilemap.WorldToCell(transform.position);
        selectedPosition = Vector3Int.forward;
        _tempSelectedPosition = Vector3Int.forward;
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

        TurnToMovement();

    }

    private void TurnToMovement()
    {
        playerSprite.transform.rotation = _movementDirection switch
        {
            MovementDirection.UP => Quaternion.Euler(0, 0, 90f),
            MovementDirection.DOWN => Quaternion.Euler(0, 0, -90f),
            MovementDirection.LEFT => Quaternion.Euler(0, 0, 180f),
            MovementDirection.RIGHT => Quaternion.Euler(0, 0, 0f),
            _ => playerSprite.transform.rotation
        };
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

        if (nearbyTiles.Any(tile => tile.name.Contains("Water")))
        {
            currentThirst = _maxThirst;
        }

    }

    private void FoodCheck()
    {
        if (currentPosition == Vector3Int.forward)
        {
            Debug.Log("currentPositon was Vector3Int.forward - Correcting..");
            currentPosition = _tilemap.WorldToCell(transform.position);
        }
        TileBase currentTile = CheckTile(currentPosition);

        if (currentTile.name.Contains("Food") && !currentTile.name.Contains("Used"))
        {
            Eat();
            collectedFood++;
        }
    }

    private void Eat()
    {
        currentHunger = _maxHunger;
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

    public void SetPlayerColor(string playerName)
    {
        SpriteRenderer spriteRenderer = playerSprite.GetComponent<SpriteRenderer>();

        switch (playerName)
        {
            case "Player 1":
                spriteRenderer.color = Color.red;
                break;
            case "Player 2":
                spriteRenderer.color = Color.yellow;
                break;
        }
    }
}