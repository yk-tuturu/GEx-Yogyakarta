using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class buttonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData) {
        transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.1f);
    }

    public void OnPointerExit(PointerEventData eventData) {
        transform.DOScale(new Vector3(1f, 1f, 1f), 0.1f);
    }
}
