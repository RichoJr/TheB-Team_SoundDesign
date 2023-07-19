using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class EnemyAction
{
    public int id;
    public string actionName;
    public int buffEffect;
    public int debuffEffect;
    public int damage;
    public int block;
    public int hits;
    public int buffpotency;
    public int debuffpotency;

    public Sprite thisImage;
    public AudioClip attackSound;

    public EnemyAction(int Id, string ActionName, int Damage, int Block, int Hits, int BuffEffect, int BuffPotency, int DebuffEffect, int DebuffPotency, Sprite ThisImage, AudioClip AttackSound)
    {
        id = Id;
        actionName = ActionName;
        buffEffect = BuffEffect;
        debuffEffect = DebuffEffect;
        damage = Damage;
        block = Block;
        thisImage = ThisImage;
        hits = Hits;
        buffpotency = BuffPotency;
        debuffpotency = DebuffPotency;
        attackSound = AttackSound;
    }
}
