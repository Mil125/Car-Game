using UnityEngine;

public class FuelRotate : MonoBehaviour
{
    public float rotationSpeed = 60f; // degrees per second

    void Update()
    {
        // Rotate around Y axis
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
}
