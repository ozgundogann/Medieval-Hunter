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

    private float decreaseRate = 0.6f;
    private GameObject player;
    private PlayerMovement playerMovement;
    private Vector3 playerLocation;

    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
        player = GameObject.Find("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        speed = maxSpeed;        
    }

    void FixedUpdate()
    {
        EnemyMovement();
    }

    private void EnemyMovement()
    {
        playerLocation = player.transform.position;
        playerLocation.y = enemyPosY;
        transform.LookAt(playerTransform.position);
        transform.position = Vector3.MoveTowards(transform.position, playerLocation, speed * Time.fixedDeltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
            playerMovement.DecreaseSpeed();   
            speed *= decreaseRate;
            Invoke(nameof(StopEnemy), 1);
        }

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

    void OnCollisionExit(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
            playerMovement.IncreaseSpeed();
        }
    }


}
