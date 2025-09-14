using System.Collections.Generic;
using UnityEngine;

public class CollectSpawner : MonoBehaviour
{
    public GameObject collectiblePrefab;
    public int collectibleCount = 5; // berapa banyak spawn
    public Vector2 spawnAreaMin = new Vector2(-8, -4);
    public Vector2 spawnAreaMax = new Vector2(8, 4);

    public List<GameObject> allCollect = new();

    private void Start()
    {
        allCollect = new();
    }
    public void SpawnCollectibles()
    {
        for (int i = 0; i < collectibleCount; i++)
        {
            Vector2 spawnPos = new Vector2(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y)
            );

            GameObject obj = Instantiate(collectiblePrefab, spawnPos, Quaternion.identity);
            allCollect.Add(obj);
        }
    }

    public void ResetSpawner()
    {
        foreach (var obj in allCollect)
        {
            if (obj != null) Destroy(obj);
        }

        allCollect.Clear();
    }
}
