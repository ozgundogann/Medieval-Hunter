using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHandler : MonoBehaviour
{
    [SerializeField] int healthCooldown;

    void OnDisable()
    {
        Invoke(nameof(EnableObject), healthCooldown);    
    }

    void EnableObject()
    {
        transform.gameObject.SetActive(true);
    }
}
