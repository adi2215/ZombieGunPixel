using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigDialaog : MonoBehaviour
{
    public LetGo contextOn;

    public LetGo contextOff;

    public bool playerInRange;

    public DialogTrigger trigger;

    public bool currentPlayer = true;

    public GameObject contextClue;
    public GameObject zone1;
    public Data data;
    public PlayerMovement player;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange && !data.DialogManager)
        {
            trigger.StartDialog();
            //contextClue.SetActive(false);
            Zone(zone1);
        }
    }

    public void AnimFinished()
    {
        data.DialogManager = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Hero" && !data.DialogManager)
        {
            contextOn.Raise();
            playerInRange = true;
        }
    }

    private void Zone(GameObject gm) => gm.SetActive(false);
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Hero")
        {
            contextOff.Raise();
            playerInRange = false;
        }
            
    }
}
