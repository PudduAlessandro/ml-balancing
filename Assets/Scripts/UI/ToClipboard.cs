using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class ToClipboard : MonoBehaviour
{
    public TMP_InputField codeTextField;
    private TextMeshProUGUI buttonText;

    private void Start()
    {
        buttonText = GameObject.Find("Text").GetComponent<TextMeshProUGUI>();
    }

    public void CopyToClipboard()
    {
        StartCoroutine(Copy());
    }

    private IEnumerator Copy()
    {
        var textToCopy = "";
        
        switch (gameObject.name)
        {
            case "MapstringCopy":
            {
                textToCopy = codeTextField.text;
                
                GUIUtility.systemCopyBuffer = textToCopy;
                buttonText.text = "Copied!";
        
                yield return new WaitForSeconds(1);
        
                buttonText.text = "Copy to clipboard";
                
                break;
            }
            case "URLCopy":
            {
                if (!Application.isEditor)
                {
                    var baseURL = Application.absoluteURL;
                    var mapString = codeTextField.text;

                    mapString = mapString.Replace(" ", "").Replace(",", "");

                    GUIUtility.systemCopyBuffer = mapString;
                    buttonText.text = "Copied!";
        
                    yield return new WaitForSeconds(1);
        
                    buttonText.text = "Copy URL";
                    
                    
                    
                    //var regex = new Regex(@"\d\s\d\s\d\s\d\s\d\s\d,\d\s\d\s\d\s\d\s\d\s\d,\d\s\d\s\d\s\d\s\d\s\d,\d\s\d\s\d\s\d\s\d\s\d,\d\s\d\s\d\s\d\s\d\s\d,\d\s\d\s\d\s\d\s\d\s\d")
                    
                    
                    
                    //mapString = Regex.Replace(mapString, @"\d\s\d\s\d\s\d\s\d\s\d,\d\s\d\s\d\s\d\s\d\s\d,\d\s\d\s\d\s\d\s\d\s\d,\d\s\d\s\d\s\d\s\d\s\d,\d\s\d\s\d\s\d\s\d\s\d,\d\s\d\s\d\s\d\s\d\s\d")

                } 
                else 
                {
                    var mapString = codeTextField.text;

                    mapString = mapString.Replace(" ", "").Replace(",", "");

                    GUIUtility.systemCopyBuffer = mapString;
                    buttonText.text = "Copied!";
        
                    yield return new WaitForSeconds(1);
        
                    buttonText.text = "Copy URL";
                }
                break;
            }
        }
    }

    public static string Base64Encode(string plainText) 
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }

}
