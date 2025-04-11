using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImage : MonoBehaviour
{
    public float ghostDelay;
    private float ghostDelaySeconds;
    public GameObject ghost;
    public bool makeGhost = false;

    void Start()
    {
        ghostDelaySeconds = ghostDelay;
    }

    void Update()
    {
        if (!makeGhost)
            return;

        if (ghostDelaySeconds > 0)
        {
            ghostDelaySeconds -= Time.deltaTime; 
        }

        else
        {
            GameObject currentGhost = Instantiate(ghost, transform.position, transform.rotation);
            Sprite currentSprite = GetComponent<SpriteRenderer>().sprite;
            currentGhost.GetComponent<SpriteRenderer>().sprite = currentSprite;
            ghostDelaySeconds = ghostDelay;
            Destroy(currentGhost, 1f);
        }
    }
}
