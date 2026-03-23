using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;

public class LevelManager : MonoBehaviour
{

    [SerializeField] XROrigin xrOrigin;

    void Start()
    {
        xrOrigin.MoveCameraToWorldLocation(Vector3.zero);
    }
}
