using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameFromEditor : MonoBehaviour
{
    //TODO: Create main menu wide controller instead of one script per button 
    
    public TextMeshProUGUI codeField;

    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = gameObject.GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        button.enabled = !codeField.text.Contains("Invalid");
    }

    public void StartGame()
    {
        PlayerPrefs.SetString("map", codeField.text);
        SceneManager.LoadSceneAsync("GameScene");
    }
}
