using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScore : MonoBehaviour {
    [SerializeField]
    private Text label;

    public void UpdateScore(int score) {
        this.label.text = score.ToString();
    }
    
}
