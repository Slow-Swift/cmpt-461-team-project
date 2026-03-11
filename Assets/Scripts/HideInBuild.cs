using UnityEngine;

public class HideInBuild : MonoBehaviour
{
    void Start()
    {
        if (!Application.isEditor)
        {
            gameObject.SetActive(false);
        }
    }
}
