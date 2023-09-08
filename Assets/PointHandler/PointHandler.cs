using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointHandler : MonoBehaviour
{
    [SerializeField] GameObject weaponHead;
    [SerializeField] int levelMultiplier = 20;
    [SerializeField] int messageTime = 2;

    PlayerMovement playerMovementScript;
    TMP_Text statusMessage;
    
    bool isEarned = false;

    bool isAllEarned = false;


    void Start()
    {
        playerMovementScript = GameObject.Find("PlayerPlaceholder").GetComponentInParent<PlayerMovement>();
        statusMessage = playerMovementScript.StatusMessage;
    }

    public void SetNewReward(int point)
    {
        if(isAllEarned) { return; }

        statusMessage.gameObject.SetActive(true);

        if(point >= levelMultiplier * 4 && !isEarned)
        {
            StartCoroutine(playerMovementScript.AutoAttackCaller());
            statusMessage.text = "Auto attack active!";
            isEarned = true;
            isAllEarned = true;
        }
        else if(point >= levelMultiplier * 3 )
        {
            weaponHead.transform.localScale = weaponHead.transform.localScale * 2;
            statusMessage.text = "A new big weapon!";
        }
        else if (point >= levelMultiplier * 2)
        {
            statusMessage.text = "Attack time decreased";
            playerMovementScript.NewAttackTime = 0.5f;
        }
        else if (point >= levelMultiplier)
        {
            statusMessage.text = "Attack time decreased";
            playerMovementScript.NewAttackTime = 1f;
        }

        Invoke(nameof(DisableMessage), messageTime);
    }

    void DisableMessage()
    {
        statusMessage.gameObject.SetActive(false);
    }
}
