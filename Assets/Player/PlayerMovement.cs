using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] FixedJoystick fixedJoystick;
    [SerializeField] Button attackButton;
    [SerializeField] TMP_Text statusMessage;
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] AnimationClip clip;
    [SerializeField] Animator animator;
    [SerializeField] float speed;
    [SerializeField] float maxSpeed = 5f;
    [SerializeField] float decreaseRate = 0.55f;
    [SerializeField] float autoAttackCooldown = 0f;

    private TMP_Text buttonText;
    private Vector3 addedPos;
    private Vector3 lookVector;
    private float horizontal;
    private float vertical;
    private float attackCooldown = 0f;    
    private float newAttackTime = 2f;
    private bool isAttacking = false;
    private bool isDead = true;

    public TMP_Text StatusMessage { get => statusMessage; set => statusMessage = value; }
    public Animator Animator { get => animator; set => animator = value; }
    public float NewAttackTime { get => newAttackTime; set => newAttackTime = value; }
    public bool IsAttacking { get => isAttacking; }
    public bool IsAutoAttackContinue { get => isDead; set => isDead = value; }

    void Start()
    {
        speed = maxSpeed;
        attackButton.onClick.AddListener(Attack);
        buttonText = attackButton.GetComponentInChildren<TMP_Text>();
        statusMessage.gameObject.SetActive(false);
    }
    
    void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
        CooldownCounter();
        LookCameraAlways();
    }    

    void OnDisable()
    {
        attackButton.onClick.RemoveListener(Attack);
    }

    void Attack()
    {
        if(attackCooldown > 0) { return; }
        if(!Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            Animator.SetTrigger("Attack");
            isAttacking = true;
            Invoke(nameof(SetIsAttakingFalse), Animator.GetCurrentAnimatorStateInfo(0).length / 2);
            attackCooldown = newAttackTime;
        }
    }

    void SetIsAttakingFalse(){ isAttacking = false; }//To use invoke this function exist.

    void MovePlayer()
    {        
        horizontal = fixedJoystick.Horizontal;
        vertical = fixedJoystick.Vertical;

        if(horizontal != 0 && vertical != 0)
        {
            addedPos = new Vector3(horizontal * speed * Time.fixedDeltaTime, 0, vertical * speed * Time.fixedDeltaTime);
            transform.position += addedPos;
        }
    }

    void RotatePlayer()
    {
        lookVector = Vector3.forward * fixedJoystick.Vertical - Vector3.left * fixedJoystick.Horizontal;
        if (fixedJoystick.Horizontal != 0 || fixedJoystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(lookVector * Time.fixedDeltaTime);
        }
    }
    
    void CooldownCounter()
    {
        if(attackCooldown > 0)
        {
            buttonText.text = Mathf.FloorToInt(attackCooldown + 1).ToString();
            attackCooldown -= Time.fixedDeltaTime;
        }
        else
        {
            buttonText.SetText("Attack");
        }
    }

    void LookCameraAlways()
    {
        statusMessage.transform.LookAt(virtualCamera.transform);
        statusMessage.transform.Rotate(0f, 180f, 0f);
    }

    public void DecreaseSpeed()
    {
        speed = maxSpeed * decreaseRate;
    }

    public void IncreaseSpeed()
    {
        speed = maxSpeed;
    }

    public IEnumerator AutoAttackCaller()
    {
        attackButton.gameObject.SetActive(false);
        while(isDead)
        {
            AutoAttack();
            yield return new WaitForSeconds(autoAttackCooldown);
        }
    }

    void AutoAttack()
    {
        if (attackCooldown > 0) { return; }
        if (!Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            Animator.SetTrigger("Attack");
            isAttacking = true;
            Invoke(nameof(SetIsAttakingFalse), clip.length / 2);
            attackCooldown = newAttackTime;
        }
    }
}
