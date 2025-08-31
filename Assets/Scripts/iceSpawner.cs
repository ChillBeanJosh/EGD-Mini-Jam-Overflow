using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iceSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public List<GameObject> ObstructionObjects; 
    public Transform corner1;                 
    public Transform corner2;          
    
    public float spawnInterval = 5f;          
    public float spawnY = 0.7f;                 
    public float destroyTime = 10f;

    private void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            SpawnRandomObject();
        }
    }

    private void SpawnRandomObject()
    {
        if (ObstructionObjects.Count == 0)
        {
            Debug.Log("NOTHING IN LIST!!");
            return;
        }

        GameObject prefab = ObstructionObjects[Random.Range(0, ObstructionObjects.Count)];

        //Spawn Range:
        float minX = Mathf.Min(corner1.position.x, corner2.position.x);
        float maxX = Mathf.Max(corner1.position.x, corner2.position.x);
        float minZ = Mathf.Min(corner1.position.z, corner2.position.z);
        float maxZ = Mathf.Max(corner1.position.z, corner2.position.z);


        Vector3 spawnPos = new Vector3
        (
            Random.Range(minX, maxX),
            spawnY,
            Random.Range(minZ, maxZ)
        );

        GameObject spawnedObj = Instantiate(prefab, spawnPos, Quaternion.identity);
        Debug.Log("ICE SPAWNED!!");
        Destroy(spawnedObj, destroyTime);
    }
}
