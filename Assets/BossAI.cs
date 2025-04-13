using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossAI : MonoBehaviour
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
    public Data data;
    public Transform posSecondStage;

    private void Start()
    {
        playerL = GameObject.FindGameObjectWithTag("Hero");
        player = playerL.transform;
    }

    private void Update()
    {
        if (player == null)
            return;

        if (data.bossHp <= 70)
        {
            transform.position = posSecondStage.position;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            GetComponent<ZombieMovement>().enabled = false;
            GetComponent<Agent>().enabled = false;
            GetComponent<AgentAnimations>().enabled = false;
            GetComponent<Erlik>().enabled = true;
            GetComponent<Erlik>().StartFighr();
            GetComponent<Animator>().SetBool("Moving", false);
            enabled = false;
        }

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
