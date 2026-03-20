using UnityEngine;

public class StringGrabPoint : MonoBehaviour
{
    [SerializeField] Transform grabPoint;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnGrabRelease()
    {
        transform.position = grabPoint.position;
        rb.linearVelocity = Vector3.zero;
    }
}
