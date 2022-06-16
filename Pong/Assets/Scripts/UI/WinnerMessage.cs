using UnityEngine;

public class WinnerMessage : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private float distanceFromCenter;

    public void Hide() {
        gameObject.SetActive(false);
    }

    public void Show(IScoreProvider scoreProvider) {
        Vector3 pos = rectTransform.localPosition;
        pos.x = scoreProvider.GetLeftScore() > scoreProvider.GetRightScore() ? -distanceFromCenter : distanceFromCenter;
        rectTransform.localPosition = pos;
        gameObject.SetActive(true);
    }
}
