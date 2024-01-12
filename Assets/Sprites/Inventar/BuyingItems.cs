using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


public class BuyingItems : MonoBehaviour, Collectible
{
    public static event Action OnCoinCollect;

    public InventoryManager inventoryManager;
    public Gun[] itemsToPickUp;
    public Data data;
    private bool havePlace;
    public Text countGranates;
    public Text countCoins;

    private void Start()
    {
        countGranates.text = data.GranateCount.ToString();
    }

    public void PickUpItems(int id)
    {
        if (itemsToPickUp[id].cost <= data.countCoins)
        {
            havePlace = inventoryManager.AddItem(itemsToPickUp[id]);
            if (havePlace)
            {
                data.countCoins -= itemsToPickUp[id].cost;
                data.guns.Add(itemsToPickUp[id]);
                StartCoroutine(TakingCoin());
                countCoins.text = data.countCoins.ToString();
            }
        }
    }

    IEnumerator TakingCoin()
    {
        yield return new WaitForSeconds(0.05f);
        CoinCollect();
    }

    public void BuyingGranate()
    {
        if (data.countCoins < 5)
        {
            return;
        }
        
        data.GranateCount++;
        countGranates.text = data.GranateCount.ToString();
        data.countCoins -= 5;
        countCoins.text = data.countCoins.ToString();
    }

    public void CoinCollect()
    {
        OnCoinCollect?.Invoke();
    }
}
