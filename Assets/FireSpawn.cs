using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpawn : MonoBehaviour
{
    public GameObject Target;
    public GameObject Shockwave;
    public float speed;
    private Animator Hit;
    public float lineOfSite, shooTingRange;
    public float fireRate = 1f, nextFireRate, rateFirst = 1f;
    private bool facingRight = false;
    public Target target;


    private void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Hero");
        Hit = GetComponent<Animator>();
    }

    private void Update()
    {
        if (target.health <= 0)
            return;

        float distanceFromPlayer = Vector2.Distance(Target.transform.position, transform.position);
        /*if (distanceFromPlayer <= lineOfSite && distanceFromPlayer > shooTingRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, Target.transform.position, speed * Time.deltaTime);
            Hit.SetBool("Moving", true);
        }*/
        if (distanceFromPlayer <= shooTingRange && rateFirst < Time.time)
        {
            //Hit.SetBool("Moving", false);
            Hit.SetTrigger("Attack");
            nextFireRate = Time.time + fireRate;
            rateFirst = nextFireRate;
        }

        /*else if (distanceFromPlayer > lineOfSite)
        {
            //Hit.SetBool("Moving", false);
        }*/

        if (transform.position.x < Target.transform.position.x && !facingRight)
        {
            Flip();
        }
        else if (transform.position.x > Target.transform.position.x && facingRight)
        {
            Flip();
        }
    }

    private void Flip() 
    { 
        transform.Rotate(0f, 180f, 0f);
        facingRight = !facingRight;
    }


    IEnumerator SummonShockwaves()
    {
            Vector3 pos = transform.position;
            Vector3 goal = Vector3.Normalize(Target.transform.position - pos) * 1.1f;
            //Anim.SetBool("IsAttack", true);
            yield return new WaitForSeconds(0.1f);
            for (int i = 0; i < 10; ++i)
            {
                pos += goal;
                GameObject obj = Instantiate(Shockwave, pos, Quaternion.identity);
                obj.SetActive(true);
                yield return new WaitForSeconds(0.2f);
            }
        //Anim.SetBool("IsAttack", false);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, shooTingRange);
    }
}
