using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PoisonDebuff : MonoBehaviour
{
    public int stacks;
    public GameObject attached;
    public ImmunityBuff immunityBuff;
    public GameObject debuffIcon;
    private TextMeshProUGUI stacksText;
    private PlayerManager playerManager;
    GameObject child;

    private void Start()
    {
        attached = this.gameObject;
        playerManager = FindObjectOfType<PlayerManager>();
        immunityBuff = GetComponent<ImmunityBuff>();
        debuffIcon = Resources.Load("effects/PoisonDebuffIcon Variant") as GameObject;
        enabled = false;
        
    }
    private void OnEnable()
    {
        if (GameObject.ReferenceEquals(attached, GameObject.FindGameObjectWithTag("PlayerManager")))
        {
            TurnSystem.TurnStart += DebuffTick;
        }
        else
        {
            TurnSystem.TurnEnd += DebuffTick;
        }
        TurnSystem.CombatEnd += OnCombatEnd;
        CombatStartAndEnd.RewardSelected += OnCombatEnd;
    }
    private void OnDisable()
    {
        if (GameObject.ReferenceEquals(attached, GameObject.FindGameObjectWithTag("PlayerManager")))
        {
            TurnSystem.TurnStart -= DebuffTick;
        }
        else
        {
            TurnSystem.TurnEnd -= DebuffTick;
        }
        TurnSystem.CombatEnd -= OnCombatEnd;
        CombatStartAndEnd.RewardSelected -= OnCombatEnd;
    }

    public void DebuffTick()
    {
        if (GameObject.ReferenceEquals(attached, GameObject.FindGameObjectWithTag("PlayerManager")))
        {
            playerManager.PlayerDamaged(stacks, null);
        }
        else
        {
            HealthManager healthManager = attached.GetComponent<HealthManager>();
            healthManager.UnitDamaged(stacks, true);
        }
        stacks -= 1;
        if (stacks <= 0)
        {
            stacks = 0;

            if (GameObject.ReferenceEquals(attached, GameObject.FindGameObjectWithTag("PlayerManager")))
            {
                enabled = false;
                Destroy(child);
            }
            else
            {
                //HealthManager healthManager = attached.GetComponent<HealthManager>();
                enabled = false;
                Destroy(child);
            }
        }
    }
    public void Startbuff(int stacks)
    {
        if (GameObject.ReferenceEquals(attached, GameObject.FindGameObjectWithTag("PlayerManager")))
        {
            Debug.Log("is on playermanager");
            if (immunityBuff.enabled == true)
            {
                immunityBuff.stacks -= 1;
                immunityBuff.DebuffCheck();
            }
            else
            {
                this.stacks += stacks;
                if (child == null)
                {
                    child = Instantiate(debuffIcon);
                    child.transform.SetParent(PlayerManager.playerEffectList.transform);
                    child.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                    stacksText = child.GetComponentInChildren<TextMeshProUGUI>();
                }
            }
        }
        else
        {
            //HealthManager healthManager = attached.GetComponent<HealthManager>();
            GameObject buffDebuffPannel = attached.GetComponentInChildren<HorizontalLayoutGroup>().gameObject;
            if (immunityBuff.enabled == true)
            {
                immunityBuff.stacks -= 1;
                immunityBuff.DebuffCheck();
            }
            else
            {
                this.stacks += stacks;
                if (child == null)
                {
                    child = Instantiate(debuffIcon);
                    child.transform.SetParent(buffDebuffPannel.transform);
                    child.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                    stacksText = child.GetComponentInChildren<TextMeshProUGUI>();
                }
            }
            
        }
        
    }
    public void DebuffCheck()
    {
        if (stacks <= 0)
        {
            stacks = 0;

            if (GameObject.ReferenceEquals(attached, GameObject.FindGameObjectWithTag("PlayerManager")))
            {
                enabled = false;
                Destroy(child);
            }
            else
            {
                //HealthManager healthManager = attached.GetComponent<HealthManager>();
                enabled = false;
                Destroy(child);
            }
        }
    }
    public void Update()
    {
        if (stacksText != null)
        {
            stacksText.text = "" + stacks;
        }
    }

    public void OnCombatEnd()
    {
        stacks = 0;
        DebuffCheck();
    }
}
