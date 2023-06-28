using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListInspector : MonoBehaviour
{
    public GameObject inspectorPannel;
    public GameObject inspectorCanvas;
    public List<Card> cardsInList = new List<Card>();
    public GameObject inspectorCard;
    public int cardsPreviewed;
    public List<Card> container = new List<Card>();

    // Start is called before the first frame update
    void Start()
    {
        inspectorCanvas.SetActive(false);
    }

    public void SendList(int listNumber)
    {
        switch (listNumber)
        {
            case 0:
                break;
            case 1:
                InspectList(MainDeck.staticMainDeck, false);
                break;
            case 2:
                InspectList(PlayerDeck.staticDeck, true);
                break;
            case 3:
                InspectList(PlayerDeck.staticDiscard, false);
                break;
            case 4:
                InspectList(PlayerDeck.staticBanish, false);
                break;
        }
    }

    public void InspectList(List<Card> list, bool shuffle)
    {
        GameObject[] inspectedCards;
        inspectedCards = GameObject.FindGameObjectsWithTag("Inspected");
        foreach (GameObject card in inspectedCards)
        {
            Destroy(card);
        }
        inspectorCanvas.SetActive(true);
        cardsInList.Clear();
        cardsInList.AddRange(list);
        cardsPreviewed = cardsInList.Count;
        if (shuffle == true)
        {
            Shuffle();
        }
        for(int i = 0; i < cardsPreviewed; i++)
        {
            GameObject previewCard = Instantiate(inspectorCard);
            previewCard.transform.SetParent(inspectorPannel.transform);
            previewCard.transform.localScale = new Vector3(1.15f, 1.15f, 1.15f);
            previewCard.GetComponent<ThisCard>().thisCard[0] = cardsInList[i];
            previewCard.tag = "Inspected";
        }
    }

    public void Shuffle()
    {
        for (int i = 0; i < cardsPreviewed; i++)
        {
            container[0] = cardsInList[i];
            int randomIndex = Random.Range(i, cardsInList.Count);
            cardsInList[i] = cardsInList[randomIndex];
            cardsInList[randomIndex] = container[0];
        }
    }

    public void CloseInspector()
    {
        GameObject[] inspectedCards;
        inspectedCards = GameObject.FindGameObjectsWithTag("Inspected");
        foreach (GameObject card in inspectedCards)
        {
            Destroy(card);
        }
        cardsInList.Clear();
        inspectorCanvas.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        cardsPreviewed = cardsInList.Count;
    }
}
