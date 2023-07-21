using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Unity.VisualScripting;
using System.Threading.Tasks;

public class ThisCard : MonoBehaviour
{
    public List<Card> thisCard = new List<Card>();
    public int thisId;

    //public GameObject player;

    public int id;
    public string cardName;
    public int cost;
    public int damage;
    public int hits;
    public int block;
    public string cardDescription;
    public string cardUpdatedDescription;
    public string cardType;
    public string cardTargetingType;
    public int priorEffect;
    public int postEffect;
    public int priorPotency;
    public int postPotency;
    public string data;

    public AudioClip attackSound;

    public static int drawX;
    public int drawXCards;
    public bool xEffectCard;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI typeText;

    public Sprite thisSprite;
    public Image thatImage;

    public GameObject hand;
    public GameObject card;
    public GameObject enemyPannel;

    public int numberOfCardsInDeck;

    public bool canBePlayed;

    public GameObject singleSlash;
    public GameObject multiSlash;
    public bool banishOnDestroy;
    public bool exhaust;
    public bool ethereal;
    private bool used;
    public GameObject discardPile;
    public GameObject banishPile;
    public bool inBanishPile;
    public bool inDiscardPile;

    public AudioSource attackAudioSource;


    // Start is called before the first frame update
    void Start()
    {
        if (this.tag != "Preview" && tag != "TempCard" && tag != "Inspected")
        {
            thisCard[0] = CardDatabase.cardList[thisId];
        }
        if (tag == "TempCard")
        {
            tag = "TemporaryObject";
        }
        numberOfCardsInDeck = PlayerDeck.deckSize;
        hand = GameObject.Find("Hand");
        //player = GameObject.FindGameObjectWithTag("Player");
        banishOnDestroy = false;
        canBePlayed = false;
        drawX = 0;
        enemyPannel = GameObject.Find("Enemy Panel");
        attackAudioSource = GameObject.Find("CombatManager").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        id = thisCard[0].id;
        cardName = thisCard[0].cardName;
        if (thisCard[0].cost < 0)
        {
            cost = TurnSystem.currentEnergy;
        }
        else
        {
            cost = thisCard[0].cost;
        }
        if (thisCard[0].damage < 0)
        {
            damage = thisCard[0].damage * -cost;
        }
        else
        {
            damage = thisCard[0].damage;
        }
        if (thisCard[0].hits < 0)
        {
            hits = thisCard[0].hits * -cost;
        }
        else
        {
            hits = thisCard[0].hits;
        }
        if (thisCard[0].block < 0)
        {
            block = thisCard[0].block * -cost;
        }
        else
        {
            block = thisCard[0].block;
        }
        cardDescription = thisCard[0].cardDescription;
        cardType = thisCard[0].cardType;
        cardTargetingType = thisCard[0].cardTargetingType;
        priorEffect = thisCard[0].priorEffect;
        postEffect = thisCard[0].postEffect;
        if (thisCard[0].priorEffect < 0)
        {
            priorEffect = thisCard[0].priorEffect * -cost;
        }
        else
        {
            priorPotency = thisCard[0].priorPotency;
        }
        if (thisCard[0].postEffect < 0)
        {
            postEffect = thisCard[0].postEffect * -cost;
        }
        else
        {
            postPotency = thisCard[0].postPotency;
        }
        exhaust = thisCard[0].exhaust;
        ethereal = thisCard[0].ethereal;
        
        data = "" + (int)(damage * PlayerManager.damageMultiplier + 0.5f);

        thisSprite = thisCard[0].thisImage;
        attackSound = thisCard[0].attackSound;

        cardUpdatedDescription = string.Format(cardDescription, data);
        nameText.text = "" + cardName;
        costText.text = "" + cost;
        descriptionText.text = "" + cardUpdatedDescription;
        typeText.text = "" + cardType;

        thatImage.sprite = thisSprite;

        card = this.gameObject;

        if (this.tag == "Clone")
        {
            thisCard[0] = PlayerDeck.staticDeck[0];
            PlayerDeck.staticDeck.Remove(thisCard[0]);
            PlayerDeck.staticHand.Add (thisCard[0]);
            //numberOfCardsInDeck -= 1;
            //PlayerDeck.deckSize -= 1;
            this.tag = "TemporaryObject";
        }

        if (TurnSystem.currentEnergy >= cost & TurnSystem.isPlayersTurn == true & inDiscardPile == false)
        {
            canBePlayed = true;
        }
        else
        {
            canBePlayed = false;
        }

        if (canBePlayed)
        {
            gameObject.GetComponent<CardDrag>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<CardDrag>().enabled = false;
        }
    }

    public void CardUsed(GameObject target)
    {
        StartCoroutine(CardUsedActual(target));
    }
    IEnumerator CardUsedActual(GameObject target)
    {
        if (thisCard[0].cost < 0)
        {
            thisCard[0].cost = TurnSystem.currentEnergy;
            xEffectCard = true;
        }
        TurnSystem.currentEnergy -= cost;
        PlayerManager playerManager = FindObjectOfType<PlayerManager>();
        ApplyEffect applyEffect = FindObjectOfType<ApplyEffect>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        DualWieldBuff dualWieldBuff = playerManager.GetComponent<DualWieldBuff>();
        int uses = 1;
        if (dualWieldBuff.enabled == true && cardType == "Attack")
        {
            uses += 1;
        }
        for (int a = 0; a < uses; a++)
        {
            if (cardType == "Attack")
            {
                if (cardTargetingType == "Single")
                {
                    HealthManager targetHealthManager = target.GetComponent<HealthManager>();
                    applyEffect.ActivateEffect(target, priorEffect, priorPotency);
                    for (int i = 0; i < hits; i++)
                    {
                        attackAudioSource.clip = attackSound;
                        attackAudioSource.Play();
                        Task.Delay(1500);
                        targetHealthManager.UnitDamaged((int)(damage * PlayerManager.damageMultiplier + 0.5f), false);
                        // play hit sound
                        
                        StartCoroutine(SingleHitAnimation(target));
                        yield return new WaitForSeconds(0.4f);
                    }
                    playerManager.PlayerBlocks(block);
                    applyEffect.ActivateEffect(target, postEffect, postPotency);
                }
                if (cardTargetingType == "All")
                {
                    GameObject[] enemies;
                    enemies = GameObject.FindGameObjectsWithTag("Enemy");
                    applyEffect.ActivateEffect(target, priorEffect, priorPotency);
                    for (int i = 0; i < hits; i++)
                    {
                        // play hit sound
                        attackAudioSource.clip = attackSound;
                        attackAudioSource.Play();
                        StartCoroutine(AllHitAnimation());
                        foreach (GameObject enemy in enemies)
                        {
                            HealthManager healthManager = enemy.GetComponent<HealthManager>();
                            healthManager.UnitDamaged((int)(damage * PlayerManager.damageMultiplier + 0.5f), false);
                            applyEffect.ActivateEffect(enemy, postEffect, postPotency);
                        }
                        yield return new WaitForSeconds(0.4f);
                        playerManager.PlayerBlocks(block);
                    }
                    
                }
            }
            else if (cardType == "Skill")
            {
                if (cardTargetingType == "Self")
                {
                    attackAudioSource.clip = attackSound;
                    attackAudioSource.Play();
                    applyEffect.ActivateEffect(player, priorEffect, priorPotency);
                    // play hit sound
                    
                    playerManager.PlayerBlocks(block);
                    playerManager.PlayerDamaged(damage, null);
                    applyEffect.ActivateEffect(player, postEffect, postPotency);
                }
                if (cardTargetingType == "Single")
                {
                     attackAudioSource.clip = attackSound;
                    attackAudioSource.Play();
                    applyEffect.ActivateEffect(target, priorEffect, priorPotency);
                    // play hit sound
                   
                    playerManager.PlayerBlocks(block);
                    playerManager.PlayerDamaged(damage, null);
                    applyEffect.ActivateEffect(target, postEffect, postPotency);
                }
                if (cardTargetingType == "All")
                {
                    GameObject[] enemies;
                    enemies = GameObject.FindGameObjectsWithTag("Enemy");
                    applyEffect.ActivateEffect(player, priorEffect, priorPotency);
                    foreach (GameObject enemy in enemies)
                    {
                        attackAudioSource.clip = attackSound;
                        attackAudioSource.Play();
                        HealthManager healthManager = enemy.GetComponent<HealthManager>();
                        applyEffect.ActivateEffect(enemy, postEffect, postPotency);
                        // play hit sound
                        
                    }
                    playerManager.PlayerBlocks(block);
                    playerManager.PlayerDamaged(damage, null);
                }
            }
            else if (cardType == "Power")
            {
                attackAudioSource.clip = attackSound;
                attackAudioSource.Play();
                applyEffect.ActivateEffect(player, priorEffect, priorPotency);
                // play hit sound
                
                applyEffect.ActivateEffect(player, postEffect, postPotency);
            }
            else if (cardType == "Curse")
            {
                if (cardTargetingType == "Self")
                { 
                    attackAudioSource.clip = attackSound;
                    attackAudioSource.Play();
                    applyEffect.ActivateEffect(player, priorEffect, priorPotency);
                    playerManager.PlayerBlocks(block);
                    playerManager.PlayerDamaged(damage, null);
                    // play hit sound
                   
                }
                if (cardTargetingType == "Single")
                {
                    applyEffect.ActivateEffect(target, priorEffect, priorPotency);
                    playerManager.PlayerBlocks(block);
                    playerManager.PlayerDamaged(damage, null);
                    applyEffect.ActivateEffect(target, postEffect, postPotency);
                    // play hit sound
                    attackAudioSource.clip = attackSound;
                    attackAudioSource.Play();
                    
                }
                if (cardTargetingType == "All")
                {
                    GameObject[] enemies;
                    enemies = GameObject.FindGameObjectsWithTag("Enemy");
                    applyEffect.ActivateEffect(player, priorEffect, priorPotency);
                    foreach (GameObject enemy in enemies)
                    {
                        HealthManager healthManager = enemy.GetComponent<HealthManager>();
                        applyEffect.ActivateEffect(enemy, postEffect, postPotency);
                        // play hit sound
                        attackAudioSource.clip = attackSound;
                        attackAudioSource.Play();
                    }
                    playerManager.PlayerBlocks(block);
                    playerManager.PlayerDamaged(damage, null);
                }
            }
            if (ethereal == true)
            {
                banishOnDestroy = false;
                used = true;
            }
            if (exhaust == true)
            {
                banishOnDestroy = true;
            }
            if (a > 0)
            {
                dualWieldBuff.stacks -= 1;
                dualWieldBuff.DebuffTick();
            }
            if (xEffectCard == true)
            {
                thisCard[0].cost = -1;
                xEffectCard = false;
            }
            //Destroy();
        }
        Destroy();
    }
    IEnumerator SingleHitAnimation(GameObject target)
    {
        GameObject hitAnimation = Instantiate(singleSlash);
        hitAnimation.transform.SetParent(target.transform, false);
        yield return new WaitForSeconds(0.25f);
        Destroy(hitAnimation);
    }

    IEnumerator AllHitAnimation()
    {
        GameObject multiHitAnimation = Instantiate(multiSlash);
        multiHitAnimation.transform.SetParent(enemyPannel.transform, false);
        multiHitAnimation.transform.Rotate(0.0f, 0.0f, Random.Range(-10, 10), Space.Self);
        yield return new WaitForSeconds(0.25f);
        Destroy(multiHitAnimation);
    }

    public void Destroy()
    {
        if (ethereal == true && used == false)
        {
            banishOnDestroy = true;
        }
        if (cardType == "Curse")
        {
            ApplyEffect applyEffect = FindObjectOfType<ApplyEffect>();
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            applyEffect.ActivateEffect(player, postEffect, postPotency);
        }

        discardPile = GameObject.Find("DiscardPile");
        banishPile = GameObject.Find("BanishedPile");
        if(banishOnDestroy == false)
        {
            PlayerDeck.staticHand.Remove(thisCard[0]);
            PlayerDeck.staticDiscard.Add(thisCard[0]);
            card.transform.SetParent(discardPile.transform);
            inDiscardPile = true;
            card.SetActive(false);
            Destroy(card);
        }
        else
        {
            PlayerDeck.staticHand.Remove(thisCard[0]);
            PlayerDeck.staticBanish.Add(thisCard[0]);
            card.transform.SetParent(banishPile.transform);
            inBanishPile = true;
            card.SetActive(false);
            Destroy (card);
        }

    }
}
