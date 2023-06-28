using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TurnSystem;

public class OverworldManager : MonoBehaviour
{
    public GameObject currentStage;
    public GameObject previousStage;

    // Start is called before the first frame update
    void Start()
    {
        currentStage = null;
        previousStage = null;
    }
    private void OnEnable()
    {
        TurnSystem.CombatEnd += OnStageComplete;
    }

    private void OnDisable()
    {
        TurnSystem.CombatEnd -= OnStageComplete;
    }

    public void BeginStage(GameObject stage)
    {
        currentStage = stage;
        //previousStage = null;
    }
    public void OnStageComplete()
    {
        previousStage = currentStage;
        currentStage = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
