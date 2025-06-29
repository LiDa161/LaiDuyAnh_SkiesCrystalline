using DG.Tweening;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public RectTransform logo;
    public float logoDropY = 0f;
    public float logoDropDuration = 0.8f;

    public CanvasGroup[] buttons;
    public float buttonFadeDelay = 0.2f;
    public float buttonFadeDuration = 0.4f;

    private Vector2 logoStartPos;

    void OnEnable()
    {
        AnimateIntro();
    }

    public void AnimateIntro()
    {
        if (logo == null) return;

        logoStartPos = logo.anchoredPosition;

        logo.anchoredPosition = new Vector2(logoStartPos.x, logoStartPos.y + 500);

        logo.DOAnchorPosY(logoStartPos.y, logoDropDuration)
            .SetEase(Ease.OutBounce);

        for (int i = 0; i < buttons.Length; i++)
        {
            CanvasGroup btn = buttons[i];
            btn.alpha = 0f;
            btn.gameObject.SetActive(true);

            btn.DOFade(1f, buttonFadeDuration)
                .SetEase(Ease.OutQuad)
                .SetDelay(logoDropDuration + i * buttonFadeDelay);
        }
    }
}
