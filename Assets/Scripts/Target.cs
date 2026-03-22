using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour
{
    [SerializeField] float spawnTime;
    [SerializeField] float ringStartScale = 1.5f;
    [SerializeField] float ringEndScale = 1f;
    [SerializeField] float hitTime;
    [SerializeField] float graceTime = 0.2f;

    [SerializeField] GameObject ring;

    [SerializeField] GameObject model;

    void Start()
    {
        model.SetActive(false);
        // This is a bad way of spawning the target
        // and will lead to lag. Better to actually spawn it Int.
        Invoke("ActivateTarget", spawnTime);
    }

    void ActivateTarget()
    {
        Debug.Log("Showing");
        model.SetActive(true);
    }

    void Update()
    {
        float liveTime = Time.time - spawnTime;
        if (liveTime < 0) return;

        ring.transform.localScale = Vector3.one * Mathf.Lerp(ringStartScale, ringEndScale, liveTime / hitTime);
        
        if (liveTime > hitTime + graceTime)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision c)
    {
        Destroy(c.gameObject);
        Destroy(gameObject);

        // TODO: Add score
    }
}
