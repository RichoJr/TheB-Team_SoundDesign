using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThisEncounter : MonoBehaviour
{
    public int thisId;
    public int encounterId;
    public List<string> enemyList;
    public GameObject enemyZone;
    public List<Encounter> thisEncounter = new List<Encounter> ();
    // Start is called before the first frame update
    public void Start()
    {
        enemyZone = GameObject.Find("Enemy Panel");
        thisEncounter[0] = EncounterDatabase.EncounterList[thisId];
    }

    // Update is called once per frame
    public void CallEncounter(int Encounterid)
    {
        switch (Encounterid)
        {
            case 0:
                break;
            case 1:
                thisEncounter[0] = EncounterDatabase.EncounterList[Encounterid];
                encounterId = thisEncounter[0].encounterId;
                enemyList = thisEncounter[0].enemies;
                break;
            case 2:
                thisEncounter[0] = EncounterDatabase.EncounterList[Random.Range(2,7)];
                encounterId = thisEncounter[0].encounterId;
                enemyList = thisEncounter[0].enemies;
                break;
            case 3:
                thisEncounter[0] = EncounterDatabase.EncounterList[Random.Range(7, 10)];
                encounterId = thisEncounter[0].encounterId;
                enemyList = thisEncounter[0].enemies;
                break;
            case 4:
                thisEncounter[0] = EncounterDatabase.EncounterList[10];
                encounterId = thisEncounter[0].encounterId;
                enemyList = thisEncounter[0].enemies;
                break;
            case 5:
                thisEncounter[0] = EncounterDatabase.EncounterList[Random.Range(11,13)];
                encounterId = thisEncounter[0].encounterId;
                enemyList = thisEncounter[0].enemies;
                break;
        }
    }
}
