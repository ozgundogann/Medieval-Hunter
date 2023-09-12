using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] Slider healthBar;
    [SerializeField] TMP_Text healthValueText;
    [SerializeField] GameObject diedScreen;
    [SerializeField] int enemyDamagePoint = 5;
    [SerializeField] int enemyLargeDamagePoint = 10;
    [SerializeField] int healthPoint = 20;

    GameObject playableUI;
    GameObject diedScreenHandler;
    PlayerMovement playerMovementScript;
    Collider healthCollider;
    int reloadSceneTime = 3;

    void Start()
    {
        playableUI = GameObject.Find("PlayableUI");
        playerMovementScript = GetComponent<PlayerMovement>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            PlayerGetsDamage(enemyDamagePoint);
        }
        else if (collision.transform.tag == "EnemyLarge")
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
        ShowCurrentHealth();
    }

    void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Health")
        {
            HealthHandler(other);            
        }
    }

    void HealthHandler(Collider other)
    {
        if (healthBar.value >= 100) { return; }

        if (healthBar.value + healthPoint > 100)
        {
            healthBar.value = 100;
        }
        else
        {
            healthBar.value += healthPoint;
        }

        other.gameObject.SetActive(false);
        healthCollider = other;
        ShowCurrentHealth();
    }

    void ShowCurrentHealth()
    {
        healthValueText.text = Mathf.RoundToInt(healthBar.value).ToString();
    }
}
