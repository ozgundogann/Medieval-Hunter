using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] FixedJoystick fixedJoystick;
    [SerializeField] Animator animator;
    [SerializeField] Button attackButton;
    [SerializeField] float speed;
    [SerializeField] float maxSpeed = 5f;

    private Vector3 addedPos;
    private Vector3 lookVector;
    private float decreaseRate = 0.5f;
    private float horizontal;
    private float vertical;

    void Start()
    {
        speed = maxSpeed;
        attackButton.onClick.AddListener(Attack);
        //StartCoroutine(AttackFunc());
    }
    
    void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
    }

    void Attack()
    {
        Debug.Log("Attacking!");
    }

    private void MovePlayer()
    {        
        horizontal = fixedJoystick.Horizontal;
        vertical = fixedJoystick.Vertical;

        if(horizontal != 0 && vertical != 0)
        {
            addedPos = new Vector3(horizontal * speed * Time.fixedDeltaTime, 0, vertical * speed * Time.fixedDeltaTime);
            transform.position += addedPos;
        }
    }

    private void RotatePlayer()
    {
        lookVector = Vector3.forward * fixedJoystick.Vertical - Vector3.left * fixedJoystick.Horizontal;
        if (fixedJoystick.Horizontal != 0 || fixedJoystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(lookVector * Time.fixedDeltaTime);
        }
    }

    public void DecreaseSpeed()
    {
        speed = maxSpeed * decreaseRate;
    }

    public void IncreaseSpeed()
    {
        speed = maxSpeed;
    }
    
    //IEnumerator AttackFunc()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(attackSpeed);
    //    }
    //}
}
