using System;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{

    [Serializable]
    public struct TargetSpawn
    {
        public Vector3 position;
        public float time;
    }

    [SerializeField] float waitTime = 10;
    [SerializeField] Target targetPrefab;
    [SerializeField] float targetShowTime;
    [SerializeField] List<TargetSpawn> targetSpawns;

    int nextTargetIndex = 0;
    float startTime;

    void Start()
    {
        startTime = Time.time + waitTime;
        targetSpawns.Sort((a,b) => a.time.CompareTo(b.time));
    }

    void Update()
    {
        if (nextTargetIndex >= targetSpawns.Count) return;
        TargetSpawn nextTargetSpawn = targetSpawns[nextTargetIndex];

        if (Time.time - startTime > (nextTargetSpawn.time - targetShowTime))
        {
            Target target = Instantiate(targetPrefab);
            target.transform.position = nextTargetSpawn.position;
            target.SetTimes(Time.time, startTime + nextTargetSpawn.time);
            nextTargetIndex++;
        }
    }

    void OnDrawGizmosSelected()
    {
        foreach (TargetSpawn spawn in targetSpawns)
        {
           Gizmos.DrawSphere(spawn.position, 0.5f); 
        }
    }
}
