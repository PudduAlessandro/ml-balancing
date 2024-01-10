using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HelpPanelController : MonoBehaviour
{
    public GameObject helpPanel;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("entered");
        helpPanel.SetActive(true);
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("left");
        helpPanel.SetActive(false);
    }

    private void OnMouseEnter()
    {
        Debug.Log("entered");
        helpPanel.SetActive(true);
    }
    
    private void OnMouseExit()
    {
        Debug.Log("left");
        helpPanel.SetActive(false);
    }
}
