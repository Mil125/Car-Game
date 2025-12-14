using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [Header("References")]
    public HealthSystem healthSystem;
    public FuelSystem fuelSystem;

    [Header("UI")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI reasonText;

    private bool isGameOver = false;

    void Start()
    {
        // Ensure game runs
        Time.timeScale = 1f;

        // Hide panel on start
        if (gameOverPanel)
            gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (isGameOver)
        {
            // Restart
            if (Input.GetKeyDown(KeyCode.R))
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            return;
        }

        if (healthSystem && healthSystem.currentHealth <= 0f)
        {
            TriggerGameOver("No health\nPress R to Restart");
        }
        else if (fuelSystem && fuelSystem.currentFuel <= 0f)
        {
            TriggerGameOver("Out of fuel\nPress R to Restart");
        }
    }

    void TriggerGameOver(string reason)
    {
        if (isGameOver)
            return;

        isGameOver = true;

        // IMPORTANT: enable UI FIRST
        if (gameOverPanel)
            gameOverPanel.SetActive(true);

        if (reasonText)
            reasonText.text = reason;

        // Pause game AFTER UI is visible
        Time.timeScale = 0f;
    }
}
