using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class MapEditorController : MonoBehaviour
{
    public TMP_InputField codeField;
    private String _mapCode;
    
    [DllImport("__Internal")]
    private static extern void CopyToClipboardJS(string stringToCopy);
    

    public void CopyMapString()
    {
        _mapCode = codeField.text;

        if (Application.isEditor)
        {
            GUIUtility.systemCopyBuffer = _mapCode;
        } 
        else if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            CopyToClipboardJS(_mapCode);
        }
    }

    public void CopyMapURL()
    {
        _mapCode = codeField.text;

        var stringToCopy = "";
        
        if (Application.absoluteURL.Contains("level"))
        {
            stringToCopy = $"{Application.absoluteURL.Split('=')[0]}={_mapCode}";
        }
        else
        {
            stringToCopy = $"{Application.absoluteURL}?level={_mapCode}";
        }

        if (Application.isEditor)
        {
            GUIUtility.systemCopyBuffer = _mapCode;
        } 
        else if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            CopyToClipboardJS(stringToCopy);
        }
    }
    
}
