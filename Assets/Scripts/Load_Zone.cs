using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load_Zone : MonoBehaviour
{
    public GameObject[] mapTypePrefabs;
    private List<GameObject> activeMapTypes;

    private Transform playerTransform;
    private float spawnX = 0.0f;
    private float mapTypeLength = 50.0f;
    private int mapOnScreenMax = 3;
    private float deleteZone = 40f;
    private int lastPrefabIndex = 0;

    void Start()
    {
        activeMapTypes = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        for(int i = 0; i < mapOnScreenMax; i++)
        {
            if (i < 2)
                SpawnMapType(0);
            else
                SpawnMapType();
        }
    }

    void Update()
    {
        if(playerTransform.position.x - deleteZone > (spawnX - mapOnScreenMax * mapTypeLength))
        {
            SpawnMapType();
            DeleteMapType();
        }
    }

    private void SpawnMapType(int prefabeIndex = -1)
    {
        GameObject ground;
        if (prefabeIndex == -1)
            ground = Instantiate(mapTypePrefabs[RandomPrefabIndex()]) as GameObject;
        else
            ground = Instantiate(mapTypePrefabs[prefabeIndex]) as GameObject;
        ground.transform.SetParent(transform);
        ground.transform.position = Vector2.right * spawnX;
        spawnX += mapTypeLength;
        activeMapTypes.Add(ground);
    }

    private void DeleteMapType()
    {
        Destroy(activeMapTypes[0]);
        activeMapTypes.RemoveAt(0);
    }
    
    private int RandomPrefabIndex()
    {
        if (mapTypePrefabs.Length <= 1)
            return 0;

        int randomIndex = lastPrefabIndex;
        while(randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(0, mapTypePrefabs.Length);
        }

        lastPrefabIndex = randomIndex;
        return randomIndex;
    }

}
