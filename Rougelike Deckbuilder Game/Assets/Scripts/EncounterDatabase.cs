using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterDatabase : MonoBehaviour
{
    public static List<Encounter> EncounterList = new List<Encounter>();

    private void Awake()
    {
        EncounterList.Add(new Encounter(0, new List<string> {"null", "null"}));
        EncounterList.Add(new Encounter(1, new List<string> { "Slime", "Slime" }));
        EncounterList.Add(new Encounter(2, new List<string> { "Slime", "Goblin" }));
        EncounterList.Add(new Encounter(3, new List<string> { "Slime", "MindGoblin" }));
        EncounterList.Add(new Encounter(4, new List<string> { "Goblin", "MindGoblin" }));
        EncounterList.Add(new Encounter(5, new List<string> { "Goblin", "Goblin" }));
        EncounterList.Add(new Encounter(6, new List<string> { "Slime", "Slime", "MindGoblin" }));
        EncounterList.Add(new Encounter(7, new List<string> { "Goblin", "SnakeCultist" }));
        EncounterList.Add(new Encounter(8, new List<string> { "MindGoblin", "MindGoblin", "SnakeCultist" }));
        EncounterList.Add(new Encounter(9, new List<string> { "Goblin", "MindGoblin", "Goblin" }));
        EncounterList.Add(new Encounter(10, new List<string> { "Basilisk"}));
        EncounterList.Add(new Encounter(11, new List<string> { "SnakeCultist", "SnakeCultist" }));
        EncounterList.Add(new Encounter(12, new List<string> { "Goblin", "Goblin", "Goblin" }));
    }
}
