using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointHandler : MonoBehaviour
{
    [SerializeField] GameObject weaponHead;
    [SerializeField] TMP_Text statusMessage;
    [SerializeField] int levelMultiplier = 20;
    [SerializeField] int messageTime = 2;
    
    FireScript fireScript;
    PlayerMovement playerMovementScript;
        
    bool isEarned = false;
    bool isAllEarned = false;

    void Start()
    {
        playerMovementScript = GameObject.Find("PlayerPlaceholder").GetComponent<PlayerMovement>();
        fireScript = playerMovementScript.GetComponentInChildren<FireScript>();
        fireScript.enabled = false;
    }

    public void SetNewReward(int point)
    {
        if(isAllEarned) { return; }

        statusMessage.gameObject.SetActive(true);
        
        if(point >= levelMultiplier * 5 && !isEarned)
        {
            statusMessage.text = "Firing gained tap to screen!!";
            isEarned = true;
            isAllEarned = true;
            fireScript.enabled = true;
        }
        else if (point >= levelMultiplier * 4)
        {
            statusMessage.text = "Auto attack!";
            StartCoroutine(playerMovementScript.AutoAttackCaller());
        }
        else if(point >= levelMultiplier * 3 )
        {
            weaponHead.transform.localScale = weaponHead.transform.localScale * 2;
            statusMessage.text = "A new big weapon!";
        }
        else if (point >= levelMultiplier * 2)
        {
            statusMessage.text = "Attack time decreased (0.8 sec.)";
            playerMovementScript.NewAttackTime = 0.8f;
        }
        else if (point >= levelMultiplier)
        {
            statusMessage.text = "Attack time decreased (1 sec.)";
            playerMovementScript.NewAttackTime = 1f;
        }

        Invoke(nameof(DisableMessage), messageTime);
    }

    public void returnPlaygroundMessage()
    {
        if(!statusMessage.gameObject.activeSelf)
        {
            Debug.Log("Active");
            statusMessage.gameObject.SetActive(true);
        }

        statusMessage.text = "Return to Playground!!!";

        Invoke(nameof(DisableMessage), messageTime);
    }

    void DisableMessage()
    {
        statusMessage.gameObject.SetActive(false);
    }
}
