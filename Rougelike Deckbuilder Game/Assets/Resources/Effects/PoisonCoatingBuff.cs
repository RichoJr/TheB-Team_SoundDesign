using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PoisonCoatingBuff : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject attached;
    public int stacks;
    public GameObject buffIcon;
    private TextMeshProUGUI stacksText;
    GameObject child;
    public bool innate;

    private void Start()
    {
        attached = this.gameObject;
        buffIcon = Resources.Load("effects/PoisonCoatingBuffIcon Variant") as GameObject;
        if (innate == true)
        {
            enabled = true;
            Startbuff(2);
        }
        else
        {
            enabled = false;
        }
    }
    private void OnEnable()
    {
        if (GameObject.ReferenceEquals(attached, GameObject.FindGameObjectWithTag("PlayerManager")))
        {
            TurnSystem.TurnEnd += DebuffTick;
            HealthManager.EnemyDamaged += EffectTriggered;
        }
        else
        {
            TurnSystem.TurnStart += DebuffTick;
            PlayerManager.PlayerHealthDamage += EffectTriggered;
        }
        TurnSystem.CombatEnd += OnCombatEnd;
        CombatStartAndEnd.RewardSelected += OnCombatEnd;
    }
    private void OnDisable()
    {
        if (GameObject.ReferenceEquals(attached, GameObject.FindGameObjectWithTag("PlayerManager")))
        {
            TurnSystem.TurnEnd -= DebuffTick;
            HealthManager.EnemyDamaged -= EffectTriggered;
        }
        else
        {
            TurnSystem.TurnStart -= DebuffTick;
            PlayerManager.PlayerHealthDamage -= EffectTriggered;
        }
        TurnSystem.CombatEnd -= OnCombatEnd;
        CombatStartAndEnd.RewardSelected -= OnCombatEnd;
    }

    public void DebuffTick()
    {
        if (stacks <= 0)
        {
            stacks = 0;

            if (GameObject.ReferenceEquals(attached, GameObject.FindGameObjectWithTag("PlayerManager"))) //(attached == GameObject.FindGameObjectWithTag("PlayerManager"))
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

            this.stacks += stacks;
            if (child == null)
            {
                child = Instantiate(buffIcon);
                child.transform.SetParent(PlayerManager.playerEffectList.transform);
                child.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                stacksText = child.GetComponentInChildren<TextMeshProUGUI>();
            }
        }
        else
        {
            //HealthManager healthManager = attached.GetComponent<HealthManager>();
            GameObject buffDebuffPannel = attached.GetComponentInChildren<HorizontalLayoutGroup>().gameObject;
            this.stacks += stacks;
            if (child == null)
            {
                child = Instantiate(buffIcon);
                child.transform.SetParent(buffDebuffPannel.transform);
                child.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                stacksText = child.GetComponentInChildren<TextMeshProUGUI>();
            }

        }
    }

    public void EffectTriggered(GameObject target, GameObject source)
    {
        if (TurnSystem.isPlayersTurn == false)
        {
            if ((attached.tag == "Enemy") && (target.tag == "Player") && (source == attached))
            {
                PoisonDebuff poisonDebuff = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PoisonDebuff>();
                poisonDebuff.enabled = true;
                poisonDebuff.Startbuff(stacks);
            }
        }
        if (TurnSystem.isPlayersTurn == true)
        {
            if ((GameObject.ReferenceEquals(attached, GameObject.FindGameObjectWithTag("PlayerManager"))) && (target.tag != "Player"))
            {
                PoisonDebuff poisonDebuff = target.GetComponent<PoisonDebuff>();
                poisonDebuff.enabled = true;
                poisonDebuff.Startbuff(stacks);
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
