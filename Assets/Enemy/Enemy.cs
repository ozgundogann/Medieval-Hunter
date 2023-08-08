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

    private float decreaseRate = 0.5f;
    private PlayerMovement playerMovement;
    private Vector3 playerLocation;

    void Start()
    {
        PlayerFeatures();
        speed = maxSpeed;//Speed of enemy
    }

    void FixedUpdate()
    {
        EnemyMovement();
    }
    
    void OnCollisionEnter(Collision collision)
    {
        DecreasePlayerSpeed(collision);
        ResetKinematics();// Resets kinematicks of player and enemy.
    }
    
    void OnCollisionExit(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
            playerMovement.IncreaseSpeed();
        }
    }

    private void PlayerFeatures()
    {
        playerTransform = GameObject.Find("Player").transform;
        playerMovement = playerTransform.GetComponent<PlayerMovement>();
    }    

    private void EnemyMovement()
    {
        playerLocation = playerTransform.position;
        playerLocation.y = enemyPosY;
        transform.LookAt(playerTransform.position);
        transform.position = Vector3.MoveTowards(transform.position, playerLocation, speed * Time.fixedDeltaTime);
    }    

    private void DecreasePlayerSpeed(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            playerMovement.DecreaseSpeed();
            speed *= decreaseRate;
            Invoke(nameof(StopEnemy), 1);
        }
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
