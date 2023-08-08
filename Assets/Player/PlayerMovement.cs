using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] FixedJoystick fixedJoystick;
    [SerializeField] float speed;
    [SerializeField] float maxSpeed = 5f;
    
    private Vector3 addedPos;
    private float decreaseRate = 0.5f;

    float horizontal;
    float vertical;

    // Start is called before the first frame update
    void Start()
    {
        speed = maxSpeed;
    }

    void FixedUpdate()
    {
        horizontal = fixedJoystick.Horizontal;
        vertical = fixedJoystick.Vertical;

        addedPos = new Vector3(horizontal * speed * Time.fixedDeltaTime, 0, vertical * speed * Time.fixedDeltaTime);

        transform.position += addedPos; 
    }

    public void DecreaseSpeed()
    {
        speed = maxSpeed * decreaseRate;
    }

    public void IncreaseSpeed()
    {
        speed = maxSpeed;
    }
}
