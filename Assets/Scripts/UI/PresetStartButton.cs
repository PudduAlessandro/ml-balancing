using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PresetStartButton : MonoBehaviour
{
    public PresetDropdownController dropdownController;


    public void StartPresetMap()
    {
        int value = dropdownController.selectedOption;
        
        string[] presetMaps =
        {
            "0 0 3 0 2 1,0 0 0 2 0 2,0 2 0 0 1 0,2 0 0 5 0 0,1 0 0 2 1 0,1 0 0 4 1 0", //WATERSWAP_UNBALANCED
            "0 0 0 0 2 1,0 0 0 2 0 2,0 2 0 0 1 0,2 0 0 5 3 0,1 0 0 2 1 0,1 0 0 4 1 0", //WATERSWAP_BALANCED

            "0 0 0 5 0 1,2 2 1 1 3 1,0 0 0 1 2 3,0 1 0 2 0 3,0 0 2 4 2 1,3 0 0 0 0 0", //MOUNTAINBLOCK_UNBALANCED
            "0 0 0 5 2 1,2 2 1 1 3 1,0 0 0 1 2 3,0 1 0 0 0 3,0 0 2 4 2 1,3 0 0 0 0 0", //MOUNTAINBLOCK_BALANCED

            "0 2 2 0 1 0,0 0 0 1 3 0,1 1 2 2 3 3,3 2 0 2 5 0,0 1 1 2 1 1,0 3 0 0 4 1", //EQUALIZELINE_0_2
            "1 2 2 0 1 2,0 0 0 1 3 0,1 0 2 2 3 3,3 2 0 0 5 0,1 1 0 1 1 1,2 3 0 0 4 0", //EQUALIZELINE_0_5

            "2 1 0 5 1 1,0 2 1 0 3 1,0 2 0 1 0 3,0 1 0 0 0 4,0 0 2 3 2 0,3 0 0 0 0 2", //MOUNTAINBLOCK2_UNBALANCED
            "0 0 0 5 2 1,2 2 1 1 3 1,0 0 0 1 2 3,0 1 0 0 0 3,0 0 2 4 2 1,3 0 0 0 0 0"
        };
        
        PlayerPrefs.SetString("map",presetMaps[value]);

        SceneManager.LoadScene("GameScene");
        
    }
}
