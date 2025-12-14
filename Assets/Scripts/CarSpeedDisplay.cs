using UnityEngine;
using TMPro;

public class CarSpeedDisplay : MonoBehaviour
{
    public AnimateCar car;                 // Reference to AnimateCar
    public TextMeshProUGUI speedText;      // TMP text

    public float maxSpeedKMH = 120f;       // Max speed shown on UI

    void Update()
    {
        if (car && speedText)
        {
            // Normalize current speed (0..1)
            float normalizedSpeed = Mathf.Abs(car.currentSpeed) / car.maxSpeed;

            // Scale to km/h for display
            float speedKMH = normalizedSpeed * maxSpeedKMH;

            speedText.text = "Speed: " + speedKMH.ToString("0") + " km/h";
        }
    }
}
