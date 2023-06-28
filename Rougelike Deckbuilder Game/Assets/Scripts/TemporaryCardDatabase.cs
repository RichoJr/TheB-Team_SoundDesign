using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryCardDatabase : MonoBehaviour
{
    public static List<Card> tempCardList = new List<Card>();

    private void Awake()
    {
        tempCardList.Add(new Card(0, "None", 0, 0, 0, 0, "None", "None", Resources.Load<Sprite>("1"), "None", 0, 0, 0, 0, false, false));
        tempCardList.Add(new Card(1, "Mind Goblin D's", 1, 0, 0, 0, "A Terrible Joke.\nExhaust.", "Curse", Resources.Load<Sprite>("1"), "Self", 0, 0, 0, 0, true, false));
        tempCardList.Add(new Card(2, "Whiplash", 1, 0, 0, 0, "If This Card isnt used take 5 Damage at the End ot the Turn.", "Curse", Resources.Load<Sprite>("1"), "Self", 0, 9, 0, 5, false, false));
        tempCardList.Add(new Card(3, "Stab", 0, 3, 1, 0, "Deal {0} Damage.\nExhaust.", "Attack", Resources.Load<Sprite>("1"), "Single", 0, 0, 0, 0, true, false));
        tempCardList.Add(new Card(4, "Antivenom", 2, 0, 0, 0, "Cure Yourself of All Poison Stacks.\nExhaust.\nEthereal", "Skill", Resources.Load<Sprite>("1"), "Self", 15, 0, 1, 0, true, true));
        tempCardList.Add(new Card(5, "None", 0, 0, 0, 0, "None", "None", Resources.Load<Sprite>("1"), "None", 0, 0, 0, 0, false, false));
        tempCardList.Add(new Card(6, "None", 0, 0, 0, 0, "None", "None", Resources.Load<Sprite>("1"), "None", 0, 0, 0, 0, false, false));
        tempCardList.Add(new Card(7, "None", 0, 0, 0, 0, "None", "None", Resources.Load<Sprite>("1"), "None", 0, 0, 0, 0, false, false));
        tempCardList.Add(new Card(8, "None", 0, 0, 0, 0, "None", "None", Resources.Load<Sprite>("1"), "None", 0, 0, 0, 0, false, false));
    }

}
