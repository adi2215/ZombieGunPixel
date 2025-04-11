using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrigBoss : MonoBehaviour
{
    public GameObject obj;
    public GameObject objBlock;

    public GameObject Line;

    public ManagerMenu menu;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Hero")
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Line.SetActive(false);
            obj.SetActive(true);
            objBlock.SetActive(true);
        }
    }

    public void NextScene()
    {
        Invoke("ManagerScene", 1.5f);
    }

    private void ManagerScene()
    {
        menu.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
