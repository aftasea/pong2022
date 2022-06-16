using UnityEngine;
using UnityEngine.UI;

public class UIScore : MonoBehaviour
{
    [SerializeField] private Text label;

    public void UpdateScore(short score) {
        label.text = score.ToString();
    }
}
