using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadeUI : MonoBehaviour
{
    [SerializeField] private float _fadeDuration = 1.5f;

    private Image _image;
    private Tween _fadeTween;

    private void Awake()
    {
        _image = GetComponent<Image>();

        Fade(true);
    }

    public void Fade(bool fadeIn)
    {
        float endValue = fadeIn ? 0 : 1;
        _image.color = new Color(1, 1, 1, 1 - endValue);
        _image.DOFade(endValue, _fadeDuration);
    }
}
