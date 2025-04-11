using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AimShootManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerAimGun;

    public AnimationCurve mainCurve;

    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

    [SerializeField] private Transform pfBullet;
    [SerializeField] private Transform pfGranate;
    [SerializeField] private Transform pfRayscastGranate;

    private void Start() {
        playerAimGun.OnShoot += PlayerAimGun_OnShoot;
        playerAimGun.OnShootGranate += PlayerAimGranate_OnShoot;
    }

    /*private void Update()
    {
        if (start) 
        {
            //start = false;
            StartCoroutine(Shaking(mainCurve));
        }
    }*/

    private void PlayerAimGun_OnShoot(object sender, PlayerMovement.OnShootEventsArgs e)
    {
        Transform bulletTransform = Instantiate(pfBullet, e.gunEndPointPosition, Quaternion.identity);

        Vector3 shootDir = (e.shootPosition - e.gunEndPointPosition).normalized;
        bulletTransform.GetComponent<Bullet>().Setup(shootDir);
    }

    private void PlayerAimGranate_OnShoot(object sender, PlayerMovement.OnShootEventsArg e)
    {
        Transform bulletTransform = Instantiate(pfGranate, e.PlayerEndPointPosition, Quaternion.identity);
        Transform granatePoint = Instantiate(pfRayscastGranate, e.shootPosition, Quaternion.identity);

        Vector3 shootDir = (e.shootPosition - e.PlayerEndPointPosition).normalized;
        bulletTransform.GetComponent<GranateMovement>().Setup(granatePoint.position, shootDir);
    }

    /*IEnumerator Shaking(AnimationCurve curve)
    {
        Vector3 startPosition = Random.insideUnitCircle;
        float elapsedTime = 0f;

        while (elapsedTime < duration / 4) 
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            Vector2 randomMovement = startPosition.normalized * strength * speedShake;
            cinemachineVirtualCamera.transform.position += startPosition;
            startPosition = randomMovement;
            yield return null;
        }
    }*/
}
