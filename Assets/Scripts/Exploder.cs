using UnityEngine;

public class Exploder : Damager
{
    public GameObject fireType;
    public float explosionForce = 800f;
    public float explosionRadius = 5f;

    private bool isOnFire = false;

    void OnCollisionEnter(Collision other)
    {
        // React only to player's car
        AnimateCar car = other.gameObject.GetComponent<AnimateCar>();
        Rigidbody carRb = other.gameObject.GetComponent<Rigidbody>();

        if (car && !isOnFire)
        {
            isOnFire = true;

            // Spawn explosion FX
            if (fireType)
            {
                Instantiate(
                    fireType,
                    transform.position,
                    transform.rotation
                );
            }

            // Apply explosion force (lecture 6)
            if (carRb)
            {
                carRb.AddExplosionForce(
                    explosionForce,
                    transform.position,
                    explosionRadius,
                    1f,
                    ForceMode.Impulse
                );
            }

            // Destroy barrel after explosion
            Destroy(gameObject);
        }
    }
}
