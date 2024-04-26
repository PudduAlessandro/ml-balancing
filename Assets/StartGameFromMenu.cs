using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameFromMenu : MonoBehaviour
{
    private Button button;
    public TMP_InputField codeField;
    
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        button.interactable = Regex.IsMatch(codeField.text,
            @"\d\s\d\s\d\s\d\s\d\s\d,\d\s\d\s\d\s\d\s\d\s\d,\d\s\d\s\d\s\d\s\d\s\d,\d\s\d\s\d\s\d\s\d\s\d,\d\s\d\s\d\s\d\s\d\s\d,\d\s\d\s\d\s\d\s\d\s\d");
    }

    public void StartGame()
    {
        string mapString = codeField.text;

        PlayerPrefs.SetString("map", mapString);
        SceneManager.LoadScene("GameScene");
    }
}
