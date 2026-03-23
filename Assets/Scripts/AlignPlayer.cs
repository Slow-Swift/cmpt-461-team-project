using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;

public class AlignPlayer : MonoBehaviour
{
    // It's actually so stupid that I have to do this.
    // And it took me so long to work this out.


    [SerializeField] Transform cameraTransform;

    IEnumerator Start()
    {
        // Wait until camera actually moves from origin
        while (cameraTransform.position == Vector3.zero)
        {
            yield return null;
        }

        // Wait an extra frame to stabilize
        yield return null;

        Vector3 camPos = cameraTransform.position;

        // Only adjust horizontal offset
        Vector3 offset = new Vector3(camPos.x, 0f, camPos.z);

        transform.position -= offset;
    }
}
