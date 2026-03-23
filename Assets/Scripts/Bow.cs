using UnityEngine;

public class Bow : MonoBehaviour
{
    [SerializeField] Transform grabPoint;
    [SerializeField] Transform arrowBowPoint;
    [SerializeField] GameObject arrow;

    [SerializeField] float maxFireForce;
    [SerializeField] float minDrawPercent;
    [SerializeField] StringGrabPoint drawManager;

    void Update()
    {
        arrow.transform.position = grabPoint.position;
        arrow.transform.LookAt(arrowBowPoint);
    }

    public void OnGrab()
    {
        arrow.SetActive(true);
    }

    public void OnRelease()
    {
        if (drawManager.DrawPercent >= minDrawPercent)
        {
            GameObject firedArrow = Instantiate(arrow, arrow.transform.position, arrow.transform.rotation);
            Rigidbody firedRb = firedArrow.GetComponent<Rigidbody>();
            firedRb.isKinematic = false;
            float fireForce = maxFireForce * drawManager.DrawPercent;
            firedRb.AddForce(arrow.transform.forward * fireForce);
        }
        arrow.SetActive(false);
    }
}
