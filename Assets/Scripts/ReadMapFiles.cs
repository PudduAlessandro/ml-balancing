using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class ReadMapFiles : MonoBehaviour
{
    private List<string> mapFiles = new();
    private List<string> mapNames = new();
    private TMP_Dropdown dropdown;
    private string mapPath;
    // Start is called before the first frame update
    void Start()
    {
        mapPath = Application.streamingAssetsPath + "/Maps";

        dropdown = GetComponent<TMP_Dropdown>();
        dropdown.ClearOptions();
        mapFiles.AddRange(System.IO.Directory.GetFiles(mapPath));

        foreach (var map in mapFiles)
        {
            var mapName = System.IO.Path.GetFileName(map);
            Debug.Log(mapName);
            mapNames.Add(mapName);
        }
        
        dropdown.AddOptions(mapNames);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
