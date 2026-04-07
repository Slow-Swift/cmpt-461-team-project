using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    

    public static UI_Manager instance;

    [SerializeField] float maxHealth = 100;
    [SerializeField] float missHealth = 10;
    [SerializeField] float maxHitHealth = 10;

    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text hitText;
    [SerializeField] TMP_Text missText;
    [SerializeField] TMP_Text countdownText;
    [SerializeField] GameObject gameOverObject;
    [SerializeField] GameObject levelFailedText;
    [SerializeField] GameObject levelCompleteText;
    [SerializeField] TMP_Text comboText;
    [SerializeField] Image healthImage;
    [SerializeField] TargetManager targetManager;

    public float timeOffset;
    [SerializeField] float startTime;
    [SerializeField] float endTime;

    [SerializeField] float comboMultiplier = 0.25f;

    float health;
    float score;
    int hitCount;
    int missCount;
    bool gameOver = false;
    int combo = 0;

    void Awake()
    {
        if (instance != null && instance != this && instance.gameObject.activeSelf) 
            Destroy(instance.gameObject);
        
        instance = this;
        health = maxHealth;
        UpdateComboText();
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

        if (!gameOver && (Time.time - timeOffset > endTime))
        {
            OnLevelComplete();
        }
    }

    public void AddScore(int amount)
    {
        hitCount += 1;
        score += Mathf.Floor(amount * (combo * comboMultiplier + 1));
        health += (amount / 100f) * maxHitHealth;
        health = Mathf.Clamp(health, 0, maxHealth);
        healthImage.fillAmount = health / maxHealth;
        combo++;
        UpdateComboText();
    }

    void UpdateComboText()
    {
        comboText.text = $"{combo} \n {combo * comboMultiplier + 1}x";
    }

    public void OnMiss()
    {
        if (gameOver) return;

        missCount++;
        health -= missHealth;
        healthImage.fillAmount = health / maxHealth;
        combo = 0;
        UpdateComboText();

        if (health <= 0)
        {
            OnLevelFailed();
        }
    }

    void OnLevelComplete()
    {
        gameOver = true;
        gameOverObject.SetActive(true);
        levelCompleteText.SetActive(true);
    }

    void OnLevelFailed()
    {
        gameOver = true;
        gameOverObject.SetActive(true);
        levelFailedText.SetActive(true);
        targetManager.CancelSpawns();

        Target[] targets = FindObjectsByType<Target>(FindObjectsSortMode.None);
        foreach (Target target in targets)
        {
            target.OnLevelFailed();
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
