using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionHandler : MonoBehaviour
{
    [SerializeField] Animator animatior;
    [SerializeField] AnimationClip clipEnemyDying;
    [SerializeField] AnimationClip clipDamage;
    [SerializeField] AnimationClip clipWeapon;
    [SerializeField] ParticleSystem enemyParticleSystem;

    private bool hasEntered = false;
    public bool HasEntered { get => hasEntered; set => hasEntered = value; }

    private Rigidbody rb;
    private ScoreBoard scoreBoard;
    private PlayerMovement playerMovementScript;
    private Enemy enemyScript;
    private Renderer enemyRenderer;

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
        enemyRenderer = GetComponentInChildren<Renderer>();
    }    

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player" && !HasEntered)
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

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Weapon" && playerMovementScript.IsAttacking && !hasEntered)
        {
            ProcessHit();
        }
    }

    void ProcessHit()
    {
        hasEntered = true;
        enemyScript.EnemyHealth--;
        if (enemyScript.EnemyHealth <= 0)
        {
            animatior.SetTrigger("Dying");
            float particleAnimTime = enemyParticleSystem.main.duration + enemyParticleSystem.startLifetime;
            Invoke(nameof(DisableEnemy), clipEnemyDying.length + particleAnimTime);
            Invoke(nameof(PlayParticles), clipEnemyDying.length);
            scoreBoard.AddPoints(enemyScript.EnemyPoint);
            playerMovementScript.IncreaseSpeed();
        }
        else
        {
            animatior.SetTrigger("Damage");
            //Invoke(nameof(WaitAnimationToEnd), clipDamage.length +1 );
        }
        Invoke(nameof(SetHasEnteredFalse), clipWeapon.length);
    }
    
    void PlayParticles()
    {
        enemyRenderer.enabled = false;
        enemyParticleSystem.Play();
    }

    void DisableEnemy()
    {        
        gameObject.SetActive(false);
    }

    //void WaitAnimationToEnd()
    //{
    //    switch (enemyScript.EnemyHealth)
    //    {
    //        case 1:
    //            break;
    //        case 2:
    //            break;
    //        case 3:
    //            break;
    //        case 4:
    //            Debug.Log("Enemy H: " + enemyScript.EnemyHealth);
    //            enemyRenderer.material.color = Color.yellow;
    //            break;
    //        default:
    //            break;
    //    }
    //}

    void SetHasEnteredFalse() { HasEntered = false; }

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
