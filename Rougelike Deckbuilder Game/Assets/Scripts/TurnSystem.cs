using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TurnSystem : MonoBehaviour
{
    public static bool isPlayersTurn;
    public static int playerTurn;
    public static int enemyTurn;

    public int maxEnergy;
    public static int currentEnergy;
    public TextMeshProUGUI energyText;
    
    public PlayerDeck playerDeck;
    public PlayerManager playerManager;

    public static bool isInCombat;

    public delegate void OnTurnStart();
    public static event OnTurnStart TurnStart;

    public delegate void OnTurnEnd();
    public static event OnTurnEnd TurnEnd;

    public delegate void OnCombatEnd();
    public static event OnCombatEnd CombatEnd;


    // Start is called before the first frame update
    void Start()
    {
        isPlayersTurn = false;
        playerTurn = 1;
        enemyTurn = 0;

        maxEnergy = 3;
        currentEnergy = 3;
        playerManager = FindObjectOfType<PlayerManager>();
        playerDeck = FindObjectOfType<PlayerDeck>();
    }

    public void FirstTurnStart()
    {
        isInCombat = true;
        isPlayersTurn = false;
        playerTurn = 1;
        enemyTurn = 0;

        maxEnergy = 3;
        currentEnergy = 3;

        playerDeck.DrawForTurn();
        GameObject[] enemies;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            EnemyAttacks enemyAttacks = enemy.GetComponent<EnemyAttacks>();
            enemyAttacks.EnemyIntent();
        }
    }

    public void EndTurn()
    {
        if (isPlayersTurn == true)
        {
            isPlayersTurn = false;
            enemyTurn += 1;
            playerDeck.DiscardHand();
            if (TurnEnd != null)
            {
                TurnEnd();
            }
            if (isInCombat == true)
            {
                StartCoroutine(EnemyTurn());
            }
        }
    }

    IEnumerator EnemyTurn()
    {
        GameObject[] enemies;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            
            EnemyAttacks enemyAttacks = enemy.GetComponent<EnemyAttacks>();
            enemyAttacks.EnemyAttack();
            int hits = enemyAttacks.hits;
            yield return new WaitForSeconds(1f+(0.3f*hits));
        }
        Debug.Log(enemies.Length);
        new WaitForSeconds(2f * enemies.Length);
        Debug.Log("start turn");
        StartTurn();
    }


    public void StartTurn()
    {
        if (isInCombat == true)
        {
            if (TurnStart != null)
            {
                TurnStart();
            }
            playerManager.SheildDecay();

            GameObject[] enemies;
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                EnemyAttacks enemyAttacks = enemy.GetComponent<EnemyAttacks>();
                enemyAttacks.EnemyIntent();
            }

            playerDeck.DrawForTurn();
            playerTurn += 1;
            currentEnergy = maxEnergy;
        }
    }

    // Update is called once per frame
    void Update()
    {
        playerDeck = FindObjectOfType<PlayerDeck>();
        energyText.text = currentEnergy + "/" + maxEnergy;

    }

    private void OnEnable()
    {
        HealthManager.EnemyKilled += CheckEndCombat;
        CombatStartAndEnd.CombatStart += FirstTurnStart;
    }

    private void OnDisable()
    {
        HealthManager.EnemyKilled -= CheckEndCombat;
        CombatStartAndEnd.CombatStart -= FirstTurnStart;
    }

    public void CheckEndCombat()
    {
        GameObject[] enemies;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log("Enemies remaining = " + enemies.Length);
        if (enemies.Length <= 0)
        {
            isInCombat = false;
            Debug.Log("CombatEnd");
            isPlayersTurn = false;
            if (CombatEnd != null)
            {
                CombatEnd();
            }
        }
        else
        {
            Debug.Log("Combat Continues");
        }
    }
}
