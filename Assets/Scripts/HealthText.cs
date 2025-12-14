using UnityEngine;
using TMPro;

public class HealthText : MonoBehaviour
{
    public HealthSystem healthSystem;
    public TextMeshProUGUI healthText;

    void Update()
    {
        if (!healthSystem || !healthText)
            return;

        float hp = healthSystem.currentHealth;

        // Update text
        healthText.text = "+HP: " + hp.ToString("0");

        // ===============================
        //        COLOR LOGIC
        // ===============================

        if (hp > 50f)
        {
            // 100 - 51
            healthText.color = Color.green;
        }
        else if (hp > 25f)
        {
            // 50 - 26
            healthText.color = Color.yellow;
        }
        else if (hp > 0f)
        {
            // 25 - 1
            healthText.color = Color.red;
        }
    }
}
