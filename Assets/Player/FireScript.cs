using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FireScript : MonoBehaviour
{
    [SerializeField] GameObject fireButtonObject;
    [SerializeField] ParticleSystem _particleSystem;
    [SerializeField] Button field;

    void OnEnable()
    {
        field.onClick.AddListener(Fire);
        fireButtonObject.SetActive(true);
    }

    void OnDisable()
    {
        field.onClick.RemoveListener(Fire);
        if (fireButtonObject != null) { fireButtonObject.SetActive(false); }
    }   

    void Fire() 
    {
        if(!_particleSystem.isPlaying)
        {
            _particleSystem.Play();
        }
    }
}
