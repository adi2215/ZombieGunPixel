using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Erlik : MonoBehaviour
{
    public GameObject Target;
    public GameObject Wall;
    public GameObject Iceball;
    public GameObject[] Mob;
    public int health = 50;
    public Data data;

    public void StartFighr()
    {
        //Shoot();
        Target = GameObject.FindGameObjectWithTag("Hero");
        StartCoroutine(Fight());
    }
    public IEnumerator Fight()
    {
        //yield return new WaitForSeconds(10f);
        //Spawn();
        yield return new WaitForSeconds(0.5f);
        while (data.bossHp > 0) {
            Shoot();
            yield return new WaitForSeconds(3f);
            Spawn();
            yield return new WaitForSeconds(7f);
        }
        health = 20;
        while (data.bossHp > 0)
        {
            var angle = Random.Range(0, 2 * Mathf.PI);
            var len = Random.Range(5, 10);
            GameObject obj = Instantiate(Iceball, transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * len, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
    }
    void Update()
    {
        if (Target.transform.position.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    void Shoot()
    {
        GetComponent<Animator>().SetTrigger("Magic");
        for (int i = 0; i < 3; ++i)
        {
            float alpha = i * Mathf.PI * 2 / 4;
            Instantiate(Iceball, Target.transform.position + new Vector3(Mathf.Cos(alpha), Mathf.Sin(alpha), 0) * 10, Quaternion.identity);
        }
        for (int i = 0; i < 12; ++i)
        {
            float alpha = i * Mathf.PI * 2 / 12;
            GameObject obj = Instantiate(Wall, transform.position + new Vector3(Mathf.Cos(alpha), Mathf.Sin(alpha), 0) * 2, Quaternion.identity);
            obj.transform.localScale = new Vector3(6, 0, 6);
        }
    }
    void Spawn()
    {
        StartCoroutine(Summoning());
    }
    IEnumerator Summoning()
    {
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < 5; ++i)
        {
            var angle = Random.Range(1, 2 * Mathf.PI);
            var len = Random.Range(7, 8);
            GameObject obj = Instantiate(Mob[Random.Range(0, Mob.Length)], transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * len, Quaternion.identity);
            obj.transform.localScale = new Vector3(1, 1, 1);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
