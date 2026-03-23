using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    [HideInInspector] public float score;
    [HideInInspector] public int hitCount;
    [HideInInspector] public int missCount;

    public static UI_Manager instance;

    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text hitText;
    [SerializeField] TMP_Text missText;
    [SerializeField] TMP_Text countdownText;
    [SerializeField] GameObject levelClearedObject;

    public float timeOffset;
    [SerializeField] float startTime;
    [SerializeField] float endTime;

    void Awake()
    {
        if (instance != null && instance != this && instance.gameObject.activeSelf) 
            Destroy(instance.gameObject);
        
        instance = this;
    }

    void Update()
    {
        scoreText.text = $"Score: {score}";
        hitText.text = $"Hit: {hitCount}";
        missText.text = $"Miss: {missCount}";

        if ((Time.time - timeOffset > startTime) || (startTime - Time.time + timeOffset > 3)) {
            countdownText.gameObject.SetActive(false);
        } else
        {
            countdownText.gameObject.SetActive(true);
            countdownText.text = Mathf.Ceil(startTime - Time.time + timeOffset).ToString();
        }

        if (Time.time - timeOffset > endTime)
        {
            levelClearedObject.SetActive(true);
        }
    }

    public void OnReloadPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnMainMenuPressed()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
