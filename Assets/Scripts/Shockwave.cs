using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    float alpha;
    public float lifetime = 0.5f;
    float size = 1f;
    private void Awake()
    {
        //StartCoroutine(Oscilate);
        transform.localScale = new Vector3(1.5f, 0, 1.5f)*size;

        alpha = 0;
    }
    private void Update()
    {
        if (alpha > Mathf.PI)
        {
            Destroy(gameObject);
        }
        transform.localScale = new Vector3(1.5f, Mathf.Sin(alpha) * 1.5f, 1.5f) * size;
        alpha += Mathf.PI/lifetime * Time.deltaTime;
    }
}
