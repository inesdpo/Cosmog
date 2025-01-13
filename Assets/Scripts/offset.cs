using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class offset : MonoBehaviour
{
    private float desiredHeight = 0.8f; // Desired height in meters

    private XROrigin xrOrigin;

    void Update()
    {
  
        xrOrigin = GetComponent<XROrigin>();

        if (xrOrigin != null)
        {
            // Get the Camera Offset Transform
            Transform cameraOffset = xrOrigin.CameraFloorOffsetObject.transform;

            // Adjust the Y position of the Camera Offset
            Vector3 offsetPosition = cameraOffset.localPosition;
            offsetPosition.y = desiredHeight;
            cameraOffset.localPosition = offsetPosition;
        }
    }
}
