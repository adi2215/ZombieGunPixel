using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Target : MonoBehaviour
{
    public int health = 2;
    public GameObject effect;
    public GameObject parentObject;
    public GameObject coins;

    private int currentHealth;

    private Animator anim;
    public EnemyAI enemyAI;
    public Rigidbody2D rb;
    public CircleCollider2D colliderCps;

    public HealthBarS healthBoss;
    public Data data;
    public TrigBoss sceneNew;


    private void Start()
    {
        anim = GetComponentInParent<Animator>();
        colliderCps = GetComponentInParent<CircleCollider2D>();
        currentHealth = health;
        if (gameObject.tag == "Boss")
        {
            healthBoss.SetMaxHealth(health);
            data.bossHp = health;
        }
    }


    public int Health
    {
        set {
            health = value;

            if (health <= 0)
                Defeated();
        }

        get {
            return health;
        }
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        currentHealth -= damage;
        if (gameObject.tag == "Boss")
        {
            data.bossHp = currentHealth;
            healthBoss.SetHealth(currentHealth);
        }
    }

    private void Defeated()
    {
        anim.SetTrigger("DieZombie");
        if (gameObject.tag == "Boss")
            sceneNew.NextScene();
        Destroy(gameObject, 0.05f);
        rb.bodyType = RigidbodyType2D.Static;
        //enemyAI.enabled = false;
        colliderCps.enabled = false;
        Instantiate(coins, transform.position, Quaternion.identity);
        GameObject clone = Instantiate(effect, transform.position, Quaternion.identity);
        Destroy (clone, 1.0f);
        Destroy(parentObject, 1.4f);
    } 

}
