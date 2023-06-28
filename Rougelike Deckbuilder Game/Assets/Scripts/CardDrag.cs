using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class CardDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public float arrowHeadSize;
    public GameObject arrow;
    public LineRenderer arrowLine;
    public string cardTargeting;
    public Vector2 startPosition;
    public Vector2 screenPosition;
    public Vector2 mousePosition;
    public ThisCard thisCard;
    public TargetingManager targetingManager;


    public void Start()
    {
        arrowLine = arrow.GetComponentInChildren<LineRenderer>();
        arrow.SetActive(false);
        startPosition = new Vector2(arrow.transform.position.x, arrow.transform.position.y);
        targetingManager = FindObjectOfType<TargetingManager>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("OnBeginDrag");
        thisCard = GetComponent<ThisCard>();
        cardTargeting = thisCard.cardTargetingType;
        targetingManager.cardInUse = gameObject;

        //Debug.Log(cardTargeting);
        arrow.SetActive(true);
        if (cardTargeting == "Self")
        {
            Debug.Log("targeting self");
            targetingManager.targetingSelf = true;
        }
        else if (cardTargeting == "Single")
        {
            Debug.Log("targeting enemy");
            targetingManager.targetingEnemy = true;
        }
        else if (cardTargeting == "All")
        {
            Debug.Log("targeting all");
            targetingManager.targetingAll = true;
        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        mousePosition = Camera.main.ScreenToWorldPoint(eventData.position);
        startPosition = new Vector2(arrow.transform.position.x, arrow.transform.position.y);

        arrowLine.SetPosition(0, startPosition);
        arrowLine.SetPosition(1, mousePosition);

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        arrow.SetActive(false);
        targetingManager.targetingAll = false;
        targetingManager.targetingSelf = false;
        targetingManager.targetingEnemy = false;
        targetingManager.cardInUse = null;
    }


}
