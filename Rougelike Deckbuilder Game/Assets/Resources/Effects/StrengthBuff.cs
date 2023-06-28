using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static TurnSystem;

public class StrengthBuff : MonoBehaviour
{
    
    public GameObject attached;
    private WeaknessDebuff weaknessDebuff;
    public int duration;
    public GameObject buffIcon;
    private TextMeshProUGUI durationText;
    GameObject child;

    private void Start()
    {
        attached = this.gameObject;
        weaknessDebuff = GetComponent<WeaknessDebuff>();
        buffIcon = Resources.Load("effects/StrengthBuffIcon Variant") as GameObject;
        //durationText = buffIcon.GetComponentInChildren<TextMeshProUGUI>();
        enabled = false;
    }
    private void OnEnable()
    {
        if (GameObject.ReferenceEquals(attached, GameObject.FindGameObjectWithTag("PlayerManager")))
        {
            TurnSystem.TurnEnd += DebuffTick;
        }
        else
        {
            TurnSystem.TurnStart += DebuffTick;
        }
        TurnSystem.CombatEnd += OnCombatEnd;
        CombatStartAndEnd.RewardSelected += OnCombatEnd;
    }
    private void OnDisable()
    {
        if (GameObject.ReferenceEquals(attached, GameObject.FindGameObjectWithTag("PlayerManager")))
        {
            TurnSystem.TurnEnd -= DebuffTick;
        }
        else
        {
            TurnSystem.TurnStart -= DebuffTick;
        }
        TurnSystem.CombatEnd -= OnCombatEnd;
        CombatStartAndEnd.RewardSelected -= OnCombatEnd;
    }

    public void DebuffTick()
    {
        duration -= 1;
        if (duration <= 0)
        {
            duration = 0;

            if (GameObject.ReferenceEquals(attached, GameObject.FindGameObjectWithTag("PlayerManager"))) //(attached == GameObject.FindGameObjectWithTag("PlayerManager"))
            {
                PlayerManager.damageMultiplier = 1;
                enabled = false;
                Destroy(child);
                
            }
            else
            {
                HealthManager healthManager = attached.GetComponent<HealthManager>();
                healthManager.damageModifier = 1;
                enabled = false;
                Destroy(child);
            }
        }
    }
    public void Startbuff(int turns)
    {
        if (GameObject.ReferenceEquals(attached, GameObject.FindGameObjectWithTag("PlayerManager")))
        {
            Debug.Log("is on playermanager");
            if (weaknessDebuff.enabled == true)
            {
                int excess = turns - weaknessDebuff.duration;
                weaknessDebuff.duration -= turns;
                weaknessDebuff.DebuffCheck();
                if (excess <= 0)
                {
                    enabled = false;
                }
                else
                {
                    duration = excess;
                    PlayerManager.damageMultiplier = 1.25f;
                    if (child == null)
                    {
                        child = Instantiate(buffIcon);
                        child.transform.SetParent(PlayerManager.playerEffectList.transform);
                        child.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                        durationText = child.GetComponentInChildren<TextMeshProUGUI>();
                    }
                }
            }
            else
            {
                duration = turns;
                PlayerManager.damageMultiplier = 1.25f;
                if (child == null)
                {
                    child = Instantiate(buffIcon);
                    child.transform.SetParent(PlayerManager.playerEffectList.transform);
                    child.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                    durationText = child.GetComponentInChildren<TextMeshProUGUI>();
                }
            }
        }
        else
        {
            HealthManager healthManager = attached.GetComponent<HealthManager>();
            GameObject buffDebuffPannel = attached.GetComponentInChildren<HorizontalLayoutGroup>().gameObject;
            if (weaknessDebuff.enabled == true)
            {
                int excess = turns - weaknessDebuff.duration;
                weaknessDebuff.duration -= turns;
                weaknessDebuff.DebuffCheck();
                if (excess <= 0)
                {
                    enabled = false;
                }
                else
                {
                    duration = excess;
                    healthManager.damageModifier = 1.25f;
                    if (child == null)
                    {
                        child = Instantiate(buffIcon);
                        child.transform.SetParent(buffDebuffPannel.transform);
                        child.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                        durationText = child.GetComponentInChildren<TextMeshProUGUI>();
                    }
                }
            }
            else
            {
                duration = turns;
                healthManager.damageModifier = 1.25f;
                if (child == null)
                {
                    child = Instantiate(buffIcon);
                    child.transform.SetParent(buffDebuffPannel.transform);
                    child.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                    durationText = child.GetComponentInChildren<TextMeshProUGUI>();
                }
            }

        }
    }
    public void DebuffCheck()
    {
        if (duration <= 0)
        {
            duration = 0;

            if (GameObject.ReferenceEquals(attached, GameObject.FindGameObjectWithTag("PlayerManager")))
            {
                PlayerManager.damageMultiplier = 1;
                enabled = false;
                Destroy(child);
            }
            else
            {
                HealthManager healthManager = attached.GetComponent<HealthManager>();
                healthManager.damageModifier = 1;
                enabled = false;
                Destroy(child);
            }
        }
    }
    public void Update()
    {
        if (durationText != null)
        {
            durationText.text = "" + duration;
        }
        
    }

    public void OnCombatEnd()
    {
        duration = 0;
        DebuffCheck();
    }
}
