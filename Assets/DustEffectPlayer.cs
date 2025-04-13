using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustEffectPlayer : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play("Dust");
        Invoke("DestroyEffect", 1f);
    }

    private void DestroyEffect() => Destroy(gameObject);
}
