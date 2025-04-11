using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigDialog2 : MonoBehaviour
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
    public bool trig = false;
    public GameObject fuel;
    public GameObject ZombieOrde;
    public GameObject survivor;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange && !data.DialogManager)
        {
            trigger.StartDialog();
            player.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            //contextClue.SetActive(false);
            Zone(zone1);
            trig = true;
            fuel.SetActive(true);
            survivor.SetActive(false);
        }

        if (!data.DialogManager && trig)
        {
            player.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            Debug.Log(player.canMove);
            AnimFinished();
        }
    }

    public void AnimFinished()
    {
        data.DialogManager = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        Invoke("ZombieOrdeAppear", 1f);
    }

    private void ZombieOrdeAppear() => ZombieOrde.SetActive(true);

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
