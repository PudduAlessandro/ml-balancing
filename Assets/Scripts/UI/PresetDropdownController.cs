using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PresetDropdownController : MonoBehaviour
{
    [SerializeField] private Tilemap presetTilemap;
    private TMP_Dropdown dropdown;
    [SerializeField] private Tile[] tiles;

    public int selectedOption;


    // Start is called before the first frame update
    void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        dropdown.value = Random.Range(0, 8);
        dropdown.RefreshShownValue();
        selectedOption = dropdown.value;
        
        UpdatePreview(dropdown.value);
    }

    public void UpdatePreview(int value)
    {
        selectedOption = value;
        
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

        string mapString = presetMaps[value];
        
        int xCoord = 0;
        int yCoord = 0;
        
        foreach (string y in mapString.Split(","))
        {
            xCoord = 0;
            foreach (string x in y.Split(" "))
            {
                presetTilemap.SetTile(new Vector3Int(xCoord, -yCoord), tiles[int.Parse(x)]);
                xCoord++;
            }

            yCoord++;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
