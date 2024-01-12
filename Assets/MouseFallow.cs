using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MouseFallow : MonoBehaviour
{
    public event EventHandler<OnShootEventsArgs> OnShoot;
    public class OnShootEventsArgs : EventArgs {
        public Vector3 gunEndPointPosition;
        public Vector3 shootPosition;
    }

    [SerializeField] private Transform aimTransform;
    [SerializeField] private Transform aimGunEndPosintTransform;
    [SerializeField] private AudioSource shootSound;
    [SerializeField] private GameObject gunLight;

    public Animator aimAnimator;
    public float offset;
    public PlayerMovement mainCheck;
    public float timebet = 0.3f;

    private void Start()
    {
        mainCheck = GetComponent<PlayerMovement>();
        //aimAnimator = aimAnimator.GetComponent<Animator>();
    }

    void Update()
    {
        /*FaceMouse();
        Shooting();*/
    }

    /*private void FaceMouse()
    {
        Vector3 mousePosition = GetMouseWorldPosition();
        double distance = Math.Pow(mousePosition.x - transform.position.x, 2) + Math.Pow(mousePosition.y - transform.position.y - 0.25f, 2);
        if (distance > 0f)
        {
            Vector3 aimDirection = mousePosition - aimTransform.transform.position;
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            aimTransform.rotation = Quaternion.Euler(0, 0, angle + offset);
            Vector3 aimLocalScale = Vector3.one;
            if (angle > 90 || angle < -90) {
                aimLocalScale.y = +1f;
            } else {
                aimLocalScale.y = -1f;
            }
            aimTransform.localScale = aimLocalScale;
        }
    }*/

    /*private void Shooting()
    {
        timebet -= Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && timebet < 0)
        {
            timebet = 0.3f;
            Vector3 mousePosition = GetMouseWorldPosition();

            aimAnimator.SetTrigger("Shoot");
            shootSound.Play();
            gunLight.SetActive(true);
            
            OnShoot?.Invoke(this, new OnShootEventsArgs {
                gunEndPointPosition = aimGunEndPosintTransform.position, 
                shootPosition = mousePosition,
            });
        }

        if (timebet < 0)
        {
            gunLight.SetActive(false);
        }
    }*/


    /*public static Vector3 GetMouseWorldPosition() {
            Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
            vec.z = 0f;
            return vec;
        }
        public static Vector3 GetMouseWorldPositionWithZ() {
            return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        }
        public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera) {
            return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
        }
        public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera) {
            Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
            return worldPosition;
        }*/
}
