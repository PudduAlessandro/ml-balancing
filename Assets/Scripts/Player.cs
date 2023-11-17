using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    // Important stats for gameplay
    [Range(0, 50)] 
    [SerializeField] private int _health, _hunger, _thirst = 50;

    // Misc stats (movement, interactions)
    [SerializeField] private readonly float _moveSpeed = 3;
    [SerializeField] private float _rotationSpeed = 3;
    private Vector2 _movementDirection;
    public bool isPlayerTwo;

    // Map related
    private Tilemap _tilemap;
    private Tile _selectedTile = null;
    private Vector3Int currentPosition;


    // Start is called before the first frame update
    void Start()
    {
        _tilemap = GameObject.Find("MapTilemap").GetComponent<Tilemap>();
        currentPosition = _tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(gameObject.transform.position));
    }

    // Update is called once per frame
    void Update()
    {
        // Get left/right and up/down movement from button inputs
        

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
           Vector3Int tilemapPos = _tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
           
           _selectedTile = _tilemap.GetTile<Tile>(tilemapPos);
           
           Debug.Log(new Vector2Int(tilemapPos.x, tilemapPos.y));
        }
        
        
       
    }
}
