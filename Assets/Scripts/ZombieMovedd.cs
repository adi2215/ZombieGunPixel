using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovedd : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        Invoke("Move", 1f);
    }

    private void Move()
    {
        anim.SetBool("Moving", true);
    }
}
