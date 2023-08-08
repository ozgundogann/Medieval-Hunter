using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;

    [SerializeField]
    [Range(1, 10)] int maxEnemy;
    
    int enemySize = 1;

    float aboveInvisiblePoints = 30f;
    float belowInvisiblePoints = -15f;
    float leftInvisiblePoints = -30f;
    float rightInvisiblePoints = 30f;

    Transform playerTransform;
    GameObject[] pool;
    Vector3 spawnPoint;
    float y = 0.5f;

    void Start()
    {
        Invoke(nameof(SpawnEnemy), 2);
    }

    void SpawnEnemy() //In the future, this function should not be recursive!!!
    {
        if(enemySize > maxEnemy)
        {
            return;
        }

        playerTransform = GameObject.Find("Player").transform;

        switch(Random.Range(1,5))// Returns 1-4
        {
            case 1:
                spawnPoint = new Vector3(playerTransform.position.x, y, playerTransform.position.z + aboveInvisiblePoints);
                break;
            case 2:
                spawnPoint = new Vector3(playerTransform.position.x + leftInvisiblePoints, y, playerTransform.position.z);
                break;
            case 3:
                spawnPoint = new Vector3(playerTransform.position.x, y, playerTransform.position.z + belowInvisiblePoints);
                break;
            case 4:
                spawnPoint = new Vector3(playerTransform.position.x + rightInvisiblePoints, y, playerTransform.position.z);
                break;
            default:
                Debug.Log("Error");
                break;
        }

        Instantiate(enemyPrefab, spawnPoint, Quaternion.identity, transform);

        enemySize++;

        Invoke(nameof(SpawnEnemy), 2);
    }
}
