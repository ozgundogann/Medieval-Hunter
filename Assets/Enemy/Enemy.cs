using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] Rigidbody rb;
    [SerializeField] Animator animatior;
    [SerializeField] int enemyMaxHealth = 1;
    [SerializeField] int enemyPoint = 5;
    [SerializeField] float maxSpeed = 2.5f;
    [SerializeField] AnimationClip clip;

    private int enemyHealth;
    private float speed;
    private float playerHeightPosY = 0.5f;
    private float decreaseRate = 0.5f;
    private PlayerMovement playerMovementScript;
    private ScoreBoard scoreBoard;
    private Vector3 playerLocation;
    private bool hasEntered = false;

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
        Debug.Log(hasEntered);
    }
    
    void GetPlayerFeatures()
    {
        scoreBoard = GameObject.Find("Score").GetComponent<ScoreBoard>();
        playerTransform = GameObject.Find("PlayerPlaceholder").transform;
        playerMovementScript = playerTransform.GetComponent<PlayerMovement>();
    }      
    
    void ResetEnemyFeatures()
    {
        speed = maxSpeed;//Speed of enemy
        enemyHealth = enemyMaxHealth;
        hasEntered = false;
    }
    
    void EnemyMovement()
    {
        playerLocation = playerTransform.position;
        playerLocation.y = playerHeightPosY;
        transform.LookAt(playerLocation);
        transform.position = Vector3.MoveTowards(transform.position, playerLocation, speed * Time.fixedDeltaTime);
    }    
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player" && !hasEntered)
        {
            DecreasePlayerSpeed();
        }        

        ResetKinematics();// Resets enemy kinematicks.
    }

    void ResetKinematics()
    {
        rb.isKinematic = true;
        rb.isKinematic = false; //This two line resets physics that is applied to this enemy from other enemy or player when collide.
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
           playerMovementScript.IncreaseSpeed();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Weapon" && playerMovementScript.IsAttacking && !hasEntered)
        {
            hasEntered = true;
            enemyHealth--;
            if (enemyHealth <= 0)
            {
                KillEnemy();
            }
            else
            {
                Debug.Log("Damaged!!");
                animatior.SetTrigger("Damage");
            }
            Invoke(nameof(SetHasEnteredFalse), clip.length / 2);
        }
    }

    void SetHasEnteredFalse(){ hasEntered = false; }

    void DecreasePlayerSpeed()
    {        
        playerMovementScript.DecreaseSpeed();
        speed *= decreaseRate;
        Invoke(nameof(StopEnemy), 1);        
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

    public void KillEnemy()
    {
        animatior.SetTrigger("Dying");
        Invoke(nameof(DisableEnemy), clip.length);
    }

    void DisableEnemy()
    {
        scoreBoard.AddPoints(enemyPoint);
        gameObject.SetActive(false);
    }
}
