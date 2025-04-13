using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZombieChecs : MonoBehaviour
{
    public GameObject Target;
    public Data data;
    public bool taking = false;
    public Health hp;
    public ManagerMenu menu;

    private void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Hero");
    }
    private void Update()
    {
        if (Vector3.Distance(transform.position, Target.transform.position) < 1)
        {
            transform.position += Vector3.Normalize(Target.transform.position - transform.position) * (Vector3.Distance(transform.position, Target.transform.position)/2) * 18f * Time.deltaTime;
        } 
        else if (Vector3.Distance(transform.position, Target.transform.position) < 5)
        {
            transform.position += Vector3.Normalize(Target.transform.position - transform.position) * 3f * Time.deltaTime;
            //Debug.Log(Target.transform.position.ToString() + Vector3.Normalize(Target.transform.position - transform.position).ToString());
        }
        else if (Vector3.Distance(transform.position, Target.transform.position) < 10)
        {
            transform.position += Vector3.Normalize(Target.transform.position - transform.position) * 8f * Time.deltaTime;
            //Debug.Log(Target.transform.position.ToString() + Vector3.Normalize(Target.transform.position - transform.position).ToString());
        }
        else
        {
            transform.position += Vector3.Normalize(Target.transform.position - transform.position) * 8f * Time.deltaTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Hero"))
        {
            hp.Die();
            menu.LoadLevel(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
