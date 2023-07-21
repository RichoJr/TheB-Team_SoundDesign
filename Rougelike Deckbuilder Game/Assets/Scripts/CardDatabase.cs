using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    public static List<Card> cardList = new List<Card>();
    public static int X = -1;

    private void Awake()
    {
        cardList.Add(new Card(0, "None", 0, 0, 0, 0, "None", "None", Resources.Load <Sprite> ("1"), "None", 0, 0, 0, 0, false, false, Resources.Load<AudioClip>("Sounds/Fart")));
        cardList.Add(new Card(1, "Strike", 1, 6, 1, 0, "Deal {0} Damage", "Attack", Resources.Load<Sprite>("1"), "Single", 0, 0, 0, 0, false, false, Resources.Load<AudioClip>("Sounds/light_attack")));
        cardList.Add(new Card(2, "Block", 1, 0, 0, 5, "Gain 5 Block", "Skill", Resources.Load<Sprite>("1"), "Self", 0, 0, 0, 0, false, false, Resources.Load<AudioClip>("Sounds/buff")));
        cardList.Add(new Card(3, "Shield Bash", 1, 5, 1, 5, "Deal {0} Damage and Gain 5 Block", "Attack", Resources.Load<Sprite>("1"), "Single", 0, 0, 0, 0, false, false, Resources.Load<AudioClip>("Sounds/Fart")));
        cardList.Add(new Card(4, "Heavy Swing", 2, 8, 1, 0, "Deal {0} Damage and Gain 2 Strength", "Attack", Resources.Load<Sprite>("1"), "Single", 0, 1, 0, 2, false, false, Resources.Load<AudioClip>("Sounds/heavy_attack")));
        cardList.Add(new Card(5, "Spin Attack", 1, 5, 1, 0, "Deal {0} Damage to all Enemies", "Attack", Resources.Load<Sprite>("1"), "All", 0, 0, 0, 0, false, false, Resources.Load<AudioClip>("Sounds/light_attack")));
        cardList.Add(new Card(6, "Hit and Run", 1, 5, 1, 0, "Deal {0} Damage and Draw 1 card", "Attack", Resources.Load<Sprite>("1"), "Single", 0, 2, 0, 1, false, false, Resources.Load<AudioClip>("Sounds/light_attack")));
        cardList.Add(new Card(7, "Supplements", 1, 0, 0, 0, "Gain 1 Immunity.", "Power", Resources.Load<Sprite>("1"), "Self", 5, 0, 1, 0, false, false, Resources.Load<AudioClip>("Sounds/buff")));
        cardList.Add(new Card(8, "Ethereal Armour", 1, 0, 0, 11, "Gain 11 Block.\nEthereal(Banish if not Played).", "Skill", Resources.Load<Sprite>("1"), "Self", 0, 0, 0, 0, false, true, Resources.Load<AudioClip>("Sounds/Fart")));
        cardList.Add(new Card(9, "Dual Wield", 1, 0, 0, 0, "The Next Attack Used is Played Twice", "Power", Resources.Load<Sprite>("1"), "Self", 11, 0, 1, 0, false, false, Resources.Load<AudioClip>("Sounds/buff")));
        cardList.Add(new Card(10, "Overdrive", 1, 0, 0, 0, "Gain 2 Energy.\nAdd a Whiplash to your Deck", "Skill", Resources.Load<Sprite>("1"), "Self", 10, 8, 2, 1, false, false, Resources.Load<AudioClip>("Sounds/buff")));
        cardList.Add(new Card(11, "Poison Spray", 1, 0, 0, 0, "Apply 4 Poison to All Enemies", "Skill", Resources.Load<Sprite>("1"), "All", 0, 6, 0, 4, false, false, Resources.Load<AudioClip>("Sounds/Fart")));
        cardList.Add(new Card(12, "3 Point Combo", 2, 6, 3, 0, "Deal {0} Damage to an Enemy 3 Times", "Attack", Resources.Load<Sprite>("1"), "Single", 0, 0, 0, 0, false, false, Resources.Load<AudioClip>("Sounds/light_attack")));
        cardList.Add(new Card(13, "Poisoned Scrape", 1, 4, 1, 0, "Deal {0} Damage and apply 4 Poison", "Attack", Resources.Load<Sprite>("1"), "Single", 0, 6, 0, 4, false, false, Resources.Load<AudioClip>("Sounds/Fart")));
        cardList.Add(new Card(14, "Tactical Draw", 0, 0, 0, 0, "Draw 2 cards.\nExhaust.", "Skill", Resources.Load<Sprite>("1"), "Self", 2, 0, 2, 0, true, false, Resources.Load<AudioClip>("Sounds/buff")));
        cardList.Add(new Card(15, "Thousand Cuts", 2, 2, 3, 0, "Deal {0} Damage to All Enemies 3 times", "Attack", Resources.Load<Sprite>("1"), "All", 0, 0, 0, 0, false, false, Resources.Load<AudioClip>("Sounds/stab_attack")));
        cardList.Add(new Card(16, "Poison Coating", 1, 0, 0, 0, "Whenever You Hit an Enemy apply 1 Poison", "Power", Resources.Load<Sprite>("1"), "Self", 13, 0, 1, 0, false, false, Resources.Load<AudioClip>("Sounds/buff")));
        cardList.Add(new Card(17, "Cloak & Dagger", 2, 0, 0, 6, "Gain 6 Block and Add 2 Stabs to Hand", "Skill", Resources.Load<Sprite>("1"), "Self", 12, 0, 2, 0, false, false, Resources.Load<AudioClip>("Sounds/buff")));
        cardList.Add(new Card(18, "None", 0, 0, 0, 0, "None", "None", Resources.Load<Sprite>("1"), "None", 0, 0, 0, 0, false, false, Resources.Load<AudioClip>("Sounds/Fart")));
        cardList.Add(new Card(19, "None", 0, 0, 0, 0, "None", "None", Resources.Load<Sprite>("1"), "None", 0, 0, 0, 0, false, false, Resources.Load<AudioClip>("Sounds/Fart")));
        cardList.Add(new Card(20, "Judgement Cut", X, 4, (X) * 2, 0, "Deal {0} damage to All Enemies X*2 times.\n(X = Energy Spent)", "Attack", Resources.Load<Sprite>("1"), "All", 0, 0, 0, 0, false, false, Resources.Load<AudioClip>("Sounds/heavy_attack")));
        cardList.Add(new Card(21, "None", 0, 0, 0, 0, "None", "None", Resources.Load<Sprite>("1"), "None", 0, 0, 0, 0, false, false, Resources.Load<AudioClip>("Sounds/Fart")));
        cardList.Add(new Card(22, "None", 0, 0, 0, 0, "None", "None", Resources.Load<Sprite>("1"), "None", 0, 0, 0, 0, false, false, Resources.Load<AudioClip>("Sounds/Fart")));
    }
}
