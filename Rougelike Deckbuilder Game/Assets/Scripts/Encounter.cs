using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Encounter
{
    public int encounterId;
    public List<string> enemies;

    public Encounter(int EncounterId, List<string> Enemies)
    {
        encounterId = EncounterId;
        enemies = Enemies;
    }
}
