using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelTake : MonoBehaviour
{
    public Data data;
    public Text text;
    private void OnTriggerEnter2D(Collider2D col)
    {
        Health target = col.GetComponent<Health>();
        if (target != null)
        {
            data.countFuel++;
            text.text = data.countFuel.ToString() + " / 3";
            Destroy(gameObject);
        }
    }
}
