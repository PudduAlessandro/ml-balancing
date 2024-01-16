using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class JSTest : MonoBehaviour
{
    public GameController gameController;
    
    // Start is called before the first frame update
    void Start()
    {
        if (Application.isEditor)
        {
            JSInput();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void JSInput(string hash = null)
    {
        var mapIndex = hash switch
        {
            "a948fed06eddc83bf5c369e86b5e1e4dcdcb3258" => 0,
            "7a16d728ef7b8735b655a705e076d21ba61a3732" => 1,
            "909b9b279ad9ac84b8cbb0cdddadb3d9266890d6" => 2,
            "f561ef72142127046f5f1221afbc577151172c9d" => 3,
            "8c4b879bef2aadd67c18b34bd7c9526358faa763" => 4,
            "57080b4d5cdbe96403537bb4418421aeefefdd0d" => 5,
            "3b38abef59db3446d3e2d8aa473bab6ea5ffadb0" => 6,
            "bae4e32a21350bbea99e989c7fa1859d29837df4" => 7,
            _ => 8
        };

        gameController.selectedMapIndex = mapIndex;
        gameController.SetupGame();
    }
}
