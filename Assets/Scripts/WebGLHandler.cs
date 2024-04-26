using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Windows;
using Random = UnityEngine.Random;

public class WebGLHandler : MonoBehaviour
{
    private GameController1P _gameController;
    private string mapHash = "";
    private string mapString = "";
    private int mapIndex = 8;

    // Start is called before the first frame update
    void Start()
    {
        _gameController = GetComponent<GameController1P>();

        if (Application.isEditor)
        {
            //Debug.Log("Starting in Editor");
            //JsInput("MyAyIDMgMCAyIDEsMCAwIDEgMiAwIDIsMCAyIDAgMCAxIDAsMiAwIDMgNSAwIDAsMSAwIDAgMiAxIDAsMSAwIDAgNCAxIDE=");
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

    public void ReadMapString(string mapString)
    {
        var formattedMapString = DecodeMapString(mapString);
        PlayerPrefs.SetString("map", formattedMapString);
        _gameController.SetupGame();
    }


    public void InputMap(string hash = null)
    {

        mapIndex = hash switch
        {
            "a948fed06eddc83bf5c369e86b5e1e4dcdcb3258" => 0, // Level Index 1
            "7a16d728ef7b8735b655a705e076d21ba61a3732" => 1, // Level Index 5
            "909b9b279ad9ac84b8cbb0cdddadb3d9266890d6" => 2, // Level Index 6 (2 and 6 swapped)
            "f561ef72142127046f5f1221afbc577151172c9d" => 3, // Level Index 2 (2 and 6 swapped) -> Equals 8
            "8c4b879bef2aadd67c18b34bd7c9526358faa763" => 4, // Level Index 7 (3 and 7 swapped)
            "57080b4d5cdbe96403537bb4418421aeefefdd0d" => 5, // Level Index 3 (3 and 7 swapped)
            "3b38abef59db3446d3e2d8aa473bab6ea5ffadb0" => 6, // Level Index 4
            "bae4e32a21350bbea99e989c7fa1859d29837df4" => 7, // Level Index 8 -> Equals 2
            _ => 8
        };

        _gameController.selectedMapIndex = mapIndex;
        //Debug.Log("Starting game...");
        _gameController.SetupGame();
    }

    private void Restart()
    {
        _gameController.selectedMapIndex = mapIndex;
        //Debug.Log("Restarting game...");
        _gameController.SetupGame();
    }
    
    private string DecodeMapString(string mapString)
    {
        return Regex.Replace(mapString, @"(\d{1})(\d{1})(\d{1})(\d{1})(\d{1})(\d{1})", "$1 $2 $3 $4 $5 $6,").TrimEnd(',');
    }
}
