using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAnim : MonoBehaviour
{
    public void Start()
    {
        Invoke(nameof(DestrouBul), 1f);
    }

    public void DestrouBul()
    {
        Destroy(gameObject);
    }
}
