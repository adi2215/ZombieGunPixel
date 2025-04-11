using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrigEnd : MonoBehaviour
{
    public Data data;
    [SerializeField] private ManagerMenu nextLevel;
    private void OnTriggerEnter2D(Collider2D col)
    {
        Health target = col.GetComponent<Health>();
        if (target != null && data.countFuel >= 3)
        {
            Invoke("ColseZone", 0.3f);
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
