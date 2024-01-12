using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots;

    public GameObject inventoryItemPrefab;
    public Data data;

    int selectedSlot = -1;
    int numberSlot = 0;

    public int maxNumberSlot = 2;

    private void Start()
    {
        ChangeSelectedSlot(0);

        for (int i = 0; i < data.guns.Count; i++)
        {
            AddItem(data.guns[i]);
        }
    }

    private void Update()
    {
        if (Input.inputString != null)
        {
            if (Input.mouseScrollDelta.y != 0)
            {
                numberSlot -= (int)Input.mouseScrollDelta.y;
                Debug.Log(numberSlot + "lll" + (int)Input.mouseScrollDelta.y);

                if (numberSlot >= 0 && numberSlot < maxNumberSlot)
                    ChangeSelectedSlot(numberSlot);

                else if (numberSlot < 0)
                {
                    numberSlot = 0;
                }
                else if (numberSlot > maxNumberSlot - 1)
                {
                    numberSlot = 1;
                }
            }
        }
    }

    void ChangeSelectedSlot(int newValue)
    {
        if (selectedSlot >= 0)
        {
            inventorySlots[selectedSlot].DeSelected();
        }

        data.itemChachedl = true;
        inventorySlots[newValue].Selected();
        selectedSlot = newValue;
    }

    public bool AddItem(Gun item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnItem(item, slot);
                return true;
            }
        }

        return false;
    }

    public void SpawnItem(Gun item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialItem(item);
    }

    public Gun GetSelectedItem()
    {
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null)
        {
            return itemInSlot.item;
        }

        return null;
    }
}
