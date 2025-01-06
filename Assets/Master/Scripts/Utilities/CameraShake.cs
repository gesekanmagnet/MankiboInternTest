using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }

    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private float intesity;

    private CinemachineBasicMultiChannelPerlin perlin;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        perlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void OnEnable()
    {
         perlin.m_AmplitudeGain = intesity;
    }

    private void Update()
    {
        if(perlin.m_AmplitudeGain <= 0)
            this.enabled = false;

        perlin.m_AmplitudeGain -= Time.deltaTime * 10;
        perlin.m_AmplitudeGain = Mathf.Clamp(perlin.m_AmplitudeGain, 0, intesity);
    }
}
