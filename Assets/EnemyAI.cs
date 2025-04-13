using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAI : MonoBehaviour
{
    public UnityEvent<Vector2> OnMovementInput, OnPointerInput;
    public UnityEvent OnAttack;

    [SerializeField]
    private Transform player;

    [SerializeField]
    private float chaseDistance = 3,
    attackDistance = 0.8f;

    [SerializeField]
    private float attackDelay = 2;
    private float passedTime = 1;

    public GameObject playerL;

    public bool PlayerThere = false;

    private void Start()
    {
        playerL = GameObject.FindGameObjectWithTag("Hero");
        player = playerL.transform;
    }

    private void Update()
    {
        if (player == null)
            return;

        float distance = Vector2.Distance(player.position, transform.position);

        if (distance < chaseDistance || PlayerThere)
        {
            PlayerThere = true;
            OnPointerInput?.Invoke(player.position);
            if (distance <= attackDistance)
            {
                OnMovementInput?.Invoke(Vector2.zero);
                if (passedTime >= attackDelay)
                {
                    passedTime = 0;
                    Debug.Log("Time");
                    OnAttack?.Invoke();
                }
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
