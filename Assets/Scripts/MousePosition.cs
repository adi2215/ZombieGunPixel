using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform followTransform;
    [SerializeField] private bool cameraPositionWithMouse;

    [SerializeField] private Texture2D cursorTexture;

    private Vector2 cursorHotSpot;

    private Vector3 mouseWorldPosition;
    

    private void Start()
    {
        cursorHotSpot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);
        Cursor.SetCursor(cursorTexture, cursorHotSpot, CursorMode.Auto);
        Cursor.lockState = CursorLockMode.Confined;
    }
    
    private void Update()
    {
        mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        transform.position = mouseWorldPosition;
    }
}
