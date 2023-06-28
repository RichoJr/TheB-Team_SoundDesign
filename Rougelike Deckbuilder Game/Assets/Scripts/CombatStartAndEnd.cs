using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CombatStartAndEnd : MonoBehaviour
{
    public List<Card> rewardList = new List<Card>();

    public delegate void OnCombatStart();
    public static event OnCombatStart CombatStart;

    public ThisEncounter thisEncounter;
    public GameObject overworldUi;
    public GameObject overworldBackground;
    public GameObject rewardsPrompt;
    public GameObject rewardsPanel;
    public GameObject cardSelector;
    public TextMeshProUGUI confirmCardSelectionText;

    public GameObject newCardButton;
    public GameObject card;
    public MainDeck mainDeck;

    public static GameObject lastCardSelected;

    public delegate void OnRewardSelected();
    public static event OnRewardSelected RewardSelected;

    // Start is called before the first frame update
    void Start()
    {
        thisEncounter = gameObject.GetComponent<ThisEncounter>();
        mainDeck = FindObjectOfType<MainDeck>();
        confirmCardSelectionText.text = "Skip";
        
        rewardsPrompt.SetActive(false);
        cardSelector.SetActive(false);
    }

    private void OnEnable()
    {
        TurnSystem.CombatEnd += EndCombat;
    }

    private void OnDisable()
    {
        TurnSystem.CombatEnd += EndCombat;
    }

    public void StartCombat(int encounterId)
    {
        thisEncounter.CallEncounter(encounterId);
        List<string> enemyList = thisEncounter.enemyList;
        foreach (string enemy in enemyList)
        {
            GameObject enemyPrefab= Resources.Load<GameObject>("Enemies/" + enemy);
            GameObject childEnemy = Instantiate(enemyPrefab);
            childEnemy.transform.SetParent(thisEncounter.enemyZone.transform);
            childEnemy.transform.localScale = new Vector3(1.660617f, 1.660617f, 1.660617f);
        }
        overworldUi.SetActive(false);
        CombatStart();
    }

    public void EndCombat()
    {
        GameObject[] tempObjects;
        tempObjects = GameObject.FindGameObjectsWithTag("TemporaryObject");
        foreach (GameObject obj in tempObjects)
        {
            Destroy(obj);
        }
        overworldUi.SetActive(true);
        overworldBackground.SetActive(false);
        rewardsPrompt.SetActive(true);
        GameObject button = Instantiate(newCardButton);
        button.transform.SetParent(rewardsPanel.transform);
        button.transform.localScale = new Vector3(1, 1, 1);
        button.GetComponent<Button>().onClick.AddListener(CardRewardSelect);
    }

    public void CardRewardSelect()
    {
        cardSelector.SetActive(true);
        lastCardSelected = null;
        //List<Card> rewardList = new List<Card>();
        rewardList.Add(CardDatabase.cardList[Random.Range(7, 18)]);
        rewardList.Add(CardDatabase.cardList[Random.Range(7, 18)]);
        rewardList.Add(CardDatabase.cardList[Random.Range(7, 18)]);
        for(int i = 0; i < 3; i++)
        {
            Debug.Log(i);
            GameObject rewardCard = Instantiate(card, transform.position, transform.rotation); ;
            rewardCard.transform.SetParent(cardSelector.transform);
            rewardCard.transform.localScale = new Vector3(1.75f, 1.75f, 1.75f);
            ThisCard thisCard = rewardCard.GetComponent<ThisCard>();
            thisCard.thisCard[0] = rewardList[i];
            thisCard.GetComponentInChildren<Button>().onClick.AddListener(delegate { SetLastCard(rewardCard); });
        }

    }

    public void SetLastCard(GameObject lastSelectedCard)
    {
        if (lastSelectedCard == lastCardSelected)
        {
            lastCardSelected = null;
            confirmCardSelectionText.text = "Skip";
        }
        else
        {
            lastCardSelected = lastSelectedCard;
            confirmCardSelectionText.text = "Confirm";
        }
        
    }

    public void CardSelectionConfirm()
    {
        if (lastCardSelected != null)
        {
            ThisCard thisCard = lastCardSelected.GetComponent<ThisCard>();
            mainDeck.AddCardToMain(thisCard.thisCard);
            lastCardSelected = null;
            confirmCardSelectionText.text = "Skip";
            GameObject[] possibleCards;
            possibleCards = GameObject.FindGameObjectsWithTag("Preview");
            foreach (GameObject possibleCard in possibleCards)
            {
                Destroy(possibleCard);
            }
            cardSelector.SetActive(false);
            rewardList.Clear();
            GameObject firstReward = rewardsPanel.transform.GetChild(0).gameObject;
            Destroy(firstReward);
        }
        else
        {
            lastCardSelected = null;
            confirmCardSelectionText.text = "Skip";
            GameObject[] possibleCards;
            possibleCards = GameObject.FindGameObjectsWithTag("Preview");
            foreach (GameObject possibleCard in possibleCards)
            {
                Destroy(possibleCard);
            }
            cardSelector.SetActive(false);
            rewardList.Clear();
            GameObject firstReward = rewardsPanel.transform.GetChild(0).gameObject;
            Destroy(firstReward);
        }
    }

    public void RewardSelectionComplete()
    {
        if (rewardsPanel.transform.childCount > 0)
        {
            foreach(Transform child in rewardsPanel.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
        //rewardsPanel.transform.GetChild
        rewardsPrompt.SetActive(false);
        overworldBackground.SetActive(true);
        if (RewardSelected != null)
        {
            RewardSelected();
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
