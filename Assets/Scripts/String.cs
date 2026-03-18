using UnityEngine;
using System.Collections.Generic;

public class String : MonoBehaviour
{
    [SerializeField] List<Transform> stringPositions;

    LineRenderer lRenderer;

    void Start()
    {
        lRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        for (int i = 0; i < stringPositions.Count; i++)
        {
            lRenderer.SetPosition(i, stringPositions[i].position);
        }
    }
}
