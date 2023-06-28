using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainDeck : MonoBehaviour
{
    public static List<Card> staticMainDeck = new List<Card>();
    public static int mainDeckSize;
    public List<Card> mainDeck = new List<Card>();

    public TextMeshProUGUI mainDeckCountText;

    // Start is called before the first frame update
    void Start()
    {
        mainDeck[0] = CardDatabase.cardList[1];
        mainDeck[1] = CardDatabase.cardList[1];
        mainDeck[2] = CardDatabase.cardList[1];
        mainDeck[3] = CardDatabase.cardList[1];
        mainDeck[4] = CardDatabase.cardList[2];
        mainDeck[5] = CardDatabase.cardList[2];
        mainDeck[6] = CardDatabase.cardList[2];
        mainDeck[7] = CardDatabase.cardList[2];
        mainDeck[8] = CardDatabase.cardList[3];
        mainDeck[9] = CardDatabase.cardList[4];
        mainDeck[10] = CardDatabase.cardList[5];
        mainDeck[11] = CardDatabase.cardList[6];
        Debug.Log("static main created");

        staticMainDeck = mainDeck;
        mainDeckSize = mainDeck.Count;
    }

    // Update is called once per frame
    void Update()
    {
        mainDeckCountText.text = "" + mainDeck.Count;
    }

    public void AddCardToMain(List<Card> newCard)
    {
        mainDeck.Add(newCard[0]);
        staticMainDeck = mainDeck;
        mainDeckSize = mainDeck.Count;
    }
}
