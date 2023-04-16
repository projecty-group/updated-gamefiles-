using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public Camera mainCamera;
    public Camera orthoCamera;
    public KeyCode startKey;

    void Start()
    {
        mainCamera.enabled = true;
        orthoCamera.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(startKey))
        {
            mainCamera.enabled = !mainCamera.enabled;
            orthoCamera.enabled = !orthoCamera.enabled;
        }
    }
}


