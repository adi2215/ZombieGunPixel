using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    [Header("UI")]
    public Image image;

    [HideInInspector] public Gun item;
    [HideInInspector] public int count = 1;
    [HideInInspector] public Transform parentAfterDrag;

    public void InitialItem(Gun newItem)
    {
        item = newItem;
        image.sprite = newItem.image;
    }
}
