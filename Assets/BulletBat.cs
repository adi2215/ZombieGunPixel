using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBat : MonoBehaviour
{
    public Data data;

    public float moveSpeed = 5f;
    
    private Rigidbody2D bulletRB;

    private GameObject target;

    Vector3 moveDir;

    public GameObject destroyAnim;

    private bool facingRight = false;

    
    private void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Hero");
        moveDir = (target.transform.position - transform.position).normalized;
        Invoke(nameof(DestroyBullet), 2f);
    }

    private void Update()
    {
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Health target = col.GetComponent<Health>();
        if (target != null)
        {
            target.GetHit(data.BatBullet, gameObject);
            DestroyBullet();
        }
    }

    public void DestroyBullet()
    {
        Instantiate(destroyAnim, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
