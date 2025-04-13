using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentBat : MonoBehaviour
{
    private Animator anim;
    public GameObject bullet;
    public GameObject bulletParent;
    private Transform player;
    public Target targetBat;
    private bool facingRight = false;

    public float lineOfSite, fireRate = 1f, nextFireRate, rateFirst = 1f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Hero").transform;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Static)
            return;

        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer <= lineOfSite && rateFirst < Time.time)
        {
            Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
            nextFireRate = Time.time + fireRate;
            rateFirst = nextFireRate;
        }

        //Vector3 scale = zombie.localScale;
        if (transform.position.x < player.position.x && !facingRight)
        {
            Flip();
        }
        else if (transform.position.x > player.position.x && facingRight)
        {
            Flip();
        }
        //zombie.localScale = new Vector3(scale.x, scale.y, scale.z);
    }

    private void Flip() 
    { 
        transform.Rotate(0f, 180f, 0f);
        facingRight = !facingRight;
    }

        /*if (distanceFromPlayer <= explode)
        {
            targetBat.GetComponent<Target>().enabled = false;
            OnAttack?.Invoke();
            Destroy(gameObject, 1f);
        }*/

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        //Gizmos.DrawWireSphere(transform.position, explode);
    }
}
