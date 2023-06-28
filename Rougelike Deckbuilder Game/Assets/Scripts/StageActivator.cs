using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StageActivator : MonoBehaviour
{
    public Button button;
    public OverworldManager OverworldManager;
    public List<GameObject> previousStages;
    public bool startingStage;
    public Material greyedMaterial;
    public Material whiteMaterial;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        OverworldManager = FindObjectOfType<OverworldManager>();
        button.onClick.AddListener(delegate { OverworldManager.BeginStage(button.gameObject); });
    }

    // Update is called once per frame
    void Update()
    {
        if (startingStage == true)
        {
            if (OverworldManager.previousStage == null)
            {
                button.interactable = true;
            }
            else
            {
                button.interactable = false;
            }
        }
        else
        {
            if (previousStages.Contains(OverworldManager.previousStage) && OverworldManager.currentStage == null)
            {
                button.interactable = true;
            }
            else
            {
                button.interactable = false;
            }
        }

        if (OverworldManager.previousStage == this.gameObject)
        {
            LineRenderer[] lines;
            lines = GetComponentsInChildren<LineRenderer>();
            foreach (LineRenderer line in lines)
            {
                line.material = whiteMaterial;
            }
        }
        else
        {
            LineRenderer[] lines;
            lines = GetComponentsInChildren<LineRenderer>();
            foreach (LineRenderer line in lines)
            {
                line.material = greyedMaterial;
            }
        }
    }
}
