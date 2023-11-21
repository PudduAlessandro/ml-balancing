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
    private PlayerControls _playerControls = null;

    // Map related
    private Tilemap _tilemap;
    private Tile _selectedTile = null;
    public Vector3Int currentPosition;


    // Start is called before the first frame update
    void Start()
    {
        _tilemap = GameObject.Find("MapTilemap").GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SelectAbove(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            CheckTile(currentPosition + new Vector3Int(0, 1, 0));
        }
    }

    public void SelectBelow(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            CheckTile(currentPosition + new Vector3Int(0, -1, 0));
        }
    }

    public void SelectLeft(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            CheckTile(currentPosition + new Vector3Int(-1, 0, 0));
        }
    }

    public void SelectRight(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            CheckTile(currentPosition + new Vector3Int(1, 0, 0));
        }
    }

    public void Confirm(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            Debug.Log("performed");
        }
    }

    public void Cancel(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            Debug.Log("performed");
        }
    }

    private TileBase CheckTile(Vector3Int tileToCheckPos)
    {

        if (!CheckForOutOfBounds(tileToCheckPos))
        {
           TileBase tileBase = _tilemap.GetTile(tileToCheckPos);
           Debug.Log(tileBase.name);
           
           return tileBase; 
        }
        else
        {
            Debug.Log("Invalid Choice!");
        }

        return null;
    }

    private bool CheckForOutOfBounds(Vector3Int tileToCheckPos)
    {
        return (tileToCheckPos.x is > 5 or < 0 || tileToCheckPos.y is > 0 or < -5);
    }
}
