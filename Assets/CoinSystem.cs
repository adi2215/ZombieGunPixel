using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSystem : MonoBehaviour, Collectible
{
    public static event Action OnCoinCollect;

    public GameObject Target;
    public GameObject coinEffect;
    public Data data;
    public bool taking = false;
    public AudioSource audioPlayer;

    private void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Hero");
    }
    private void Update()
    {
        if (Vector3.Distance(transform.position, Target.transform.position) < 1)
        {
            transform.position += Vector3.Normalize(Target.transform.position - transform.position) * (Vector3.Distance(transform.position, Target.transform.position)/2) * 25f * Time.deltaTime;
        } else if (Vector3.Distance(transform.position, Target.transform.position) < 10)
        {
            transform.position += Vector3.Normalize(Target.transform.position - transform.position) * 10f * Time.deltaTime;
            //Debug.Log(Target.transform.position.ToString() + Vector3.Normalize(Target.transform.position - transform.position).ToString());
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Hero") && !taking)
        {
            taking = true;
            StartCoroutine(TakingCoin());
        }
    }

    IEnumerator TakingCoin()
    {
        audioPlayer.Play();
        yield return new WaitForSeconds(0.3f);
        data.countCoins += 1;
        Debug.Log("TakeCoin");
        CoinCollect();
    }

    public void CoinCollect()
    {
        OnCoinCollect?.Invoke();
        GameObject coin = Instantiate(coinEffect, transform.position, Quaternion.identity);
        Destroy(coin, 0.5f);
        Destroy(gameObject);
    }
}
