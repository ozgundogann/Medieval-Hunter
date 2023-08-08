using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] [Range(0.1f, 10f)]float spawnTimer = 1f;
    [SerializeField] [Range(1, 10)] int maxEnemy;

    float aboveInvisiblePoints = 30f;
    float belowInvisiblePoints = -15f;
    float leftInvisiblePoints = -30f;
    float rightInvisiblePoints = 30f;

    GameObject player;
    GameObject[] pool;
    Vector3 spawnPoint;
    float y = 0.5f;

    void Awake()
    {
        PopulatePool();
        player = GameObject.Find("Player");
    }

    void Start()
    {
        StartCoroutine(SpawnEnemy());    
    }

    private void PopulatePool()
    {
        pool = new GameObject[maxEnemy];

        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(enemyPrefab, transform);
            pool[i].SetActive(false);
        }
    }

    void RelocateEnemy() //In the future, this function should not be recursive!!!
    {
        switch(Random.Range(1,5))// Returns 1-4
        {
            case 1:
                spawnPoint = new Vector3(player.transform.position.x, y, player.transform.position.z + aboveInvisiblePoints);
                break;
            case 2:
                spawnPoint = new Vector3(player.transform.position.x + leftInvisiblePoints, y, player.transform.position.z);
                break;
            case 3:
                spawnPoint = new Vector3(player.transform.position.x, y, player.transform.position.z + belowInvisiblePoints);
                break;
            case 4:
                spawnPoint = new Vector3(player.transform.position.x + rightInvisiblePoints, y, player.transform.position.z);
                break;
            default:
                Debug.Log("Error");
                break;
        }
        for (int i = 0; i < pool.Length; i++)
        {
            if (pool[i].activeInHierarchy == false)
            {
                pool[i].transform.position = spawnPoint;
                pool[i].SetActive(true);
                return;
            }
        }
    }

    IEnumerator SpawnEnemy()
    {
        while(true)
        {
            RelocateEnemy();
            yield return new WaitForSeconds(spawnTimer);
        }
    }
}
