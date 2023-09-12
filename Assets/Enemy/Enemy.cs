using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{    
    [SerializeField] [Range(1, 10)] int enemyMaxHealth;
    [SerializeField] float playerHeightPosY = 0.5f;
    [SerializeField] public float decreaseRate = 0.5f;
    [SerializeField] float maxSpeed = 2.5f;
    [SerializeField] int enemyPoint = 5;

    private Transform playerTransform;
    private Vector3 playerLocation;
    private int enemyHealth;
    private float speed;

    public float MaxSpeed { get => maxSpeed; set => maxSpeed = value; }
    public int EnemyPoint { get => enemyPoint; set => enemyPoint = value; }
    public float Speed { get => speed; set => speed = value; }
    public int EnemyHealth { get => enemyHealth; set => enemyHealth = value; }

    void Start()
    {
        GetPlayerFeatures();
    }

    void OnEnable()
    {
        ResetEnemyFeatures();        
    }

    void FixedUpdate()
    {
        EnemyMovement();
    }
    
    void GetPlayerFeatures()
    {
        playerTransform = GameObject.Find("PlayerPlaceholder").transform;
    }      
    
    void ResetEnemyFeatures()
    {
        speed = MaxSpeed;//Speed of enemy
        EnemyHealth = enemyMaxHealth;
        GetComponent<EnemyCollisionHandler>().HasEntered = false;
        GetComponentInChildren<Renderer>().enabled = true;
    }
    
    void EnemyMovement()
    {
        playerLocation = playerTransform.position;
        playerLocation.y = playerHeightPosY;
        transform.LookAt(playerLocation);
        transform.position = Vector3.MoveTowards(transform.position, playerLocation, speed * Time.fixedDeltaTime);
    }
}
