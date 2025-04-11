using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageSystem : MonoBehaviour
{
    public Data data;
    public Text countCoins;
    private void Start()
    {
        CoinCollected();
    }
    //wd
    private void OnEnable() {
        CoinSystem.OnCoinCollect += CoinCollected;
    }

    private void OnDisable() {
        CoinSystem.OnCoinCollect -= CoinCollected;
    }

    private void CoinCollected()
    {
        //Debug.Log("ff");
        countCoins.text = data.countCoins.ToString();
    }

    
}
