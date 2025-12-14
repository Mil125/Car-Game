using UnityEngine;
using System.Collections.Generic;

public class CurveRoadRowSpawner : MonoBehaviour
{
    [Header("Obstacle prefabs")]
    public GameObject[] obstaclePrefabs;

    [Header("Lane settings")]
    public float laneSpacing = 4.3f;
    public float offsetForward = 0f;

    // 4 most often, then 5, then 3, then 2 (rare)
    private int[] weightedAmounts = { 4, 4, 4, 4, 5, 5, 3, 3, 2 };

    void Start()
    {
        SpawnRandomRow();
    }

    void SpawnRandomRow()
    {
        int count = weightedAmounts[Random.Range(0, weightedAmounts.Length)];

        List<int> availableLanes = new List<int>() { 1, 2, 3, 4, 5, 6 };
        List<GameObject> availablePrefabs = new List<GameObject>(obstaclePrefabs);

        for (int i = 0; i < count && availableLanes.Count > 0 && availablePrefabs.Count > 0; i++)
        {
            int lanePick = Random.Range(0, availableLanes.Count);
            int laneIndex = availableLanes[lanePick];
            availableLanes.RemoveAt(lanePick);

            Vector3 basePos = transform.position + transform.forward * offsetForward;
            Vector3 laneOffset = transform.right * laneSpacing * laneIndex;
            Vector3 spawnPos = basePos + laneOffset;

            if (Physics.Raycast(spawnPos + Vector3.up * 50f, Vector3.down, out RaycastHit hit, 200f))
                spawnPos = hit.point;

            GameObject prefab = ChoosePrefabByProbability(availablePrefabs);
            availablePrefabs.Remove(prefab);

            Instantiate(prefab, spawnPos, transform.rotation);
        }
    }

    // =====================================================
    // 60% cars (0–6)
    // 30% barrels (7–8)
    // 5% medkit (9)
    // 5% fuel (10)
    // =====================================================
    GameObject ChoosePrefabByProbability(List<GameObject> available)
    {
        float roll = Random.value;

        // ⛽ Fuel — 5%
        if (roll < 0.05f && available.Contains(obstaclePrefabs[10]))
            return obstaclePrefabs[10];

        // ❤️ Medkit — 5%
        if (roll < 0.10f && available.Contains(obstaclePrefabs[9]))
            return obstaclePrefabs[9];

        // 🛢️ Barrels — 30%
        if (roll < 0.40f)
        {
            List<GameObject> barrels = new List<GameObject>();
            if (available.Contains(obstaclePrefabs[7])) barrels.Add(obstaclePrefabs[7]);
            if (available.Contains(obstaclePrefabs[8])) barrels.Add(obstaclePrefabs[8]);

            if (barrels.Count > 0)
                return barrels[Random.Range(0, barrels.Count)];
        }

        // 🚗 Cars — 60% (default)
        List<GameObject> cars = new List<GameObject>();
        for (int i = 0; i <= 6; i++)
            if (available.Contains(obstaclePrefabs[i]))
                cars.Add(obstaclePrefabs[i]);

        return cars[Random.Range(0, cars.Count)];
    }
}
