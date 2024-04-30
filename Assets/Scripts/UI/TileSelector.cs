using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TileSelector : MonoBehaviour
{
    [SerializeField ]private Transform[] buttonTransforms;
    [SerializeField] private MapEditor _mapEditor;
    [SerializeField] private GameObject selectionImage;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void SetSelectedTile(int selection)
    {
        _mapEditor.SetSelectedTile(selection);

        selectionImage.transform.SetParent(buttonTransforms[selection]);
        selectionImage.transform.SetSiblingIndex(0);
        var position = selectionImage.transform.localPosition;
        position =
            new Vector3(position.x, 0, position.z);
        selectionImage.transform.localPosition = position;
    }
}
