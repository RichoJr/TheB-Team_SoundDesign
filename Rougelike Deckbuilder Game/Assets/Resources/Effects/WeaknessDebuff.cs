using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class WeaknessDebuff : MonoBehaviour
{
    public int duration;
    public GameObject attached;
    private StrengthBuff strengthBuff;
    public ImmunityBuff immunityBuff;
    public GameObject debuffIcon;
    private TextMeshProUGUI durationText;
    GameObject child;

    private void Start()
    {
        attached = this.gameObject;
        strengthBuff = GetComponent<StrengthBuff>();
        immunityBuff = GetComponent<ImmunityBuff>();
        debuffIcon = Resources.Load("effects/WeaknessDebuffIcon Variant") as GameObject;
        //durationText = debuffIcon.GetComponentInChildren<TextMeshProUGUI>();
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
    public void Startbuff(int turns)
    {
        if (GameObject.ReferenceEquals(attached, GameObject.FindGameObjectWithTag("PlayerManager")))
        {
            Debug.Log("is on playermanager");
            if (immunityBuff.enabled == true)
            {
                immunityBuff.stacks -= 1;
                immunityBuff.DebuffCheck();
            }
            else if (strengthBuff.enabled == true)
            {
                int excess = turns - strengthBuff.duration;
                strengthBuff.duration -= turns;
                strengthBuff.DebuffCheck();
                if (excess <= 0)
                {
                    enabled = false;
                }
                else
                {
                    duration = excess;
                    PlayerManager.damageMultiplier = 0.75f;
                    if (child == null)
                    {
                        child = Instantiate(debuffIcon);
                        child.transform.SetParent(PlayerManager.playerEffectList.transform);
                        child.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                        durationText = child.GetComponentInChildren<TextMeshProUGUI>();
                    }
                }
            }
            else
            {
                duration = turns;
                PlayerManager.damageMultiplier = 0.75f;
                if (child == null)
                {
                    child = Instantiate(debuffIcon);
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
            if (immunityBuff.enabled == true)
            {
                immunityBuff.stacks -= 1;
                immunityBuff.DebuffCheck();
            }
            else if (strengthBuff.enabled == true)
            {
                int excess = turns - strengthBuff.duration;
                strengthBuff.duration -= turns;
                strengthBuff.DebuffCheck();
                if (excess <= 0)
                {
                    enabled = false;
                }
                else
                {
                    duration = excess;
                    healthManager.damageModifier = 0.75f;
                    if (child == null)
                    {
                        child = Instantiate(debuffIcon);
                        child.transform.SetParent(buffDebuffPannel.transform);
                        child.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                        durationText = child.GetComponentInChildren<TextMeshProUGUI>();
                    }
                }
                
            }
            else
            {
                duration = turns;
                healthManager.damageModifier = 0.75f;
                if (child == null)
                {
                    child = Instantiate(debuffIcon);
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
