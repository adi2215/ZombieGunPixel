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

    public AudioClip[] audioClips;

    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Hero");
        StartCoroutine(PlayEnemySoundRepeatedly(Random.Range(5, 9)));
    }

    public float minDist = 1;
    public float maxDist = 10;
 
    void Update()
    {
        float dist = Vector3.Distance(transform.position, player.transform.position);
 
        if(dist < minDist)
        {
            enemySound.volume = 0.14f;
        }
        else if(dist > maxDist)
        {
            enemySound.volume = 0;
        }
        else
        {
            enemySound.volume = 0.14f - ((dist - minDist) / (maxDist - minDist));
        }
    }

    IEnumerator PlayEnemySoundRepeatedly(float interval)
    {
        if (enemySound != null && audioClips.Length > 0)
        {

            while (true)
            {
                int randomIndex = Random.Range(0, audioClips.Length);
                Debug.Log(randomIndex);
            
                enemySound.clip = audioClips[randomIndex];

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
