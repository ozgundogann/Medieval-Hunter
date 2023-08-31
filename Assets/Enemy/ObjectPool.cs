using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] [Range(0.1f, 10f)]float spawnTimer = 1f;
    [SerializeField] [Range(1, 50)] int maxEnemy;

    float aboveInvisiblePoints = 17f;
    float belowInvisiblePoints = -13f;
    float leftInvisiblePoints = -25f;
    float rightInvisiblePoints = 25f;//Invisible points should not be adjust manually.

    GameObject player;
    GameObject[] pool;
    Vector3 spawnPoint;
    float enemyHeightFromFloor = 0f;

    void Awake()
    {
        PopulatePool();
        player = GameObject.Find("Player");
        enemyHeightFromFloor = enemyPrefab.transform.localScale.y / 2 + 0.1f;
    }

    void Start()
    {
        StartCoroutine(SpawnEnemy());    
    }

    void PopulatePool()
    {
        pool = new GameObject[maxEnemy];

        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(enemyPrefab, transform);
            pool[i].SetActive(false);
        }
    }

    void RelocateEnemy()
    {
        switch(Random.Range(1,5))// Returns 1-4
        {
            case 1:
                spawnPoint = new Vector3(player.transform.position.x, enemyHeightFromFloor, player.transform.position.z + aboveInvisiblePoints);
                break;
            case 2:
                spawnPoint = new Vector3(player.transform.position.x + leftInvisiblePoints, enemyHeightFromFloor, player.transform.position.z);
                break;
            case 3:
                spawnPoint = new Vector3(player.transform.position.x, enemyHeightFromFloor, player.transform.position.z + belowInvisiblePoints);
                break;
            case 4:
                spawnPoint = new Vector3(player.transform.position.x + rightInvisiblePoints, enemyHeightFromFloor, player.transform.position.z);
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
                pool[i].transform.localScale = Vector3.one;
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
