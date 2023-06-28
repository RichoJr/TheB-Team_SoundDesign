using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

public class PlayerDeck : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    public List<Card> container = new List<Card>();
    public static List<Card> staticDeck = new List<Card>();

    public List<Card> handList = new List<Card> ();
    public List<Card> discardList = new List<Card>();
    public List<Card> banishedList = new List<Card>();
    public static List<Card> staticHand = new List<Card>();
    public static List<Card> staticDiscard = new List<Card>();
    public static List<Card> staticBanish = new List<Card>();

    public static int deckSize;
    public static int handSize;
    public static int discardSize;
    public static int banishedSize;

    public GameObject cardToHand;

    public GameObject hand;

    public TextMeshProUGUI cardsLeftInDeckText;
    public TextMeshProUGUI cardsDiscardedText;
    public TextMeshProUGUI cardsBanisedText;
    
    private void OnEnable()
    {
        CombatStartAndEnd.CombatStart += FirstTurnStart;
        TurnSystem.CombatEnd += OnCombatEnd;
        CombatStartAndEnd.RewardSelected += OnReturnToOverworld;
    }

    private void OnDisable()
    {
        CombatStartAndEnd.CombatStart -= FirstTurnStart;
        TurnSystem.CombatEnd -= OnCombatEnd;
        CombatStartAndEnd.RewardSelected -= OnReturnToOverworld;
    }

    public void FirstTurnStart()
    {
        Debug.Log("main called");
        deck.AddRange(MainDeck.staticMainDeck);
        deckSize = deck.Count;
        cardsLeftInDeckText.text = "" + deckSize;
        Shuffle();
    }

    public void Shuffle()
    {
        for (int i = 0; i < deckSize; i++)
        {
            container[0] = deck[i];
            int randomIndex = Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = container[0]; 
        }
    }

    public void ShuffleDiscardInto()
    {
        for (int i = 0; i < discardSize; i++)
        {
            deck.Add(discardList[0]);
            discardList.RemoveAt(0);
        }
        
        deckSize = deck.Count;

        for (int i = 0; i < deckSize; i++)
        {
            container[0] = deck[i];
            int randomIndex = Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = container[0];
        }
        deckSize = deck.Count;
    }

    public void Update()
    {
        staticDeck = deck;
        staticHand = handList;
        staticDiscard = discardList;
        staticBanish = banishedList;
        deckSize = deck.Count;
        handSize = handList.Count;
        discardSize = discardList.Count;
        banishedSize = banishedList.Count;
        cardsLeftInDeckText.text = "" + deckSize;
        cardsDiscardedText.text = "" + discardSize;
        cardsBanisedText.text = "" + banishedSize;

    }

    public void DrawForTurn()
    {
        StartCoroutine(Draw(5));
    }

    public void CardDraw(int cardsToDraw)
    {
        StartCoroutine(Draw(cardsToDraw));
    }

    public void DiscardHand()
    {
        StartCoroutine(Discard(handSize));
    }

    IEnumerator Draw(int x)
    {
        if (TurnSystem.isInCombat == true)
        {
            Debug.Log("Draw Starts");
            if (deckSize == 0)
            {
                Debug.Log("no more cards");
                ShuffleDiscardInto();
            }
            for (int i = 0; i < x; i++)
            {
                if (deckSize == 0)
                {
                    Debug.Log("no more cards");
                    ShuffleDiscardInto();
                }
                yield return new WaitForSeconds(0.3f);
                Instantiate(cardToHand, transform.position, transform.rotation);
                if (deckSize == 0)
                {
                    Debug.Log("no more cards");
                    ShuffleDiscardInto();
                }

            }
            Debug.Log("draw ends");
            if (TurnSystem.isPlayersTurn == false)
            {
                TurnSystem.isPlayersTurn = true;
            }
        }
        
    }

    IEnumerator Discard (int x)
    {
        for (int i = 0; i<x; i++)
        {
            if (TurnSystem.isInCombat == true) //Was put in place to solve a strange rare error
            {
                GameObject cardInHand = hand.transform.GetChild(0).gameObject;
                ThisCard firstCard = cardInHand.GetComponent<ThisCard>();
                firstCard.Destroy();
                yield return new WaitForSeconds(0.1f);
            }
            
        }
    }

    public void OnCombatEnd()
    {
        return;
    }

    public void OnReturnToOverworld()
    {
        deck.Clear();
        staticDeck.Clear();
        handList.Clear();
        discardList.Clear();
        banishedList.Clear();
        staticHand.Clear();
        staticDiscard.Clear();
        staticBanish.Clear();
    }

    public void AddTempCardtoDeck(Card tempCard, int copies)
    {
        for (int i = 0; i < copies; i++)
        {
            deck.Add(tempCard);
        }
    }

    public void AddTempCardtoHand(Card tempCard, int copies)
    {
        for (int i = 0; i < copies; i++)
        {
            GameObject card = Instantiate(cardToHand, transform.position, transform.rotation);
            card.GetComponent<ThisCard>().thisCard[0] = tempCard;
            card.tag = "TempCard";
        }
    }

    public void AddTempCardtoDiscard(Card tempCard, int copies)
    {
        for(int i = 0; i < copies; i++)
        {
            discardList.Add(tempCard);
        }
    }
}
