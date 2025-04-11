using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public List<GameObject> weapons;

    public void SortGun(int gun)
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].SetActive(false);
        }
        weapons[gun].SetActive(true);
    }

    private void Update() {
        FaceMouse();
    }

    private void FaceMouse() {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = transform.position - mousePosition;
        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        transform.right = -direction;

        Vector3 aimLocalScale = Vector3.one;

        if (angle > 90f || angle < -90f)
            aimLocalScale.y = -1f;
        else
            aimLocalScale.y = +1f;

        transform.localScale = aimLocalScale;
    }
}
