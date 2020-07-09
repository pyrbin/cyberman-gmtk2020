using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.Mathematics;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance { get; private set; }
    public static void ShakeCamera(float intensity, float time = 0.66f)
    {
        Instance.Shake(intensity, time);
    }

    private CinemachineVirtualCamera cmCam;
    private float shakeTimer;
    private float shakeTimerTotal;
    private float startingIntensity;

    private CinemachineBasicMultiChannelPerlin Perlin
        => cmCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

    public void Shake(float intensity, float time)
    {
        startingIntensity = intensity;
        shakeTimerTotal = time;
        shakeTimer = time;
    }

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        cmCam = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeTimer > 0f)
        {
            shakeTimer -= Time.deltaTime;
            Perlin.m_AmplitudeGain = math.lerp(startingIntensity, 0f, 1 - (shakeTimer / shakeTimerTotal));
        }
    }
}
