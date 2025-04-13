using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Transform target;

    public float something;

    public Vector2 maxPos;
    public Vector2 minPos;

    public Camera myCamera;

    private void FixedUpdate()
    {
        if (transform.position != target.position)
        {
            Vector3 targetPos = new Vector3(target.position.x, target.position.y, -10f);

            targetPos.x = Mathf.Clamp(targetPos.x, minPos.x, maxPos.x);

            transform.position = Vector3.Lerp(transform.position, targetPos + new Vector3(2f, 0f, 0f), something);
        }
    }
}
