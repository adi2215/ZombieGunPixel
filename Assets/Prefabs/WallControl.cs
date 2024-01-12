using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallControl : MonoBehaviour
{
    public Vector3 Center;
    public float decay = 0;
    public float rise = 0;
    void Start()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        transform.localScale = new Vector3(6, 0, 6);
        //rise = 0.3f;

        StartCoroutine(Main());
    }

    IEnumerator Main()
    {
        rise = 0.3f;
        yield return new WaitForSeconds(7f);
        decay = 0.3f;
    }

    void Update()
    {
        //Debug.Log(rise.ToString() + ", " + decay.ToString() + "=" + transform.localScale.y.ToString());
        if (rise > 0)
        {
            transform.localScale += new Vector3(0, 70f * Time.deltaTime * (Mathf.Cos(Mathf.PI * (0.3f - rise) / 0.3f) + 0.5f), 0);
            rise -= Time.deltaTime;
        }
        else if (decay > 0)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            transform.localScale -= new Vector3(0, 70f * Time.deltaTime * (Mathf.Cos(Mathf.PI * (0.3f - decay) / 0.3f) + 0.5f), 0);
            if (transform.localScale.y < 1)
            {
                Destroy(gameObject);
            }
            decay -= Time.deltaTime;
        }
        //else
        //{
        //    transform.localScale = new Vector3(15, 15, 15);
        //    transform.position += (Center - transform.position) * (Time.deltaTime * 0.2f);
        //    if (Vector3.Distance(transform.position, Center) < 2)
        //    {
        //        decay = 1f;
        //    }
        //}
    }
}
