using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Data data;

    private Vector3 shootDir;

    public GameObject destroyAnim;

    public float moveSpeed = 16f;

    public GameObject effectSmile;

    public void Setup(Vector3 shootDir)
    {
        this.shootDir = shootDir;
        GameObject aimAngle = GameObject.FindGameObjectWithTag("Gun");
        transform.eulerAngles = new Vector3(0, 0, aimAngle.transform.eulerAngles.z);
        Invoke(nameof(DestroyBullet), data.destroyBullet);
    }

    private void Update()
    {
        transform.position += shootDir * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Target target = col.GetComponent<Target>();
        if (target != null)
        {
            target.TakeDamage(data.damage);
            GameObject clone = Instantiate(effectSmile, transform.position, Quaternion.identity);
            Destroy (clone, 1.0f);
            DestroyBullet();
        }
        if (col.gameObject.tag == "Spike")
        {
            DestroyBullet();
        }
    }

    public void DestroyBullet()
    {
        Instantiate(destroyAnim, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
