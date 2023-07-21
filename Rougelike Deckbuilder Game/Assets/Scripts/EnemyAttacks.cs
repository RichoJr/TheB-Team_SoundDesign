using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;
using Image = UnityEngine.UI.Image;

public class EnemyAttacks : MonoBehaviour
{
    public List<int> thisEnemyAttacks;
    public List<EnemyAction> thisEnemyActions = new List<EnemyAction>();
    public int enemyId;

    public int id;
    public string actionName;
    public int damage;
    public int block;
    public int hits;
    public int buffEffect;
    public int debuffEffect;
    public int buffPotency;
    public int debuffPotency;

    public bool uniqueEnemy;
    public List<int> thisEnemyUniqueAttacks;
    public int conditions;

    public TextMeshProUGUI attackNameText;
    public TextMeshProUGUI damageText;

    public Sprite thisSprite;
    public Image thatImage;

    public AudioClip attackSound;
    public AudioSource attackSoundSource;

    public GameObject enemy;
    public GameObject player;
    public Animator enemyAnimator;
    public HealthManager healthManager;
    public PlayerManager playerManager;
    public ApplyEffect applyEffect;
    public SpecialAttackConditions specialAttackConditions;

    public GameObject attackAnim;
    public int attackNumber;


    // Start is called before the first frame update
    private void Awake()
    {
        specialAttackConditions = FindObjectOfType<SpecialAttackConditions>();
    }
    void Start()
    {
        healthManager = GetComponent<HealthManager>();
        enemyAnimator = GetComponent<Animator>();
        playerManager = FindObjectOfType<PlayerManager>();
        applyEffect = FindObjectOfType<ApplyEffect>();
        specialAttackConditions = FindObjectOfType<SpecialAttackConditions>();
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = this.gameObject;
        attackSoundSource = GameObject.Find("Enemy Attack Sound").GetComponent<AudioSource>();
        foreach (int attack in thisEnemyAttacks)
        {
            thisEnemyActions.Add(EnemyActionDatabase.attacklist[attack]);
        }

        int randomAttack = Random.Range(0, (thisEnemyAttacks.Count));
        enemyId = randomAttack;

    }

    // Update is called once per frame
    void Update()
    {
        id = thisEnemyActions[enemyId].id;
        actionName = thisEnemyActions[enemyId].actionName;
        damage = thisEnemyActions[enemyId].damage;
        block = thisEnemyActions[enemyId].block;
        hits = thisEnemyActions[enemyId].hits;
        buffEffect = thisEnemyActions[enemyId].buffEffect;
        debuffEffect = thisEnemyActions[enemyId].debuffEffect;
        buffPotency = thisEnemyActions[enemyId].buffpotency;
        debuffPotency = thisEnemyActions[enemyId].debuffpotency;

        thisSprite = thisEnemyActions[enemyId].thisImage;
        attackSound = thisEnemyActions[enemyId].attackSound;

        attackNameText.text = "" + actionName;
        int actualDamage = (int)(damage * healthManager.damageModifier + 0.5f);
        if (hits == 0 || damage == 0)
        {
            damageText.text = "";
        }
        else if (hits == 1)
        {
            damageText.text = "" + actualDamage;
        }
        else
        {
            damageText.text = actualDamage + "x" + hits;
        }
        

        thatImage.sprite = thisSprite;
    }

    public void EnemyIntent()
    {

        if (uniqueEnemy == true)
        {
            foreach (int condition in thisEnemyUniqueAttacks)
            {
                bool conditionsMet = specialAttackConditions.CheckConditions(condition);
                if (conditionsMet == true)
                {
                    thisEnemyActions.Add(EnemyActionDatabase.attacklist[condition]);
                    enemyId = thisEnemyActions.Count - 1;
                }
                else
                {
                    int randomAttack = Random.Range(0, (thisEnemyAttacks.Count));
                    enemyId = randomAttack;
                }
            }
            thatImage.gameObject.SetActive(true);
            StartCoroutine(IntentAnimation());

        }
        else
        {
            int randomAttack = Random.Range(0, (thisEnemyAttacks.Count));
            enemyId = randomAttack;
            thatImage.gameObject.SetActive(true);
            StartCoroutine(IntentAnimation());
        }
    }

    public void EnemyAttack()
    {
        StartCoroutine(AnimationTimer());
    }
    IEnumerator AnimationTimer()
    {
        if (hits == 0)
        {
            enemyAnimator.SetTrigger("Enemy Buff");
            yield return new WaitForSeconds(0.45f);
            healthManager.BlockGained(block);
            // sound effect
            attackSoundSource.clip = attackSound;
            attackSoundSource.Play();
            applyEffect.ActivateEffect(enemy, buffEffect, buffPotency);
            thatImage.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.59f);
            enemyAnimator.SetTrigger("Attack complete");
        }
        else
        {
            enemyAnimator.SetTrigger("Enemy attack");
            yield return new WaitForSeconds(0.4f);
            playerManager.PlayerDamaged((int)(damage * healthManager.damageModifier + 0.5f), enemy);
            // sound effect
            attackSoundSource.clip = attackSound;
            attackSoundSource.Play();
            StartCoroutine(HitAnimation());
            if (hits > 1)
            {
                for (int i = 0; i < hits - 1; i++)
                {
                    enemyAnimator.SetTrigger("Extra Hit Foreswing");
                    yield return new WaitForSeconds(0.25f);
                    enemyAnimator.SetTrigger("Extra Hit");
                    yield return new WaitForSeconds(0.15f);
                    StartCoroutine(HitAnimation());
                    // sound effect
                    attackSoundSource.clip = attackSound;
                    attackSoundSource.Play();
                    playerManager.PlayerDamaged((int)(damage * healthManager.damageModifier + 0.5f), enemy);
                }
            }
            healthManager.BlockGained(block);
            applyEffect.ActivateEffect(player, debuffEffect, debuffPotency);
            applyEffect.ActivateEffect(enemy, buffEffect, buffPotency);
            enemyAnimator.SetTrigger("Enemy return");
            thatImage.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.59f);
            enemyAnimator.SetTrigger("Attack complete");
        }
    }

    IEnumerator IntentAnimation()
    {
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            // set color with i as alpha
            thatImage.color = new Color(thatImage.color.r, thatImage.color.g, thatImage.color.b, i);
            TextMeshProUGUI[] text;
            text = thatImage.gameObject.GetComponentsInChildren<TextMeshProUGUI>();
            foreach(TextMeshProUGUI textMeshProUGUI in text)
            {
                textMeshProUGUI.color = new Color(textMeshProUGUI.color.r, textMeshProUGUI.color.g, textMeshProUGUI.color.b, i);
            }
            yield return null;
        }
    }

    IEnumerator HitAnimation()
    {
        GameObject hitAnimation = Instantiate(attackAnim);
        hitAnimation.transform.SetParent(player.transform, false);
        yield return new WaitForSeconds(0.25f);
        Destroy(hitAnimation);
    }
}
