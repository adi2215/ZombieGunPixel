using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandBox : MonoBehaviour
{
    public Transform box;

    public void OnActive()
    {
        box.LeanMoveLocalY(0, 0.5f).setEaseOutExpo().setOnComplete( () 
        => { Time.timeScale = 0; } ).delay = 0.1f;
    }

    public void OpenBox()
    {
        box.LeanMoveLocalY(-200f, 1f).setEaseOutQuad();
    }

    public void CloseBox()
    {
        box.LeanMoveLocalY(-Screen.height, 0.5f).setEaseInExpo().setOnComplete(OnComplete);
    }

    void OnComplete()
    {
        gameObject.SetActive(false);
    }
}
