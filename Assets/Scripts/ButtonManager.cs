using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float scaleUp = 1.1f;
    public float duration = 0.2f;

    public float shakeInterval = 2f;
    public float shakeStrength = 10f;
    public float shakeDuration = 0.2f;

    private Vector3 originalScale;
    private Tween scaleTween;
    private Tween shakeTween;

    void Start()
    {
        originalScale = transform.localScale;
        InvokeRepeating(nameof(ShakeButton), shakeInterval, shakeInterval);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        KillTweens();

        scaleTween = transform
            .DOScale(originalScale * scaleUp, duration)
            .SetEase(Ease.OutBack);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        KillTweens();

        scaleTween = transform
            .DOScale(originalScale, duration)
            .SetEase(Ease.OutBack);
    }

    void ShakeButton()
    {
        if (!IsPointerOver())
        {
            if (shakeTween != null && shakeTween.IsActive()) shakeTween.Kill();
            shakeTween = transform.DOShakeRotation(shakeDuration, new Vector3(0, 0, shakeStrength), 10, 90)
                                   .SetEase(Ease.Linear);
        }
    }

    void KillTweens()
    {
        if (scaleTween != null && scaleTween.IsActive()) scaleTween.Kill();
        if (shakeTween != null && shakeTween.IsActive()) shakeTween.Kill();
    }

    bool IsPointerOver()
    {
        return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
    }
}
