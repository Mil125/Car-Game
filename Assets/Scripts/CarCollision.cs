using UnityEngine;

public class CarCollision : MonoBehaviour
{
    private HealthSystem healthSystem;

    void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if collided object has Damager
        Damager damager = collision.gameObject.GetComponent<Damager>();

        if (damager && healthSystem)
        {
            healthSystem.TakeDamage(damager.damageAmount);
        }
    }
}
