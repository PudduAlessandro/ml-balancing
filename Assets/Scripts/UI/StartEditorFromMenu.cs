using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartEditorFromMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartEditor()
    {
        SceneManager.LoadScene("MapEditor");
    }
}
