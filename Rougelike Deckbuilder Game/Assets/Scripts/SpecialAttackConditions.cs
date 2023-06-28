using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackConditions : MonoBehaviour
{
    public TurnSystem TurnSystem;
    // Start is called before the first frame update
    void Start()
    {
        TurnSystem = FindObjectOfType<TurnSystem>();
    }

    public bool CheckConditions(int condition)
    {
        switch (condition)
        {
            case 0: 
                return false;
            case 11: 
                if (TurnSystem.enemyTurn == 4)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                case 2: 
                return true;
                case 3: return false;
                case 4: return true;
            default:
                return false;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
