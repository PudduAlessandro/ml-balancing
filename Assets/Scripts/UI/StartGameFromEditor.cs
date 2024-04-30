using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameFromEditor : MonoBehaviour
{
    //TODO: Create main menu wide controller instead of one script per button 
    
    public TMP_InputField codeField;

    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = gameObject.GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (codeField.text.Contains("Invalid"))
        {
            button.enabled = false;
        }
        else
        {
            button.enabled = true;
        }
    }

    public void StartGame()
    {
        PlayerPrefs.SetString("map", codeField.text);
        SceneManager.LoadScene("GameScene");
    }
}
