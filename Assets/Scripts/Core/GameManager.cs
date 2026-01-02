using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gameOverUI;

    private float score;
    private bool isGameOver;

    void Awake()
    {
        Instance = this;
        gameOverUI.SetActive(false);
    }

    void Update()
    {
        if (isGameOver)
            return;

        score += Time.deltaTime;
        scoreText.text = $"Score: {Mathf.FloorToInt(score)}";
    }

    public void GameOver()
    {
        if (isGameOver)
            return;

        isGameOver = true;
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
