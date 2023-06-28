using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TargetingZone : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    public bool selfTargetZone;
    public bool enemyTargetZone;
    public bool allTargetZone;
    public bool targetingSelf;
    public bool targetingEnemy;
    public bool targetingAll;

    public GameObject enemy;
    public Image targetZone;
    public Image targetingIcon;
    public GameObject[] allTargets;

    public TargetingManager targetingManager;
    

    public void Start()
    {
        targetingManager = FindObjectOfType<TargetingManager>();
        targetZone = GetComponent<Image>();
        targetZone.enabled = false;
        allTargets = GameObject.FindGameObjectsWithTag("EnemyTargetZone");
        if (enemyTargetZone == true)
        {
            enemy = gameObject.transform.parent.gameObject;
        }
        else
        {
            enemy = null;
        }
        
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
        allTargets = GameObject.FindGameObjectsWithTag("EnemyTargetZone");
        if (enemyTargetZone == true)
        {
            enemy = gameObject.transform.parent.gameObject;
        }
        else
        {
            enemy = null;
        }
    }

    public void Update()
    {
        targetingSelf = targetingManager.targetingSelf;
        targetingEnemy = targetingManager.targetingEnemy;
        targetingAll = targetingManager.targetingAll;
        
        if ((selfTargetZone == true) && (targetingSelf == true))
        {
            targetZone.enabled = true;
        }
        else if ((enemyTargetZone == true) && (targetingEnemy == true))
        {
            targetZone.enabled = true;
        }
        else if ((allTargetZone == true) && (targetingAll == true))
        {
            targetZone.enabled = true;
        }
        else
        {
            targetZone.enabled = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Point Entered");
        if (eventData.pointerDrag == null)
        {
            Debug.Log("fail");
            return;
        }
        if ((selfTargetZone == true) && (targetingSelf == true))
        {
            targetingIcon.enabled = true;
        }
        else if ((enemyTargetZone == true) && (targetingEnemy == true))
        {
            //Debug.Log("enemy triggered");
            targetingIcon.enabled = true;
        }
        else if ((allTargetZone == true) && (targetingAll == true))
        {
            targetingIcon.enabled = true;
            foreach (GameObject enemyTargetZone in allTargets)
            {
                TargetingZone targetingZone = enemyTargetZone.GetComponent<TargetingZone>();
                targetingZone.targetingIcon.enabled = true;
            }   
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Point Exited");
        if ((selfTargetZone == true) && (targetingSelf == true))
        {
            targetingIcon.enabled = false;
        }
        else if ((enemyTargetZone == true) && (targetingEnemy == true))
        {
            //Debug.Log("Enemy disabled");
            targetingIcon.enabled = false;
        }
        else if ((allTargetZone == true) && (targetingAll == true))
        {
            targetingIcon.enabled = false;
            foreach (GameObject enemyTargetZone in allTargets)
            {
                TargetingZone targetingZone = enemyTargetZone.GetComponent<TargetingZone>();
                targetingZone.targetingIcon.enabled = false;
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag.name + " was dropped on " + gameObject.name);

        if(targetingIcon.enabled == true)
        {
            Debug.Log("card used successfully");
            
            targetingIcon.enabled = false;
            targetZone.enabled = false;

            foreach (GameObject enemyTargetZone in allTargets)
            {
                TargetingZone targetingZone = enemyTargetZone.GetComponent<TargetingZone>();
                targetingZone.targetingIcon.enabled = false;
            }
            ThisCard thisCard = targetingManager.cardInUse.GetComponent<ThisCard>();
            thisCard.CardUsed(enemy);
        }
        else
        {
            Debug.Log("target invalid");
            targetingIcon.enabled = false;
            targetZone.enabled = false;

            foreach (GameObject enemyTargetZone in allTargets)
            {
                TargetingZone targetingZone = enemyTargetZone.GetComponent<TargetingZone>();
                targetingZone.targetingIcon.enabled = false;
            }
        }
        targetingManager.targetingAll = false;
        targetingManager.targetingSelf = false;
        targetingManager.targetingEnemy = false;
        targetingManager.cardInUse = null;
    }
}
