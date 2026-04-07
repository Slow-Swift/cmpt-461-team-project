using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void PlayLevel(int levelNumber)
    {
        SceneManager.LoadScene($"Level {levelNumber}");
    }
}
