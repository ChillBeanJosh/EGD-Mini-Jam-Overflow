using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tableSpawner : MonoBehaviour
{
    [Header("Table Settings")]
    public List<GameObject> tablePrefabs;
    public Transform spawnPoint;
    public bool spawnRandom = true;

    [Header("Spawn Timing")]
    public float spawnTimeMin = 1f;
    public float spawnTimeMax = 10f;

    public float tableLifeMin = 7f;
    public float tableLifeMax = 15f;

    public float successSpawnMin = 3f;
    public float successSpawnMax = 5f;

    public float failSpawnMin = 5f;
    public float failSpawnMax = 10f;

    private bool canSpawn = true;
    private GameObject currentTable;
    private bool minigameActive = false;

    public void Start()
    {
        StartCoroutine(SpawnTimer());
    }

    IEnumerator SpawnTimer()
    {
        while (true)
        {
            if (canSpawn && (currentTable == null))
            {
                float waitTime = Random.Range(spawnTimeMin, spawnTimeMax);
                yield return new WaitForSeconds(waitTime);

                SpawnRandomTable();
            }
            else
            {
                yield return null;
            }
        }
    }

    void SpawnRandomTable()
    {
        GameObject selectedPrefab = GetRandomWeightedPrefab();

        if (selectedPrefab != null)
        {
            currentTable = Instantiate(selectedPrefab, spawnPoint.position, Quaternion.identity);

            tableData data = currentTable.GetComponent<tableData>();
            if (data != null)
            {
                data.ownerSpawner = this;
            }
        }

        float tableLifetime = Random.Range(tableLifeMin, tableLifeMax); 
        StartCoroutine(AutoDestroyTable(currentTable, tableLifetime));
    }

    private IEnumerator AutoDestroyTable(GameObject table, float lifetime)
    {
        yield return new WaitForSeconds(lifetime);

        if (table != null && !minigameActive)
        {
            OnTableCompleted(false, table);
            if (currentTable == table)
                currentTable = null; 
        }
    }

        GameObject GetRandomWeightedPrefab()
    {
        float totalWeight = 0f;

        //Add all weight Chance's of table prefabs in pool:
        foreach (var prefab in tablePrefabs)
        {
            tableData data = prefab.GetComponent<tableData>();
            if (data != null)
                totalWeight += data.ChanceWeight;
        }

        float randomValue = Random.Range(0f, totalWeight);
        float cumulativeWeight = 0f;

        //Iterate down the list, add the weight of each one until we reach randomValue:
        foreach (var prefab in tablePrefabs)
        {
            tableData data = prefab.GetComponent<tableData>();
            if (data != null)
            {
                cumulativeWeight += data.ChanceWeight;
                if (randomValue <= cumulativeWeight)
                    return prefab;
            }
        }

        return null; 
    }


    public void OnTableCompleted(bool success, GameObject table)
    {
        if (table != null)
        {
            Destroy(table);
        }

        //Minigame Completed:
        if (success)
        {
            spawnTimeMin = successSpawnMin;
            spawnTimeMax = successSpawnMax;
        }
        //Minigame Failed:
        else
        {
            spawnTimeMin = failSpawnMin;
            spawnTimeMax = failSpawnMax;
        }
    }


    public void StartMinigame()
    {
        minigameActive = true;
    }
    public void EndMinigame()
    {
        minigameActive = false;
    }
}
