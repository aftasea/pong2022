using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinnerMessage : MonoBehaviour
{
    [SerializeField]
    private RectTransform rectTransform;
    [SerializeField]
    private float distanceFromCenter;

    public void Hide() {
        gameObject.SetActive(false);
    }

    public void Show(Score score) {
        Vector3 pos = rectTransform.localPosition;
        pos.x = score.LeftScore > score.RightScore ? -this.distanceFromCenter : this.distanceFromCenter;
        rectTransform.localPosition = pos;
        gameObject.SetActive(true);
    }
    
}
