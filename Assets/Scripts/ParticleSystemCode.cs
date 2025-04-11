using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemCode : MonoBehaviour
{
    [SerializeField] ParticleSystem movementParticle;

    [Range(0, 10)]
    [SerializeField] int occurAfterVel;

    [Range(0, 0.2f)]
    [SerializeField] float dustFormationPeriod;
    [SerializeField] Rigidbody2D rb;

    public PlayerMovement movement;

    float counter;

    private void Update()
    {
        counter += Time.deltaTime;

        if (movement.movementInput != Vector2.zero)
        {
            if (counter > dustFormationPeriod)
            {
                movementParticle.Play();
                counter = 0;
            }
        }
    }
}
