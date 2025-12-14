using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Lane setup")]
    public Transform[] lanes;           // 6 lane slot objects

    [Header("Obstacle prefabs")]
    public GameObject[] obstaclePrefabs; // for now: only obstacles

    [Header("Row settings")]
    public int rowsToSpawn = 30;        // how many rows in total
    public float startZ = 80f;          // first row distance
    public float rowSpacing = 40f;      // distance between rows

    // Weighted number of obstacles per row (2..5, mostly 3-4)
    private int[] weightedAmounts = { 2, 3, 3, 3, 4, 4, 5 };

    void Start()
    {
        float z = startZ;

        for (int i = 0; i < rowsToSpawn; i++)
        {
            SpawnRow(z);
            z += rowSpacing;
        }
    }

    // Spawn one row of obstacles at given Z position
    void SpawnRow(float zPos)
    {
        int amount = ChooseAmountForRow();  // how many obstacles in this row

        // list of available lanes so we don't repeat the same lane
        List<int> freeLanes = new List<int>();
        for (int i = 0; i < lanes.Length; i++)
            freeLanes.Add(i);

        for (int i = 0; i < amount; i++)
        {
            if (freeLanes.Count == 0) break;

            // pick random lane index from free lanes
            int pickIndex = Random.Range(0, freeLanes.Count);
            int laneIndex = freeLanes[pickIndex];
            freeLanes.RemoveAt(pickIndex);

            Transform lane = lanes[laneIndex];

            float x = lane.position.x;
            float z = zPos;
            float y = GetGroundHeight(x, z);

            // pick random obstacle prefab from the list
            GameObject prefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];

            Vector3 spawnPos = new Vector3(x, y, z);
            Instantiate(prefab, spawnPos, Quaternion.identity, this.transform);
        }
    }

    // Weighted random for number of obstacles per row
    int ChooseAmountForRow()
    {
        int index = Random.Range(0, weightedAmounts.Length);
        return weightedAmounts[index];
    }

    // Raycast down to find the road height at given (x,z)
    float GetGroundHeight(float x, float z)
    {
        RaycastHit hit;
        Vector3 origin = new Vector3(x, 50f, z);

        if (Physics.Raycast(origin, Vector3.down, out hit, 200f))
        {
            return hit.point.y;
        }

        // fallback if nothing hit
        return 0f;
    }
}
