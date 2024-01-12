using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CinamachineShake : MonoBehaviour
{
    public static CinamachineShake Instance {get; private set; }
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    public AnimationCurve curveShake;
    CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;
    private float shakeTimer;
    private float shakeTimerTotal;
    private float startInstensity;

    private void Awake()
    {
        Instance = this;
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        cinemachineBasicMultiChannelPerlin = 
            cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        shakeTimer = time;
        shakeTimerTotal = time;
        startInstensity = intensity;
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            cinemachineBasicMultiChannelPerlin = 
                cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(startInstensity, 0f, 1 - shakeTimer / shakeTimerTotal);
        }
    }
}
