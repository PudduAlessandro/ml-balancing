using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class ToClipboard : MonoBehaviour
{
    public TextMeshProUGUI codeTextField;
    private TextMeshProUGUI buttonText;

    private void Start()
    {
        buttonText = transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
    }

    public void CopyToClipboard()
    {
        StartCoroutine(Copy());
    }

    [DllImport("__Internal")]
    private static extern void CopyToClipboardJS(string levelCode);

    private IEnumerator Copy()
    {
        var textToCopy = "";
        var mapString = codeTextField.text;

        switch (gameObject.name)
        {
            case "MapstringCopy":
            {
                textToCopy = mapString;
                
                GUIUtility.systemCopyBuffer = textToCopy;
                buttonText.text = "Copied!";

                if (Application.platform == RuntimePlatform.WebGLPlayer)
                {
                    CopyToClipboardJS(textToCopy);
                    Debug.Log(textToCopy);
                }
        
                yield return new WaitForSeconds(1);
        
                buttonText.text = "Copy to clipboard";
                
                break;
            }
            case "URLCopy":
            {
                if (!Application.isEditor)
                {
                    var url = Application.absoluteURL;

                    if (url.Contains("level"))
                    {
                        var urlParts = url.Split("=");

                        textToCopy = urlParts[0] + "=" + mapString;
                    }
                    else
                    {
                        textToCopy = url + "?level=" + mapString;
                    }
                    
                    GUIUtility.systemCopyBuffer = textToCopy;
                    buttonText.text = "Copied!";
                    
                    if (Application.platform == RuntimePlatform.WebGLPlayer)
                    {
                        CopyToClipboardJS(textToCopy);
                        Debug.Log(textToCopy);
                    }
        
                    yield return new WaitForSeconds(1);
        
                    buttonText.text = "Copy URL";
                } 
                else 
                {
                    GUIUtility.systemCopyBuffer = mapString;
                    buttonText.text = "Copied!";
                    
                    if (Application.platform == RuntimePlatform.WebGLPlayer)
                    {
                        CopyToClipboardJS(mapString);
                    }
        
                    yield return new WaitForSeconds(1);
                    
                    buttonText.text = "Copy URL";
                }
                break;
            }
        }
    }

}
