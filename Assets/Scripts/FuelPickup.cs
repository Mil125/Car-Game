using UnityEngine;

public class FuelPickup : MonoBehaviour
{
    public float fuelAmount = 50f;

    void OnTriggerEnter(Collider other)
    {
        var fuelSys = other.GetComponent<FuelSystem>();
        if (fuelSys != null)
        {
            fuelSys.currentFuel += fuelAmount;
            Destroy(gameObject);
        }
    }
}
