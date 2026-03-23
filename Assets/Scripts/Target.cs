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

    public void SetTimes(float spawnTime, float hitTime)
    {
        this.spawnTime = spawnTime;
        this.hitTime = hitTime;
    }

    void Update()
    {
        float liveTime = Time.time - spawnTime;

        ring.transform.localScale = Vector3.one * Mathf.Lerp(ringStartScale, ringEndScale, liveTime / (hitTime - spawnTime));
        
        if (Time.time > hitTime + graceTime)
        {
            Destroy(gameObject);
            if (UI_Manager.instance != null)
            {
                UI_Manager.instance.missCount++;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
        Destroy(this.gameObject);
        if (UI_Manager.instance != null)
        {
            UI_Manager.instance.hitCount++;
            UI_Manager.instance.score += 1; // TODO: Improve score
        }
    }
}
