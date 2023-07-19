using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]

public class Card
{
    public int id;
    public string cardName;
    public int cost;
    public int damage;
    public int hits;
    public int block;
    public string cardDescription;
    public string cardType;
    public string cardTargetingType;
    public int priorEffect;
    public int postEffect;
    public int priorPotency;
    public int postPotency;
    public bool ethereal;
    public bool exhaust;

    public Sprite thisImage;
    public AudioClip attackSound;

    public Card(int Id, string CardName, int Cost, int Damage, int Hits, int Block, string CardDescripion, string CardType, Sprite ThisImage, string CardTargetingType, int PriorEffect, int PostEffect, int PriorPotency, int PostPotency, bool Exhaust, bool Ethereal, AudioClip AttackSound)
    {
        id = Id;
        cardName = CardName;
        cost = Cost;
        damage = Damage;
        hits = Hits;
        block = Block;
        cardDescription = CardDescripion;
        cardType = CardType;
        thisImage = ThisImage;
        cardTargetingType = CardTargetingType;
        priorEffect = PriorEffect;
        postEffect = PostEffect;
        priorPotency = PriorPotency;
        postPotency = PostPotency;
        exhaust = Exhaust;
        ethereal = Ethereal;
        attackSound = AttackSound;
    }

}
