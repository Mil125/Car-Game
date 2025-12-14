using UnityEngine;
using UnityEngine.UI;

public class FuelBar : MonoBehaviour
{
    public FuelSystem fuelSystem;
    public Image fuelFill;

    void Update()
    {
        if (fuelSystem && fuelFill)
        {
            fuelFill.fillAmount = fuelSystem.GetFuelNormalized();
        }
    }
}
