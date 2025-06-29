using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Effect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float bounceScale = 1.1f;
    public float hoverScale = 1.1f;
    public float bounceDuration = 0.2f;

    private Vector3 originalScale;
    private Tween bounceTween;

    void Start()
    {
        originalScale = transform.localScale;
        StartBounce();
    }

    void StartBounce()
    {
        if (bounceTween != null && bounceTween.IsActive()) bounceTween.Kill();

        bounceTween = transform
            .DOScale(originalScale * bounceScale, bounceDuration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (bounceTween != null && bounceTween.IsActive()) bounceTween.Kill();

        transform.DOScale(originalScale * hoverScale, 0.15f)
            .SetEase(Ease.OutBack);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(originalScale, 0.15f)
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                StartBounce();
            });
    }
}
