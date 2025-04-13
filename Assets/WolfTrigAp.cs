using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfTrigAp : MonoBehaviour
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
    public GameObject timer;

    public List<GameObject> spawns; 

    public GameObject EndTrig;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange && !data.DialogManager)
        {
            trigger.StartDialog();
            player.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            //contextClue.SetActive(false);
            Zone(zone1);
            trig = true;
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
        trig = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        Invoke("WolfOrdeAppear", 0.5f);
        Invoke("WorldDisappear", 25f);
    }

    private void WolfOrdeAppear() 
    {
        for (int i = 0; i < spawns.Count; i++)
        {
            spawns[i].SetActive(true);
        }
        timer.SetActive(true);
    }

    private void WorldDisappear() 
    {
        EndTrig.SetActive(true);
        for (int i = 0; i < spawns.Count; i++)
        {
            Destroy(spawns[i]);
        }
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
