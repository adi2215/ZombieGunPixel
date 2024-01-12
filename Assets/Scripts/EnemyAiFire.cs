using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAiFire : MonoBehaviour
{
    public UnityEvent<Vector2> OnMovementInput, OnPointerInput;
    public UnityEvent OnAttack;

    [SerializeField]
    private Transform player;

    [SerializeField]
    private float chaseDistance = 3,
    attackDistance = 2f;

    [SerializeField]
    private float attackDelay = 2;
    private float passedTime = 1;

    public GameObject playerL;
    public Target tar;

    public bool PlayerThere = false;

    private bool explode = false;
    public Rigidbody2D rb;

    private void Start()
    {
        playerL = GameObject.FindGameObjectWithTag("Hero");
        player = playerL.transform;
    }

    private void Update()
    {
        if (player == null || explode)
            return;

        float distance = Vector2.Distance(player.position, transform.position);

        if (distance < chaseDistance || PlayerThere)
        {
            PlayerThere = true;
            OnPointerInput?.Invoke(player.position);
            if (distance <= attackDistance)
            {
                explode = true;
                if (tar != null)
                    tar.GetComponent<Target>().enabled = false;
                OnMovementInput?.Invoke(Vector2.zero);
                OnAttack?.Invoke();
                Destroy(gameObject, 1.2f);
                //rb.bodyType = RigidbodyType2D.Static;
            }
            else
            {
                Vector2 direction = player.position - transform.position;
                OnMovementInput?.Invoke(direction.normalized);
            }
        }

        if (passedTime < attackDelay)
        {
            passedTime += Time.deltaTime;
        }
    }
}
