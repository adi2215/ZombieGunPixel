using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public Message[] messages;
    public Actor[] actors;
    public DialogManager Box;

    public SaveDialog countDialog;

    public void StartDialog()
    {
        Box.OpenDialogue(messages, actors);
    }
}

[System.Serializable]
public class Message {
    public int actorId;
    public string message;
}

[System.Serializable]
public class Actor {
    public string name;
    public Sprite sprite;
}
