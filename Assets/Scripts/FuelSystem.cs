using UnityEngine;

public class FuelSystem : MonoBehaviour
{
    public AnimateCar car;

    public float maxFuel = 100f;
    public float currentFuel = 100f;

    public float fuelPerMeter = 0.10f;   // how much fuel per unit distance

    private Vector3 lastPosition;

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        // Calculate distance traveled since last frame
        float distance = Vector3.Distance(transform.position, lastPosition);

        if (distance > 0.001f)
        {
            currentFuel -= distance * fuelPerMeter;
        }

        currentFuel = Mathf.Clamp(currentFuel, 0f, maxFuel);

        lastPosition = transform.position;
    }

    public float GetFuelNormalized()
    {
        return currentFuel / maxFuel;
    }
}
