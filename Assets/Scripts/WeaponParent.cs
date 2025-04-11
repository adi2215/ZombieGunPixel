using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    private Animator animator;
    public Transform circleOrigin;
    public Vector2 PointerPosition { get; set; }

    public bool IsAttacking { get; private set; }
    public float radius;
    public float explosionForce;
    public float explosionForceZombie;

    public GameObject Shockwave;
    public GameObject Target;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
        Target = GameObject.FindGameObjectWithTag("Hero");
    }

    public void ResetIsAttacking()
    {
        IsAttacking = false;
    }

    /*private void Update()
    {
        if (IsAttacking)
            return;
        
        Vector2 direction = (PointerPosition - (Vector2)transform.position).normalized;
        transform.right = direction;

        Vector2 scale = transform.localScale;
        if (direction.x < 0)
        {
            scale.y = -1;
        }
        else if (direction.x > 0)
        {
            scale.y = 1;
        }
        transform.localScale = scale;
    }*/

    public void Attack()
    {
        animator.SetTrigger("Attack");
        //IsAttacking = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Vector3 position = circleOrigin == null ? Vector3.zero : circleOrigin.position;
        Gizmos.DrawWireSphere(position, radius);
    }

    public IEnumerator SummonShockwaves()
    {
        Vector3 pos = circleOrigin.position;
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

    public void AttackBoss()
    {
        StartCoroutine(SummonShockwaves());
    }

    public void DetectColliders()
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(circleOrigin.position,radius))
        {
            //Debug.Log(collider.name);
            Health health;
            if(health = collider.GetComponent<Health>())
            {
                health.GetHit(1, transform.parent.gameObject);
            }
        }
    }

    public void DetectColliders2()
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(circleOrigin.position,radius))
        {
            Health health;
            ZombieMovement zombie;
            if(health = collider.GetComponent<Health>())
            {
                Rigidbody2D rb = collider.GetComponentInParent<Rigidbody2D>();
                GetExplode(rb);
                health.GetHit(1, transform.parent.gameObject);
            }

            if(zombie = collider.GetComponent<ZombieMovement>())
            {
                Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
                if (!collider.GetComponent<EnemyAiFire>())
                {
                    GetExplodeZombie(rb);
                    zombie.TargetHurt();
                }
            }
        }
    }

    private void GetExplode(Rigidbody2D obj)
    {
        Vector2 direction = obj.position - (Vector2)circleOrigin.position;
        float distance = direction.magnitude;
        float force = 1 - (distance / explosionForce);
        obj.AddForce(direction.normalized * explosionForce * force, ForceMode2D.Impulse);
        StartCoroutine(Stop(obj));
    }

    IEnumerator Stop(Rigidbody2D obj) 
    {
        yield return new WaitForSeconds(0.12f);
        obj.velocity = Vector2.zero;
    }

    private void GetExplodeZombie(Rigidbody2D obj)
    {
        obj.gameObject.GetComponent<ZombieMovement>().enabled = false;
        Vector2 direction = obj.position - (Vector2)circleOrigin.position;
        float distance = direction.magnitude;
        float force = 1 - (distance / explosionForceZombie);
        obj.AddForce(direction.normalized * explosionForceZombie * force, ForceMode2D.Impulse);
        StartCoroutine(StopZombie(obj));
    }

    IEnumerator StopZombie(Rigidbody2D obj) 
    {
        yield return new WaitForSeconds(0.1f);
        obj.gameObject.GetComponent<ZombieMovement>().enabled = true;
        obj.velocity = Vector2.zero;
    }
}
