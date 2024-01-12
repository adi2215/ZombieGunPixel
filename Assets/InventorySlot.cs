using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image image;
    public Color selectedColor, notSelecedColor;

    private void Awake()
    {
        DeSelected();
    }

    public void Selected()
    {
        image.color = selectedColor;
    }

    public void DeSelected()
    {
        image.color = notSelecedColor;
    }
}
