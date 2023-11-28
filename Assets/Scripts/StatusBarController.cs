using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StatusBarController : MonoBehaviour
{
    private Slider _statusSlider;

    private Player _linkedPlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMaxStatusValue(int value)
    {
        _statusSlider = gameObject.GetComponent<Slider>();
        _statusSlider.maxValue = value;
    }

    public void UpdateStatusBar(int value)
    {
        _statusSlider.value = value;
    }
}
