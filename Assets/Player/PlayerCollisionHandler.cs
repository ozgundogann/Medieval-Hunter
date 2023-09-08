using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] Slider healthBar;
    [SerializeField] TMP_Text healthValue;
    [SerializeField] GameObject diedScreen;
    [SerializeField] int enemyDamagePoint = 10;
    [SerializeField] int enemyLargeDamagePoint = 20;

    GameObject playableUI;
    GameObject diedScreenHandler;
    PlayerMovement playerMovementScript;
    int reloadSceneTime = 3;

    void Start()
    {
        playableUI = GameObject.Find("PlayableUI");
        playerMovementScript = GetComponent<PlayerMovement>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Enemy")
        {
            PlayerGetsDamage(enemyDamagePoint);
        }

        else if(collision.transform.tag == "EnemyLarge")
        {
            PlayerGetsDamage(enemyLargeDamagePoint);
        }
    }

    void PlayerGetsDamage(int damageMultiplier)
    {
        healthBar.value -= Time.deltaTime * damageMultiplier;
        if(healthBar.value <= 0)
        {
            playableUI.SetActive(false);
            diedScreen.SetActive(true);
            playerMovementScript.IsAutoAttackContinue = false;
            Invoke(nameof(ReloadScene), reloadSceneTime);
        }
        healthValue.text = Mathf.RoundToInt(healthBar.value).ToString();
    }

    void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

}
