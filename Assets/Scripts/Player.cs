using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    // Core gameplay stats
    [Range(0, 50)] [SerializeField]
    private int _maxHealth, _currentHealth, _maxHunger, _currentHunger, _maxThirst, _currentThirst;

    // Misc stats (movement, interactions)
    [SerializeField] private readonly float _moveSpeed = 3;
    [SerializeField] private float _rotationSpeed = 3;
    private Vector2 _movementDirection;

    // Controls related
    public bool isPlayerTwo;
    private PlayerInput _playerInput;
    //private PlayerControls _playerControls = null;

    // Map related
    public Vector3Int currentPosition;
    private Tilemap _tilemap, _overlayMap;
    //private Tile _selectedTile = null;
    private Vector3Int _tempSelectedPosition = Vector3Int.zero, _selectedPosition = Vector3Int.zero;
    [SerializeField] private Tile selectionTile;


    // Start is called before the first frame update
    void Start()
    {
        _tilemap = GameObject.Find("MapTilemap").GetComponent<Tilemap>();
        _overlayMap = GameObject.Find("OverlayTilemap").GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {

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
                break;
            }
            case "Cancel":
            {
                break;
            }
        }

        if (CheckTile(_tempSelectedPosition) != null)
        {
            _overlayMap.ClearAllTiles();
            _selectedPosition = _tempSelectedPosition;
            _overlayMap.SetTile(_selectedPosition, selectionTile);
        };
    }

    private TileBase CheckTile(Vector3Int tileToCheckPos)
    {

        if (!CheckForOutOfBounds(tileToCheckPos))
        {
           TileBase tileBase = _tilemap.GetTile(tileToCheckPos);
           Debug.Log(tileBase.name);
           if (tileBase.name.Equals("1_GRASS") || tileBase.name.Equals("3_FOREST"))
           {
               return tileBase;
           }
        }
        Debug.Log("Invalid Choice!");
        return null;
    }

    private bool CheckForOutOfBounds(Vector3Int tileToCheckPos)
    {
        return (tileToCheckPos.x is > 5 or < 0 || tileToCheckPos.y is > 0 or < -5);
    }
}
