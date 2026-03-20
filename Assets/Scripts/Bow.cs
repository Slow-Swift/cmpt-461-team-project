using UnityEngine;

public class Bow : MonoBehaviour
{
    [SerializeField] Transform grabPoint;
    [SerializeField] Transform arrowBowPoint;
    [SerializeField] GameObject arrow;

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
        GameObject firedArrow = Instantiate(arrow, arrow.transform.position, arrow.transform.rotation);
        Rigidbody firedRb = firedArrow.GetComponent<Rigidbody>();
        firedRb.isKinematic = false;
        firedRb.AddForce(arrow.transform.forward * 2000);
        arrow.SetActive(false);
    }
}
