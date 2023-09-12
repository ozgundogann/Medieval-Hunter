﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionHandler : MonoBehaviour
{
    [SerializeField] Animator animatior;
    [SerializeField] AnimationClip clipEnemyDying;
    [SerializeField] AnimationClip clipWeapon;
    
    private bool hasEntered = false;
    public bool HasEntered { get => hasEntered; set => hasEntered = value; }

    private Rigidbody rb;
    private ScoreBoard scoreBoard;
    private PlayerMovement playerMovementScript;
    private Enemy enemyScript;
    private Material enemyMaterial;

    void Start()
    {
        GetComponents();
    }

    void GetComponents()
    {
        playerMovementScript = GameObject.Find("PlayerPlaceholder").GetComponent<PlayerMovement>();
        scoreBoard = GameObject.Find("Score").GetComponent<ScoreBoard>();
        enemyScript = GetComponent<Enemy>();
        rb = GetComponent<Rigidbody>();
        enemyMaterial = GetComponentInChildren<Renderer>().material;
    }    

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player" && !HasEntered)
        {
            DecreasePlayerSpeed();// Bunlari PlayerCollisionHandler scriptinde yaz.
        }

        ResetKinematics();// Resets enemy kinematicks.
    }

    void ResetKinematics()
    {
        rb.isKinematic = true;
        rb.isKinematic = false; //This two line resets physics that is applied to this enemy from other enemy or player when collide.
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    void ProcessHit()
    {
        hasEntered = true;
        enemyScript.EnemyHealth--;
        if (enemyScript.EnemyHealth <= 0)
        {
            animatior.SetTrigger("Dying");
            Invoke(nameof(DisableEnemy), clipEnemyDying.length / 2);
        }
        else
        {
            animatior.SetTrigger("Damage");

            switch (enemyScript.EnemyHealth)
            {
                case 1:
                    enemyMaterial.color = new Color(1f, 0.90f, 0.84f);
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                default:
                    break;
            }
        }
        Invoke(nameof(SetHasEnteredFalse), clipWeapon.length);
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
            ProcessHit();
        }
    }

    void SetHasEnteredFalse() { HasEntered = false; }

    void DisableEnemy()
    {
        scoreBoard.AddPoints(enemyScript.EnemyPoint);
        gameObject.SetActive(false);
    }

    void DecreasePlayerSpeed()
    {
        playerMovementScript.DecreaseSpeed();
        enemyScript.Speed *= enemyScript.decreaseRate;
        Invoke(nameof(StopEnemy), 1);
    }

    void StopEnemy()
    {
        enemyScript.Speed = 0f;
        Invoke(nameof(MoveEnemy), 1);
    }

    void MoveEnemy()
    {
        enemyScript.Speed = enemyScript.MaxSpeed;
    }
}