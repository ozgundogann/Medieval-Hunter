using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigator : MonoBehaviour
{
    [SerializeField] GameObject[] healthPacks;
    [SerializeField] Transform player;
    [SerializeField] float speed;

    void Start()
    {
        healthPacks = GameObject.FindGameObjectsWithTag("Health");
    }

    void FixedUpdate()
    {
        transform.position = new Vector3(player.position.x, 3.8f, player.position.z);
        Navigate(FindCloser());
    }

    Transform FindCloser()
    {
        Transform minTransform = healthPacks[0].transform;

        float minDist = Vector3.Distance(healthPacks[0].transform.position, player.position);
        for (int i = 1; i < healthPacks.Length; i++)
        {
            if (Vector3.Distance(healthPacks[i].transform.position, player.position) < minDist)
            {
                minDist = Vector3.Distance(healthPacks[i].transform.position, player.position);
                minTransform = healthPacks[i].transform;
            }
        }

        return minTransform;
    }

    void Navigate(Transform target)
    {
        // Determine which direction to rotate towards
        Vector3 targetDirection = player.position - target.position;

        // The step size is equal to speed times frame time.
        float singleStep = speed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
