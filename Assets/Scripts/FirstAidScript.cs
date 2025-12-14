using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float healAmount = 50f;

    void OnTriggerEnter(Collider other)
    {
        // Check if player car entered the pickup
        HealthSystem health = other.GetComponent<HealthSystem>();

        if (health)
        {
            health.Heal(healAmount);
            Destroy(gameObject);
        }
    }
}
