using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public Button player;
    public float playerMaxHealth = 75;
    public float playerCurrentHealth;
    public float playerCurrentBlock;
    public Image playerBlockIcon;
    public HealthUIManager healthUi;
    public HealthUIManager menuHealthUi;

    public delegate void OnPlayerDamage(GameObject unit, GameObject source);
    public static event OnPlayerDamage PlayerHealthDamage;

    public static GameObject playerEffectList;
    public GameObject DeathCanvas;

    public PoisonDebuff poisonDebuff;
    public GameObject poisonHealth;
    public GameObject menuPoisonHealth;
    public GameObject poisonFill;
    public GameObject fill;
    public GameObject menuFill;
    public GameObject menuPoisonFill;

    public static float damageMultiplier;

    public AudioSource audioSource;
    public AudioClip sheildHit;
    public AudioClip healthHit;
    public AudioClip sheildBreak;

    public static bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponentInChildren<AudioSource>();
        playerEffectList = GameObject.Find("PlayerEffectList");
        playerCurrentHealth = playerMaxHealth;
        playerCurrentBlock = 0;
        damageMultiplier = 1;
        poisonFill.transform.position = fill.transform.position;
        poisonHealth.SetActive(false);
        menuPoisonHealth.SetActive(false);
        poisonDebuff = GetComponentInChildren<PoisonDebuff>();
    }
    private void OnEnable()
    {
        CombatStartAndEnd.CombatStart += FirstTurnStart;
    }

    private void OnDisable()
    {
        CombatStartAndEnd.CombatStart -= FirstTurnStart;
    }

    public void FirstTurnStart()
    {
        playerCurrentBlock = 0;
        damageMultiplier = 1;
    }

    // Update is called once per frame
    public void PlayerDamaged(int damage, GameObject source)
    {
        float excessDamage = damage - playerCurrentBlock;
        if (excessDamage > 0)
        {
            if (playerCurrentBlock == 0)
            {
                //play health hit sfx
            }
            else
            {
                //play block broken hit sfx
            }
            playerCurrentBlock = 0;
            playerCurrentHealth -= excessDamage;
            float hpPercentage = playerCurrentHealth / playerMaxHealth;
            healthUi.UpdateHealth(hpPercentage, playerCurrentHealth, playerMaxHealth);
            healthUi.UpdateBlock(playerCurrentBlock);
            menuHealthUi.UpdateHealth(hpPercentage, playerCurrentHealth, playerMaxHealth);
            menuHealthUi.UpdateBlock(playerCurrentBlock);
            if (PlayerHealthDamage != null)
            {
                PlayerHealthDamage(player.gameObject, source);
            }

        }
        else
        {
            //play block hit sfx
            playerCurrentBlock -= damage;
            healthUi.UpdateBlock(playerCurrentBlock);
            menuHealthUi.UpdateBlock(playerCurrentBlock);
        }

        if (playerCurrentHealth <= 0)
        {
            gameOver = true;
            DeathCanvas.SetActive(true);
        }

    }

    public void PlayerBlocks(float Block)
    {
        playerCurrentBlock += Block;
        healthUi.UpdateBlock(playerCurrentBlock);
        menuHealthUi.UpdateBlock(playerCurrentBlock);
    }

    public void SheildDecay()
    {
        playerCurrentBlock = 0;
        healthUi.UpdateBlock(playerCurrentBlock);
        menuHealthUi.UpdateBlock(playerCurrentBlock);
    }

    public void Update()
    {
        if (poisonDebuff.enabled == true)
        {
            poisonHealth.SetActive(true);
            menuPoisonHealth.SetActive(true);
            poisonFill.transform.position = fill.transform.position;
            poisonFill.GetComponent<RectTransform>().anchorMax = new Vector2(fill.GetComponent<RectTransform>().anchorMax.x, 1f);
            menuPoisonFill.transform.position = menuFill.transform.position;
            menuPoisonFill.GetComponent<RectTransform>().anchorMax = new Vector2(menuFill.GetComponent<RectTransform>().anchorMax.x, 1f);
            int poisonStacks = poisonDebuff.stacks;
            float poisonPercentage = poisonStacks / playerCurrentHealth;
            //float poisonPercentage = poisonStacks / playerMaxHealth;
            //float hpPercentage = playerCurrentHealth / playerMaxHealth;
            if (poisonPercentage > 1)
            {
                poisonPercentage = 1;
            }
            healthUi.UpdatePoison(poisonPercentage);
            menuHealthUi.UpdatePoison(poisonPercentage);
        }
        else
        {
            poisonHealth.SetActive(false);
            menuPoisonHealth.SetActive(false);
        }
    }
}

