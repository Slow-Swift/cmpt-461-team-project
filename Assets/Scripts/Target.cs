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
    [SerializeField] Animator animator;
    [SerializeField] Gradient ringGradient;
    [SerializeField] AudioSource hitSource;

    bool hit = false;
    MaterialPropertyBlock mbp;
    MeshRenderer ringRenderer;

    void Awake()
    {
        ringRenderer = ring.GetComponent<MeshRenderer>();
        mbp = new MaterialPropertyBlock();
    }

    public void SetTimes(float spawnTime, float hitTime)
    {
        this.spawnTime = spawnTime;
        this.hitTime = hitTime;
    }

    void Update()
    {
        float liveTime = Time.time - spawnTime;

        float livePercentage = liveTime / (hitTime - spawnTime);
        float diePercentage = (liveTime - hitTime + spawnTime) / graceTime;

        ring.transform.localScale = Vector3.one * Mathf.Clamp(
            Mathf.LerpUnclamped(ringStartScale, ringEndScale, livePercentage), 0, ringStartScale
        );
        
        Color ringColor = livePercentage < 1 ? ringGradient.Evaluate(livePercentage) : ringGradient.Evaluate(1 - diePercentage);
        mbp.SetColor("_BaseColor", ringColor);
        ringRenderer.SetPropertyBlock(mbp);

        
        if (!hit && Time.time > hitTime + graceTime)
        {
            Miss();
        }
    }

    void Miss()
    {
        animator.SetTrigger("Miss");
        Destroy(gameObject, 1000);
        GetComponent<SphereCollider>().enabled = false;
        enabled = false;
        UI_Manager.instance?.OnMiss();
    }

    void OnCollisionEnter(Collision collision)
    {
        hit = true;
        hitSource.Play();
        Destroy(collision.gameObject);
        animator.SetTrigger("Hit");
        Destroy(gameObject, 1000);
        GetComponent<SphereCollider>().enabled = false;

        if (UI_Manager.instance != null)
        {
            float liveTime = Time.time - spawnTime;
            float livePercentage = liveTime / (hitTime - spawnTime);
            float diePercentage = (liveTime - hitTime + spawnTime) / graceTime;
            float scorePercent = livePercentage < 1 ? livePercentage : 1 - diePercentage;
            float score = 100 * Mathf.Clamp(scorePercent * scorePercent, 0, 1);

            UI_Manager.instance?.AddScore((int)Mathf.Floor(score));
        }
    }

    public void OnLevelFailed()
    {
        Miss();
    }
}
