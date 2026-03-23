using TMPro;
using UnityEngine;

public class DebugInfo : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    [SerializeField] Transform xr_origin;
    [SerializeField] Transform camera_offset;
    [SerializeField] new Transform camera;

    // Update is called once per frame
    void Update()
    {
        text.text = $"XR Position: <{xr_origin.localPosition.x}, {xr_origin.localPosition.y}, {xr_origin.localPosition.z}> \n CAM offset Position: <{camera_offset.localPosition.x}, {camera_offset.localPosition.y}, {camera_offset.localPosition.z}> \n CAM offset Position: <{camera.localPosition.x}, {camera.localPosition.y}, {camera.localPosition.z}>";
    }
}
