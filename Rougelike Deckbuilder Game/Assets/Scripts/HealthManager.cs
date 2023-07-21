using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static TurnSystem;

public class HealthManager : MonoBehaviour
{
    public GameObject unit;
    public float maxHealth = 75;
    public float currentHealth;
    public float currentBlock;
    public HealthUIManager healthUi;
    public delegate void OnEnemyDamage(GameObject unit, GameObject source);
    public static event OnEnemyDamage EnemyDamaged;
    public delegate void OnEnemyKill();
    public static event OnEnemyKill EnemyKilled;
    public bool enemyAlive;

    public PoisonDebuff poisonDebuff;
    public GameObject poisonHealth;
    public GameObject poisonFill;
    public GameObject fill;

    public float damageModifier;

    public AudioSource audioSource;
    public AudioClip shieldHit;
    public AudioClip healthHit;
    public AudioClip shieldBreak;
    public AudioClip poisonDamage;
    public AudioClip deathSound;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponentInChildren<AudioSource>();
        currentHealth = maxHealth;
        currentBlock = 0;
        unit = gameObject;
        damageModifier = 1;
        enemyAlive = true;
        poisonDebuff = GetComponentInChildren<PoisonDebuff>();
        poisonHealth.SetActive(false);
        StartCoroutine(UpdateHealth());
    }
    private void OnEnable()
    {
        TurnSystem.TurnEnd += SheildDecay;
    }

    private void OnDisable()
    {
        TurnSystem.TurnEnd -= SheildDecay;
    }

    IEnumerator UpdateHealth()
    {
        yield return new WaitForSeconds(0.1f);
        healthUi.UpdateHealth(1, currentHealth, maxHealth);
    }
    // Update is called once per frame
    public void UnitDamaged(int damage, bool poisonDmg)
    {
        float excessDamage = damage - currentBlock;
        if (excessDamage > 0)
        {
            if (currentBlock == 0 && poisonDmg == false)
            {
                audioSource.clip = healthHit;
                audioSource.Play();
            }
            else if (poisonDmg == true)
            {
                audioSource.clip = poisonDamage;
                audioSource.Play();
            }
            else
            {
                audioSource.clip = shieldBreak;
                audioSource.Play();
            }
            currentBlock = 0;
            currentHealth -= excessDamage;
            float hpPercentage = currentHealth / maxHealth;
            healthUi.UpdateHealth(hpPercentage, currentHealth, maxHealth);
            healthUi.UpdateBlock(currentBlock);
            if (enemyAlive == true)
            {
                if (EnemyDamaged != null)
                {
                    EnemyDamaged(unit, null);
                }
            }
            if (currentHealth <= 0 && enemyAlive == true)
            {
                StartCoroutine(DeathSounds());
            }
        }
        else
        {
            currentBlock -= damage;
            if (poisonDmg == true)
            {
                audioSource.clip = poisonDamage;
                audioSource.Play();
            }
            else
            {
                audioSource.clip = shieldHit;
                audioSource.Play();
            }
            healthUi.UpdateBlock(currentBlock);
        }

    }

    public void BlockGained(float Block)
    {
        currentBlock += Block;
        healthUi.UpdateBlock(currentBlock);
    }

    public void SheildDecay()
    {
        currentBlock = 0;
        healthUi.UpdateBlock(currentBlock);
    }

    public void Update()
    {
        if (poisonDebuff.enabled == true)
        {
            poisonHealth.SetActive(true);
            poisonFill.transform.position = fill.transform.position;
            poisonFill.GetComponent<RectTransform>().anchorMax = new Vector2(fill.GetComponent<RectTransform>().anchorMax.x, 1f);
            int poisonStacks = poisonDebuff.stacks;
            float poisonPercentage = poisonStacks / currentHealth;
            //float poisonPercentage = poisonStacks / maxHealth;
            //float hpPercentage = currentHealth / maxHealth;
            if (poisonPercentage > 1)
            {
                poisonPercentage = 1;
            }
            healthUi.UpdatePoison(poisonPercentage);
        }
        else
        {
            poisonHealth.SetActive(false);
        }
    }
    IEnumerator DeathSounds()
    {
        yield return new WaitForSeconds(0.3f);
        audioSource.clip = deathSound;
        audioSource.Play();
        yield return new WaitForSeconds(0.5f);
        unit.SetActive(false);
        if (EnemyKilled != null)
        {
            Debug.Log("Enemy Killed");
            enemyAlive = false;
            EnemyKilled();
        }
    }
}
