using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    // Core gameplay stats
    [Range(0, 50)]
    [SerializeField] private int _maxHealth, _currentHealth, _maxHunger, _currentHunger, _maxThirst, _currentThirst;

    // Misc stats (movement, interactions)
    [SerializeField] private readonly float _moveSpeed = 3;
    [SerializeField] private float _rotationSpeed = 3;
    private Vector2 _movementDirection;
    
    // Controls related
    public bool isPlayerTwo;
    private PlayerInput _playerInput;
    private PlayerControls _playerControls = null;
    
    // Map related
    private Tilemap _tilemap;
    private Tile _selectedTile = null;
    private Vector3Int currentPosition;


    // Start is called before the first frame update
    void Start()
    {
        _tilemap = GameObject.Find("MapTilemap").GetComponent<Tilemap>();
        currentPosition = _tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(gameObject.transform.position));
        _playerInput = GetComponent<PlayerInput>();
        _playerControls = new PlayerControls();
        
        _playerControls.Selection.SelectAboveTile.performed += SelectAbove;
        _playerControls.Selection.SelectBelowTile.performed += SelectBelow;
        _playerControls.Selection.SelectLeftTile.performed += SelectLeft;
        _playerControls.Selection.SelectRightTile.performed += SelectRight;
    }

    // Update is called once per frame
    void Update()
    {
        
        // Switch to new Input System
        /*if (Input.GetKeyDown(KeyCode.Mouse0))
        {
           Vector3Int tilemapPos = _tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
           
           _selectedTile = _tilemap.GetTile<Tile>(tilemapPos);
           
           Debug.Log(new Vector2Int(tilemapPos.x, tilemapPos.y));
        }*/



    }

    void SelectAbove(InputAction.CallbackContext value)
    {
        Debug.Log(value);
    }
    
    void SelectBelow(InputAction.CallbackContext value)
    {
        Debug.Log(value);
    }
    
    void SelectLeft(InputAction.CallbackContext value)
    {
        Debug.Log(value);
    }
    
    void SelectRight(InputAction.CallbackContext value)
    {
        Debug.Log(value);
    }
}
