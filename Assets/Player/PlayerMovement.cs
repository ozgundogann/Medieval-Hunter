using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] FixedJoystick fixedJoystick;
    [SerializeField] float speed;
    [SerializeField] float maxSpeed = 5f;
    [SerializeField] Animator animator;
    [SerializeField] float attackSpeed = 2f;

    private Vector3 addedPos;
    private Vector3 lookVector;
    private float decreaseRate = 0.5f;

    float horizontal;
    float vertical;

    void Start()
    {
        speed = maxSpeed;
        StartCoroutine(AttackFunc());
    }

    void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
    }

    private void MovePlayer()
    {
        horizontal = fixedJoystick.Horizontal;
        vertical = fixedJoystick.Vertical;

        addedPos = new Vector3(horizontal * speed * Time.fixedDeltaTime, 0, vertical * speed * Time.fixedDeltaTime);

        transform.position += addedPos;
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

    IEnumerator AttackFunc()
    {
        while (true)
        {
            Attack();
            yield return new WaitForSeconds(attackSpeed);
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
    }
}
