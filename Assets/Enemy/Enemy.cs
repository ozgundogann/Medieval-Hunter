using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] float maxSpeed = 1.5f;
    [SerializeField] float enemyPosY = 0.5f;
    [SerializeField] int enemyHealth = 1;

    private float decreaseRate = 0.5f;
    private PlayerMovement playerMovementScript;
    private Vector3 playerLocation;

    void Start()
    {
        GetPlayerFeatures();
        speed = maxSpeed;//Speed of enemy
    }
    
    private void GetPlayerFeatures()
    {
        playerTransform = GameObject.Find("PlayerPlaceholder").transform;
        playerMovementScript = playerTransform.GetComponent<PlayerMovement>();
    }  

    void FixedUpdate()
    {
        EnemyMovement();
    }
    
    void EnemyMovement()
    {
        playerLocation = playerTransform.position;
        playerLocation.y = enemyPosY;
        transform.LookAt(playerTransform.position);
        transform.position = Vector3.MoveTowards(transform.position, playerLocation, speed * Time.fixedDeltaTime);
    }    
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            DecreasePlayerSpeed(collision);
        }
        

        ResetKinematics();// Resets enemy kinematicks.
    }
    
    void OnCollisionExit(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
            playerMovementScript.IncreaseSpeed();
        }
    }          

    private void DecreasePlayerSpeed(Collision collision)
    {        
        playerMovementScript.DecreaseSpeed();
        speed *= decreaseRate;
        Invoke(nameof(StopEnemy), 1);        
    }

    private void ResetKinematics()
    {
        rb.isKinematic = true;
        rb.isKinematic = false; //This two line resets physic that applied from an enemy when collide with an other enemy.
    }

    void StopEnemy()
    {
        speed = 0f;
        Invoke(nameof(MoveEnemy), 1);
    }

    void MoveEnemy()
    {
        speed = maxSpeed;
    }
}
