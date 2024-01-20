using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class CPUPlayer : MonoBehaviour
{
    // Core gameplay stats
    [Range(0, 100)] public int currentHealth = 100, currentHunger = 100, currentThirst = 100;

    public GameController _GameController;

    public int collectedFood;
    
    // max values in case range attribute doesnt do its thing
    private int _maxHealth = 100, _maxHunger = 100,_maxThirst = 100;
    
    
    private GameObject playerSprite;
    

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
    private int _foodRestoreAmount = 20, _thirstRestoreAmount = 20, _healthRestoreAmount = 10;

    //private Tile _selectedTile = null;
    private Vector3Int _tempSelectedPosition = Vector3Int.forward;
    public Vector3Int selectedPosition = Vector3Int.forward;

    // UI stuff
    private StatusBarController healthBar, hungerBar, thirstBar;
    
    // Bot for smarter movement
    private Queue<Vector3Int> pathToTarget;
    private AStarBotV2 _aStarBot;
}
