using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnManager : MonoBehaviour
{
    [Header("Spawn Locations")]
    [SerializeField] private List<Transform> spawnLocations = new List<Transform>();

    [Header("Spawnable Objects")]
    [SerializeField] private List<GameObject> spawnableObjects = new List<GameObject>();

    [Header("Spawn Settings")]
    [SerializeField] private float spawnInterval = 180f; // 3 minutes
    [SerializeField, Range(0f, 1f)] private float spawnChance = 0.3f; // 30%
    [SerializeField] private float spawnVariation = 0.5f;
    [SerializeField] private int spawnAmount = 1;    

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnItems();

            yield return new WaitForSeconds(spawnInterval);

           
        }
    }

    private void SpawnItems()
    {
        if (spawnableObjects.Count == 0)
        {
            Debug.LogWarning("No spawnable objects assigned!");
            return;
        }

        foreach (Transform location in spawnLocations)
        {
            if (location == null)
                continue;

            for(int i = 0; i< spawnAmount; i++) { 
            if (Random.value <= spawnChance)
            {
                SpawnObject(location);
            }
            }
        }
    }

    private void SpawnObject(Transform location)
    {
        // Random object
        int randomObject = Random.Range(0, spawnableObjects.Count);

        // Random position variation
        Vector2 randomOffset = Random.insideUnitCircle * spawnVariation;

        Vector3 spawnPosition = location.position + new Vector3(randomOffset.x,randomOffset.y,0f);

        Instantiate(
            spawnableObjects[randomObject],
            spawnPosition,
            Quaternion.identity
        );
    }
}