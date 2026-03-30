using UnityEngine;
using UnityEngine.Events;

public class ShockWaveObject : EffectObject
{
    public UnityEvent OnExplosionEvent;

    private DamageCaster _damageCaster;
    private bool _isInitialize;

    protected override void Awake()
    {
        base.Awake();

        _damageCaster = GetComponentInChildren<DamageCaster>();
    }

    public void SetUp(Item item, Vector2 pos, int damage, float knockbackPower)
    {
        base.SetUp(item, pos, damage);

        if (!_isInitialize)
        {
            _isInitialize = true;
            _damageCaster.Initialize(item.Player);
        }

        _damageCaster.CastDamage(damage, knockbackPower, true);

        OnExplosionEvent?.Invoke();
    }
}
