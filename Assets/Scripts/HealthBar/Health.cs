using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health = 10;

    public int currentHealh;
    public Data data;

    public HealthBar healthBar;

    public float timer = 0;

    private PlayerMovement Player;

    public ManagerMenu menu;

    [SerializeField] private Rigidbody2D rb;

    public Text counCoints;

    private void Start()
    {
        health = data.hpHero;
        currentHealh = health;
        data.currentHealth = currentHealh;
        healthBar.SetMaxHealth(health);
        Player = GetComponentInParent<PlayerMovement>();
    }

    public void HealthBuy()
    {
        if (data.countCoins < 10)
            return;
        data.hpHero += 1;
        currentHealh += 1;
        data.countCoins -= 10;
        counCoints.text = data.countCoins.ToString();
    }

    private void SetTimeScale(float scale)
    {
        Time.timeScale = scale;
        Time.fixedDeltaTime = scale * .02f;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Fire") && timer <= 0)
        {
            currentHealh -= 1;
            data.currentHealth = currentHealh;
            healthBar.SetHealth(currentHealh);
            Player.dashSpeed = 0.05f;
            timer = 1.5f;

            //AnimationHurt
            transform.parent.GetComponent<Animator>().SetTrigger("IsDamage");
            //Player.canMove = true;
            Invoke("Reset", timer);
        }
    }

    public void GetHit(int hp, GameObject enemy)
    {
        currentHealh -= hp;
        timer = 1.5f;
        data.currentHealth = currentHealh;
        healthBar.SetHealth(currentHealh);

        Player.dashSpeed = 0.05f;
        transform.parent.GetComponent<Animator>().SetTrigger("IsDamage");
        //Player.canMove = true;
        Invoke("Reset", timer);
    }

    private void Reset()
    {
        Player.dashSpeed = 0.15f;
    }

    private void Update()
    {
        //healthBar.SetHealth(currentHealh);

        if (timer > 0)
        {
            SetTimeScale(1f - (timer * .4f));
            timer -= Time.deltaTime;
        }

        if (currentHealh <= 0)
            Die();
            //Die();
        
    }

    public void Die()
    {
        Player.Die();
        rb.bodyType = RigidbodyType2D.Static;
        Player.enabled = false;
        enabled = false;
        data.countFuel = 0;
        data.countCoins = 0;
        SetTimeScale(0.4f);
        StartCoroutine(NewScene());
    }

    private IEnumerator NewScene()
    {
        SetTimeScale(1f);
        yield return new WaitForSeconds(0.1f);
        menu.LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }

    /*private void NewScene()
    {
        SetTimeScale(1f);
        menu.LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }*/
}
