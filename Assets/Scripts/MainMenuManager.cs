using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void OnPlayPressed()
    {
        SceneManager.LoadScene("Level 1");
    }
}
