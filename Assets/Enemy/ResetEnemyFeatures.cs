using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResetEnemyFeatures : MonoBehaviour
{
    [SerializeField] GameObject childObject;
    Vector3 childObjectScale;
    int i = 0;

    void Start()
    {
        childObjectScale = childObject.transform.localScale;
    }

    public void ResetChildObject()
    {        
        if (i++ == 0 ) { return; }
        Debug.Log(childObjectScale);
        childObject.transform.localScale = childObjectScale;
    }
}
