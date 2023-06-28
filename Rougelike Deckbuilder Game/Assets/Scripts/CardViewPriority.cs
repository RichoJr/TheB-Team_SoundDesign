using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardViewPriority : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Canvas tempCanvas;
    private GraphicRaycaster tempRaycaster;

    //private bool dragging = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
        {
            tempCanvas = gameObject.AddComponent<Canvas>();
            tempCanvas.overrideSorting = true;
            tempCanvas.sortingOrder = 1;
            tempRaycaster = gameObject.AddComponent<GraphicRaycaster>();
        }
        
    }


    public void OnPointerExit(PointerEventData eventData)
    {
            Destroy(tempRaycaster);
            Destroy(tempCanvas);
    }
}
