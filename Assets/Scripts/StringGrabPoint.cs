using System;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;

public class StringGrabPoint : MonoBehaviour
{
    [SerializeField] Transform startPoint;
    [SerializeField] Transform stringPoint;
    [SerializeField] float maxDistance;

    [Header("Haptics")]
    [SerializeField] HapticImpulsePlayer controller;
    [SerializeField] [Range(0,1)] float maxHapticAmplitude = 0.7f;
    [SerializeField] float hapticDuration = 0.05f;

    public float DrawPercent { get; private set; }

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 startToGrab = transform.position - startPoint.position;
        Vector3 stringPos = startPoint.position + (startToGrab.normalized * Mathf.Clamp(startToGrab.magnitude, 0, maxDistance));
        DrawPercent = Mathf.Clamp(startToGrab.magnitude / maxDistance, 0, 1);
        stringPoint.position = stringPos;
        UpdateHaptics();
    }

    void UpdateHaptics()
    {
        if (controller == null) return;

        float curvedPercent = DrawPercent * DrawPercent;
        float pulse = Mathf.Sin(Time.time * 50f) * 0.3f + 0.7f;
        float amplitude = curvedPercent * pulse * maxHapticAmplitude;
        if (amplitude > 0.01f)
        {
            controller.SendHapticImpulse(amplitude, hapticDuration);
        }
    }

    public void OnGrabRelease()
    {
        transform.position = startPoint.position;
    }
}
