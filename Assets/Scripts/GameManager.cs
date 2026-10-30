using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Progress")]
    [SerializeField, Min(0)] private int totalCoins = 10;
    [SerializeField, Min(0)] private int score = 0;

    [Header("UI")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject gameOverPanel;

    public int Score => score;
    public int TotalCoins => totalCoins;
    public bool IsGameRunning { get; private set; }

    private bool hasEnded;
    private AudioSource effectsSource;
    private AudioClip coinPickupClip;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("More than one GameManager was found. Removing the duplicate.", this);
            Destroy(gameObject);
            return;
        }

        Instance = this;

        effectsSource = gameObject.AddComponent<AudioSource>();
        effectsSource.playOnAwake = false;
        effectsSource.loop = false;
        effectsSource.spatialBlend = 0f;
        effectsSource.volume = 0.65f;
        effectsSource.priority = 32;
    }

    private void Start()
    {
        CoursePolisher.Apply();

        coinPickupClip = Resources.Load<AudioClip>("Audio/CoinPickup");
        if (coinPickupClip == null)
            Debug.LogWarning("Coin pickup sound could not be loaded from Resources/Audio/CoinPickup.");

        Time.timeScale = 0f;
        score = 0;
        hasEnded = false;
        IsGameRunning = false;

        // Keep the target in sync with the actual scene instead of relying on a hard-coded number.
        CoinCollector[] coins = FindObjectsByType<CoinCollector>(FindObjectsSortMode.None);
        if (coins.Length > 0)
            totalCoins = coins.Length;

        SetActive(startPanel, true);
        SetActive(winPanel, false);
        SetActive(gameOverPanel, false);
        UpdateScore();
    }

    public void StartGame()
    {
        if (hasEnded)
            return;

        SetActive(startPanel, false);
        IsGameRunning = true;
        Time.timeScale = 1f;
    }

    public void CollectCoin(GameObject coin)
    {
        if (hasEnded || coin == null)
            return;

        if (coinPickupClip != null && effectsSource != null)
            effectsSource.PlayOneShot(coinPickupClip);

        score = Mathf.Min(score + 1, totalCoins);
        Destroy(coin);
        UpdateScore();
    }

    public void ReachFinish()
    {
        if (hasEnded || !IsGameRunning)
            return;

        if (score < totalCoins)
        {
            Debug.Log($"Collect all coins before finishing ({score}/{totalCoins}).");
            return;
        }

        EndGame(winPanel);
    }

    public void GameOver()
    {
        if (hasEnded || !IsGameRunning)
            return;

        EndGame(gameOverPanel);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void EndGame(GameObject panel)
    {
        hasEnded = true;
        IsGameRunning = false;
        SetActive(panel, true);
        Time.timeScale = 0f;
    }

    private void UpdateScore()
    {
        if (scoreText != null)
            scoreText.text = $"Coins: {score} / {totalCoins}";
    }

    private static void SetActive(GameObject target, bool active)
    {
        if (target != null)
            target.SetActive(active);
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
}
