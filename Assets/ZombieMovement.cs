using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    private Rigidbody2D rb2D;

    public Target target;

    public float maxSpeed = 2f;

    [SerializeField]
    private float acceleration = 50, deacceleration = 100;

    [SerializeField]
    private float currentSpeed = 0;
    private Vector2 oldMovementInput;
    public Vector2 MovementInput { get; set; }

    public float speed;


    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    public void TargetHurt()
    {
        target.TakeDamage(1);
    }

    public void TargetHurtGranate()
    {
        target.TakeDamage(3);
    }

    private void FixedUpdate()
    {
        if (MovementInput.magnitude > 0 && currentSpeed >= 0)
        {
            oldMovementInput = MovementInput;
            currentSpeed += acceleration * maxSpeed * Time.fixedDeltaTime;
        }

        else
        {
            currentSpeed -= acceleration * maxSpeed * Time.fixedDeltaTime;
        }

        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
        //rb2D.velocity = oldMovementInput * currentSpeed;
        rb2D.MovePosition(rb2D.position + oldMovementInput * currentSpeed * Time.fixedDeltaTime);
    }

    public AudioSource enemySound;

    void Start()
    {
        StartCoroutine(PlayEnemySoundRepeatedly(5f));
    }

    IEnumerator PlayEnemySoundRepeatedly(float interval)
    {
        if (enemySound != null)
        {

            while (true)
            {
                enemySound.Play();

                yield return new WaitForSeconds(interval);
            }
        }
        else
        {
            Debug.LogError("Enemy sound not assigned!");
        }
    }
}
