using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class MapSelection : MonoBehaviour
{

    [SerializeField] private Transform contentTransform;
    [SerializeField] private GameObject mapButtonPrefab;

    // Start is called before the first frame update
    void Start()
    {
        LoadMapFiles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void LoadMapFiles()
    {
        DirectoryInfo mapFileDir = new DirectoryInfo(Application.streamingAssetsPath + "/Maps");

        FileInfo[] maps = mapFileDir.GetFiles();

        foreach (var map in maps)
        {
            if (map.Name.Contains("meta")) continue;
            GameObject mapButton = Instantiate(mapButtonPrefab, contentTransform);
            mapButton.name = map.Name;
              
            TextMeshProUGUI tmp = mapButton.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
            tmp.text = map.Name;


        }
        
    }
}
