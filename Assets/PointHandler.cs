using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PointHandler : MonoBehaviour
{
    [SerializeField] GameObject weaponHead;
    [SerializeField] int levelMultiplier = 20;

    PlayerMovement playerMovementScript;
    bool isEarned = false;

    bool isAllEarned = false;


    void Start()
    {
        playerMovementScript = GameObject.Find("PlayerPlaceholder").GetComponentInParent<PlayerMovement>();
    }

    public void SetNewReward(int point)
    {
        if(isAllEarned) { return; }

        if(point >= levelMultiplier * 4 && !isEarned)
        {
            StartCoroutine(playerMovementScript.AutoAttackCaller());
            isEarned = true;
            isAllEarned = true;
        }
        else if(point >= levelMultiplier * 3 )
        {
            weaponHead.transform.localScale = weaponHead.transform.localScale * 2;
        }
        else if (point >= levelMultiplier * 2)
        {
            Debug.Log("NewAttackTime is 0,5 sec");
            playerMovementScript.NewAttackTime = 0.5f;
        }
        else if (point >= levelMultiplier)
        {
            Debug.Log("NewAttackTime is 1 sec");
            playerMovementScript.NewAttackTime = 1f;
        }        
        else
        {
            return;
        }
    }
}
