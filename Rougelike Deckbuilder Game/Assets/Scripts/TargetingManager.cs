using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingManager : MonoBehaviour
{
    public bool targetingSelf;
    public bool targetingEnemy;
    public bool targetingAll;

    public GameObject cardInUse;
    // Start is called before the first frame update
    public void Start()
    {
        targetingSelf = false;
        targetingEnemy = false;
        targetingAll = false;

        cardInUse = null;
    }
    
}
