using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using NaughtyAttributes;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [ShowAssetPreview(128, 128)]
    public Transform Player;
    public CinemachineVirtualCamera PlayerCamera;

    // Start is called before the first frame update
    void Awake()
    {
        PlayerCamera.Follow = Player;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
