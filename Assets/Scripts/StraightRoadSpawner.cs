using System.Collections.Generic;
using UnityEngine;

public class StraightRoadSpawner : MonoBehaviour
{
    [Header("Lane setup")]
    public Transform[] lanes; // 6 lanes

    [Header("Obstacle prefabs")]
    // 0–6 cars | 7–8 barrels | 9 medkit | 10 fuel
    public GameObject[] obstaclePrefabs;

    [Header("Row settings")]
    public int rowsToSpawn = 30;
    public float startZ = 80f;
    public float rowSpacing = 40f;

    // 4 most often, then 5, then 3, then 2
    private int[] weightedAmounts = { 4, 4, 4, 4, 5, 5, 3, 3, 2 };

    void Start()
    {
        float z = startZ;

        for (int i = 0; i < rowsToSpawn; i++)
        {
            SpawnRow(z);
            z += rowSpacing;
        }
    }

    void SpawnRow(float zPos)
    {
        int amount = weightedAmounts[Random.Range(0, weightedAmounts.Length)];

        // free lanes (no duplicates)
        List<int> freeLanes = new List<int>();
        for (int i = 0; i < lanes.Length; i++)
            freeLanes.Add(i);

        // available prefabs (no duplicates per row)
        List<GameObject> available = new List<GameObject>(obstaclePrefabs);

        for (int i = 0; i < amount && freeLanes.Count > 0; i++)
        {
            // pick lane
            int lanePick = Random.Range(0, freeLanes.Count);
            int laneIndex = freeLanes[lanePick];
            freeLanes.RemoveAt(lanePick);

            Transform lane = lanes[laneIndex];

            float x = lane.position.x;
            float y = GetGroundHeight(x, zPos);

            GameObject prefab = ChoosePrefab(available);
            available.Remove(prefab);

            Instantiate(
                prefab,
                new Vector3(x, y, zPos),
                transform.rotation,
                transform
            );
        }
    }

    GameObject ChoosePrefab(List<GameObject> available)
    {
        float roll = Random.value;

        // ⛽ Fuel — 5%
        if (roll < 0.05f && HasIndex(10) && available.Contains(obstaclePrefabs[10]))
            return obstaclePrefabs[10];

        // ❤️ Medkit — 5%
        if (roll < 0.10f && HasIndex(9) && available.Contains(obstaclePrefabs[9]))
            return obstaclePrefabs[9];

        // 🛢️ Barrels — 30%
        if (roll < 0.40f)
        {
            List<GameObject> barrels = new List<GameObject>();

            if (HasIndex(7) && available.Contains(obstaclePrefabs[7]))
                barrels.Add(obstaclePrefabs[7]);
            if (HasIndex(8) && available.Contains(obstaclePrefabs[8]))
                barrels.Add(obstaclePrefabs[8]);

            if (barrels.Count > 0)
                return barrels[Random.Range(0, barrels.Count)];
        }

        // 🚗 Cars — 60% (default)
        List<GameObject> cars = new List<GameObject>();

        for (int i = 0; i <= 6; i++)
        {
            if (HasIndex(i) && available.Contains(obstaclePrefabs[i]))
                cars.Add(obstaclePrefabs[i]);
        }

        // guaranteed fallback
        return cars[Random.Range(0, cars.Count)];
    }

    bool HasIndex(int index)
    {
        return obstaclePrefabs != null &&
               index >= 0 &&
               index < obstaclePrefabs.Length;
    }

    float GetGroundHeight(float x, float z)
    {
        if (Physics.Raycast(new Vector3(x, 50f, z), Vector3.down, out RaycastHit hit, 200f))
            return hit.point.y;

        return 0f;
    }
}
