using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StoreTrig : MonoBehaviour
{
    public Transform store;
    public CanvasGroup background;
    public GameObject obj;
    public Data data;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Hero")
        {
            data.YoucantShoot = true;
            obj.SetActive(true);
            background.alpha = 0;
            background.LeanAlpha(1, 0.5f);

            store.localPosition = new Vector2(0, -Screen.height);
            store.LeanMoveLocalY(0, 0.5f).setEaseOutExpo().delay = 0.1f;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Hero")
        {
            data.YoucantShoot = false;
            background.LeanAlpha(0, 0.5f);
            store.LeanMoveLocalY(-Screen.height, 0.5f).setEaseInExpo().setOnComplete(Store);
        }
    }

    private void Store() => obj.SetActive(false);
}
