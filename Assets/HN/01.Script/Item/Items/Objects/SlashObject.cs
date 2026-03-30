using DG.Tweening;
using UnityEngine;

public class SlashObject : EffectObject
{
    private DamageCaster _damageCaster;
    private bool _isInitialize;
    private Tween _tween;

    protected override void Awake()
    {
        base.Awake();

        _damageCaster = GetComponentInChildren<DamageCaster>();
    }

    public void SetUp(Item item, Vector2 pos, int damage, float startDelay)
    {
        _tween = DOVirtual.DelayedCall(startDelay, () =>
        {
            base.SetUp(item, pos, damage);

            if (!_isInitialize)
            {
                _damageCaster.Initialize(item.Player);
                _isInitialize = true;
            }

            _damageCaster.CastDamage(_damage, 0, false);
        });
    }

    private void OnDisable()
    {
        if (_tween != null)
            _tween.Kill();
    }
}
