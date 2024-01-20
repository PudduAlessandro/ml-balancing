using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class WebGLHandler : MonoBehaviour
{
    private GameController _gameController;
    private string mapHash = "";
    private int mapIndex = 8;
    
    // Start is called before the first frame update
    void Start()
    {
        _gameController = GetComponent<GameController>();
        
        if (Application.isEditor)
        {
            Debug.Log("Starting in Editor");
            InputMap("a948fed06eddc83bf5c369e86b5e1e4dcdcb3258");
        }
        else
        { 
            Debug.Log("Starting in browser"); 
            string url = Application.absoluteURL; 
            string page = url.Split("/").Last(); 
            mapHash = page.Split(".")[0];
            
            InputMap(mapHash); 
        }
        
        
    }


    public void InputMap(string hash = null)
    {

        mapIndex = hash switch
        {
            "a948fed06eddc83bf5c369e86b5e1e4dcdcb3258" => 0,
            "7a16d728ef7b8735b655a705e076d21ba61a3732" => 1,
            "909b9b279ad9ac84b8cbb0cdddadb3d9266890d6" => 2,
            "f561ef72142127046f5f1221afbc577151172c9d" => 3,
            "8c4b879bef2aadd67c18b34bd7c9526358faa763" => 4,
            "57080b4d5cdbe96403537bb4418421aeefefdd0d" => 5,
            "3b38abef59db3446d3e2d8aa473bab6ea5ffadb0" => 6,
            "bae4e32a21350bbea99e989c7fa1859d29837df4" => 7,
            _ => Random.Range(0, 9)
        };

        _gameController.selectedMapIndex = mapIndex;
        Debug.Log("Starting game...");
        _gameController.SetupGame();
    }

    private void Restart()
    {
        _gameController.selectedMapIndex = mapIndex;
        Debug.Log("Restarting game...");
        _gameController.SetupGame();
    }
}
