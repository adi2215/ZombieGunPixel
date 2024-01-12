using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iceball : MonoBehaviour
{
    GameObject Target;
    Vector3 direction;
    public float angularSpeed;
    public float movementSpeed;
    float aim = 4;
    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Hero");
        GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1f);
    }
    void Update()
    {
        aim -= Time.deltaTime;
        if (aim > 2)
        {
            direction = Vector3.Normalize(Target.transform.position - transform.position);
            //float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //gameObject.transform.rotation = Quaternion.Euler(0, 0, rotZ);
            //GetComponent<Rigidbody2D>().AddForce(direction * 3, ForceMode2D.Force);
        }
        if (aim < 0)
        {
            //Debug.Log()
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = transform.GetChild(0).GetComponent<SpriteRenderer>().color - new Color(0, 0, 0, Time.deltaTime/1.5f);
        }
        if (aim < -1.5)
        {
            Destroy(gameObject);
        }
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        GetComponent<Rigidbody2D>().angularVelocity = -angularSpeed * rotateAmount;
        GetComponent<Rigidbody2D>().velocity = transform.up * movementSpeed;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Hero"))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            Invoke("DestroyingObject", 0.1f);
        }
    }

    private void DestroyingObject()
    {
        Destroy(gameObject);
    }
}
