using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FinishManager : MonoBehaviour
{
    [Header("References")]
    public Transform car;        // Car from hierarchy
    public float finishZ = 1550f;

    [Header("UI")]
    public GameObject winPanel;  // WinPanel
    public TextMeshProUGUI winText;

    private bool hasWon = false;

    void Start()
    {
        Time.timeScale = 1f;

        if (winPanel)
            winPanel.SetActive(false);
    }

    void Update()
    {
        // After win → only restart
        if (hasWon)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            return;
        }

        // WIN CONDITION
        if (car && car.position.z >= finishZ)
        {
            Win();
        }
    }

    void Win()
    {
        hasWon = true;

        if (winPanel)
            winPanel.SetActive(true);

        if (winText)
            winText.text = "YOU WON\n\nPress R to Restart";

        Time.timeScale = 0f;
    }
}
