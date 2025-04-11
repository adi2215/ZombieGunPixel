using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightTurn : MonoBehaviour
{
    [SerializeField] private float speed;

    void Update()
    {
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = new Vector2(mouse.x - transform.position.x, mouse.y - transform.position.y);
        transform.up = Vector3.Lerp(transform.up, dir, Time.deltaTime * speed);
    }
}
