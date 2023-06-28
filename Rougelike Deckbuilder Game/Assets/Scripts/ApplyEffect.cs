using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ApplyEffect : MonoBehaviour
{
    public PlayerManager playerManager;
    public GameObject player;
    private PlayerDeck playerDeck;
    // Start is called before the first frame update
    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerDeck = FindObjectOfType<PlayerDeck>();
    }

    // Update is called once per frame
    public void ActivateEffect(GameObject target, int statusEffect, int effectPotency)
    {
        

        switch (statusEffect)
        {
            case 0:
                break;
            case 1:
                StrengthBuff selfStrengthBuff = playerManager.GetComponent<StrengthBuff>();
                selfStrengthBuff.enabled = true;
                selfStrengthBuff.Startbuff(effectPotency);
                break;
            case 2:
                PlayerDeck deckmanager = FindObjectOfType<PlayerDeck>();
                deckmanager.CardDraw(effectPotency);
                break;
            case 3:
                if (target == player)
                {
                    WeaknessDebuff weaknessDebuff = playerManager.GetComponent<WeaknessDebuff>();
                    weaknessDebuff.enabled = true;
                    weaknessDebuff.Startbuff(effectPotency);
                }
                else
                {
                    WeaknessDebuff weaknessDebuff = target.GetComponent<WeaknessDebuff>();
                    weaknessDebuff.enabled = true;
                    weaknessDebuff.Startbuff(effectPotency);
                }
                break;
            case 4:
                if (target == player)
                {
                    StrengthBuff strengthBuff = playerManager.GetComponent<StrengthBuff>();
                    strengthBuff.enabled = true;
                    strengthBuff.Startbuff(effectPotency);
                }
                else
                {
                    StrengthBuff strengthBuff = target.GetComponent<StrengthBuff>();
                    strengthBuff.enabled = true;
                    strengthBuff.Startbuff(effectPotency);
                }
                break;
            case 5:
                if (target == player)
                {
                    ImmunityBuff immunityBuff = playerManager.GetComponent<ImmunityBuff>();
                    immunityBuff.enabled = true;
                    immunityBuff.Startbuff(effectPotency);
                }
                else
                {
                    ImmunityBuff immunityBuff = target.GetComponent<ImmunityBuff>();
                    immunityBuff.enabled = true;
                    immunityBuff.Startbuff(effectPotency);
                }
                break;
            case 6:
                if (target == player)
                {
                    PoisonDebuff poisonDebuff = playerManager.GetComponent<PoisonDebuff>();
                    poisonDebuff.enabled = true;
                    poisonDebuff.Startbuff(effectPotency);
                }
                else
                {
                    PoisonDebuff poisonDebuff = target.GetComponent<PoisonDebuff>();
                    poisonDebuff.enabled = true;
                    poisonDebuff.Startbuff(effectPotency);
                }
                break;
            case 7:
                playerDeck.AddTempCardtoDiscard(TemporaryCardDatabase.tempCardList[1], effectPotency);
                break;
            case 8:
                playerDeck.AddTempCardtoDeck(TemporaryCardDatabase.tempCardList[2], effectPotency);
                break;
            case 9:
                if (target == player)
                {
                    playerManager.PlayerDamaged(effectPotency, null);
                }
                else
                {
                    HealthManager health = target.GetComponent<HealthManager>();
                    health.UnitDamaged(effectPotency);
                }
                break;
            case 10:
                TurnSystem.currentEnergy += effectPotency;
                break;
            case 11:
                DualWieldBuff dualWieldBuff = playerManager.GetComponent<DualWieldBuff>();
                dualWieldBuff.enabled = true;
                dualWieldBuff.Startbuff(effectPotency);
                break;
            case 12:
                playerDeck.AddTempCardtoHand(TemporaryCardDatabase.tempCardList[3], effectPotency);
                break;
            case 13:
                if (target == player)
                {
                    PoisonCoatingBuff poisonCoatingBuff = playerManager.GetComponent<PoisonCoatingBuff>();
                    poisonCoatingBuff.enabled = true;
                    poisonCoatingBuff.Startbuff(effectPotency);
                }
                else
                {
                    PoisonCoatingBuff poisonCoatingBuff = target.GetComponent<PoisonCoatingBuff>();
                    poisonCoatingBuff.enabled = true;
                    poisonCoatingBuff.Startbuff(effectPotency);
                }
                break;
            case 14:
                PoisonDebuff venomDebuff = playerManager.GetComponent<PoisonDebuff>();
                venomDebuff.enabled = true;
                venomDebuff.Startbuff(effectPotency);

                playerDeck.AddTempCardtoDiscard(TemporaryCardDatabase.tempCardList[4], 2);
                break;
            case 15:
                PoisonDebuff cureDebuff = playerManager.GetComponent<PoisonDebuff>();
                if (playerManager.GetComponent<PoisonDebuff>().enabled == true)
                {
                    cureDebuff.stacks = 0;
                    cureDebuff.DebuffCheck();
                    cureDebuff.enabled = true;
                }
                break;
            case 16:

                break;
            case 17:

                break;
            case 18:

                break;
            default:
                break;

        }
    }
}
