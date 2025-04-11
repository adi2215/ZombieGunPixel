using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondTrig : MonoBehaviour
{
    public LetGo contextOn;

    public LetGo contextOff;

    public bool playerInRange;

    public DialogTrigger trigger;

    public bool currentPlayer = true;

    public GameObject contextClue;
    public GameObject zone1;
    public Data data;
    public GameObject player;

    private bool animChatacter = false;

    private void Update()
    {
        if (animChatacter && !data.DialogManager)
        {
            player.GetComponent<Animator>().Play("GoOut");
            data.DialogManager = true;
        }

        if (Input.GetKeyDown(KeyCode.E) && playerInRange && !data.DialogManager && data.countCoins > 7)
        {
            trigger.StartDialog();
            contextClue.SetActive(false);
            Zone(zone1);
            animChatacter = true;
        }
    }

    public void AnimFinished()
    {
        player.SetActive(false);
        data.DialogManager = false;
        animChatacter = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Hero" && !data.DialogManager && data.countCoins > 7)
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
