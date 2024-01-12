using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrigScene : MonoBehaviour
{
    public GameObject player;
    [SerializeField] private ManagerMenu nextLevel;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("ef");
        if (collision.gameObject.CompareTag("Hero"))
        {
            player.GetComponent<PlayerMovement>().enabled = false;
            Invoke("NewScene", 0.3f);
        }
    }

    private void NewScene()
    {
        nextLevel.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
