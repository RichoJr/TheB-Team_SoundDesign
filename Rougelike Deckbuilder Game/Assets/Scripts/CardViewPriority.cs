using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardViewPriority : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Canvas tempCanvas;
    private GraphicRaycaster tempRaycaster;

    public AudioSource cardHover;

    //private bool dragging = false;
    public void Update()
    {
        cardHover = GetComponentInChildren<AudioSource>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
        {
            cardHover.Play();
            tempCanvas = gameObject.AddComponent<Canvas>();
            tempCanvas.overrideSorting = true;
            tempCanvas.sortingOrder = 1;
            tempRaycaster = gameObject.AddComponent<GraphicRaycaster>();
        }
    }


    public void OnPointerExit(PointerEventData eventData)
    {
            cardHover.Play();
            Destroy(tempRaycaster);
            Destroy(tempCanvas);
    }

}

