using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrigEndd3 : MonoBehaviour
{
    [SerializeField] private ManagerMenu nextLevel;
    private void OnTriggerEnter2D(Collider2D col)
    {
        Health target = col.GetComponent<Health>();
        if (target != null)
        {
            ColseZone();
        }
    }

    public void ColseZone()
    {
        Invoke("NewScene", 0.5f);
    }

    private void NewScene()
    {
        nextLevel.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
