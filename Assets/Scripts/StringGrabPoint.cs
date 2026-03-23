using UnityEngine;

public class StringGrabPoint : MonoBehaviour
{
    [SerializeField] Transform startPoint;
    [SerializeField] Transform stringPoint;
    [SerializeField] float maxDistance;

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
    }

    public void OnGrabRelease()
    {
        transform.position = startPoint.position;
        rb.linearVelocity = Vector3.zero;
    }
}
