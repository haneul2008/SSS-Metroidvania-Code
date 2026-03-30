using DG.Tweening;
using UnityEngine;

public class DistotionEffect : Effect
{
    private Tween _sizeTween;

    public void PlayEffect(Vector2 pos, float duration, Vector3 endSize)
    {
        _ws = new WaitForSeconds(duration);

        base.PlayEffect(pos);

        _sizeTween = transform.DOScale(endSize, duration);
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        if (_sizeTween != null)
            _sizeTween.Kill();
    }

    public override void ResetItem()
    {
        base.ResetItem();

        transform.localScale = Vector3.zero;
    }
}
