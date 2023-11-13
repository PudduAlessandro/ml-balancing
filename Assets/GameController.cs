using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameController : MonoBehaviour
{
    [SerializeField] private Tilemap _tileMap;
    private Vector2 _cursorLocation;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Camera.main != null) _cursorLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(_cursorLocation, Vector2.down);
                    
            Debug.Log("click on " + hit.collider.name);
            Debug.Log(hit.point);
            
            var tilePos = _tileMap.WorldToCell(hit.point);
            
            var tile = _tileMap.GetTile(tilePos);
            Debug.Log(String.Format("Tile name: {0}"));
        }
    }
}
