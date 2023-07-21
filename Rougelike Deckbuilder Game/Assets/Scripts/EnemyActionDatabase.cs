using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActionDatabase : MonoBehaviour
{
    public static List <EnemyAction> attacklist = new List<EnemyAction> ();

    private void Awake()
    {
        attacklist.Add(new EnemyAction(0, "None", 0, 0, 0, 0, 0, 0, 0, Resources.Load<Sprite>("Circle"), Resources.Load<AudioClip>("Sounds/Fart")));
        attacklist.Add(new EnemyAction(1, "Bash", 5, 0, 1, 0, 0, 0, 0, Resources.Load<Sprite>("Circle"), Resources.Load<AudioClip>("Sounds/Fart")));
        attacklist.Add(new EnemyAction(2, "Slime Sling", 3, 0, 1, 0, 0, 3, 1, Resources.Load<Sprite>("Circle"), Resources.Load<AudioClip>("Sounds/liquid_attack")));
        attacklist.Add(new EnemyAction(3, "Stab Stab", 4, 0, 2, 0, 0, 0, 0, Resources.Load<Sprite>("Circle"), Resources.Load<AudioClip>("Sounds/enemy_stab")));
        attacklist.Add(new EnemyAction(4, "Scheming", 0, 0, 0, 4, 3, 0, 0, Resources.Load<Sprite>("Circle"), Resources.Load<AudioClip>("Sounds/Fart")));
        attacklist.Add(new EnemyAction(5, "Sleaze Ball", 7, 0, 1, 0, 0, 6, 2, Resources.Load<Sprite>("Circle"), Resources.Load<AudioClip>("Sounds/liquid_attack")));
        attacklist.Add(new EnemyAction(6, "Awful Joke", 0, 0, 1, 0, 0, 7, 1, Resources.Load<Sprite>("Circle"), Resources.Load<AudioClip>("Sounds/Fart")));
        attacklist.Add(new EnemyAction(7, "Repeated Stabbing", 1, 0, 4, 0, 0, 0, 0, Resources.Load<Sprite>("Circle"), Resources.Load<AudioClip>("Sounds/Fart")));
        attacklist.Add(new EnemyAction(8, "Coat Blades", 0, 0, 0, 13, 1, 0, 0, Resources.Load<Sprite>("Circle"), Resources.Load<AudioClip>("Sounds/Fart")));
        attacklist.Add(new EnemyAction(9, "Fang Strike", 3, 0, 2, 0, 0, 6, 3, Resources.Load<Sprite>("Circle"), Resources.Load<AudioClip>("Sounds/Fart")));
        attacklist.Add(new EnemyAction(10, "Coiled Strike", 5, 5, 1, 0, 0, 0, 0, Resources.Load<Sprite>("Circle"), Resources.Load<AudioClip>("Sounds/Fart")));
        attacklist.Add(new EnemyAction(11, "Envenom", 0, 0, 1, 4, 3, 14, 1, Resources.Load<Sprite>("Circle"), Resources.Load<AudioClip>("Sounds/liquid_attack")));
        attacklist.Add(new EnemyAction(12, "Corrosive Bite", 9, 0, 1, 0, 0, 3, 1, Resources.Load<Sprite>("Circle"), Resources.Load<AudioClip>("Sounds/Fart")));
        attacklist.Add(new EnemyAction(13, "None", 0, 0, 0, 0, 0, 0, 0, Resources.Load<Sprite>("Circle"), Resources.Load<AudioClip>("Sounds/Fart")));
    }
}
