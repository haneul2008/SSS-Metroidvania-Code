using UnityEngine;
using UnityEngine.Events;

public class SpinOrbObject : SpinnableObject
{
    public UnityEvent OnAttack;

    [SerializeField] private Transform _particlePivotTrm;

    private DamageCaster _damageCaster;
    private SpinOrbDataSO _spinOrbData;

    public void SetUp(Item item, Transform centerTrm, int orbCnt, int maxCnt)
    {
        base.SetUp(item, centerTrm);

        _damageCaster = GetComponentInChildren<DamageCaster>();
        _damageCaster.Initialize(item.Player);
        _spinOrbData = item.ItemData as SpinOrbDataSO;

        float radian = Mathf.Deg2Rad * (_degree + orbCnt * (360f / maxCnt));
        float x;
        float y;

        x = Mathf.Cos(radian) * _radius;
        y = Mathf.Sin(radian) * _radius;

        transform.position = new Vector3(x, y);

        _degree = radian * Mathf.Rad2Deg;
    }

    public override void FixedUpdate()
    {
        if (_targetTrm == null) return;

        base.FixedUpdate();

        Vector2 dir = _targetTrm.position - transform.position;
        float z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        _particlePivotTrm.eulerAngles = new Vector3(0, 0, z + 180);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if(_damageCaster.CastDamage(_spinOrbData.damage, 0, false))
        {
            OnAttack?.Invoke();
        }
    }
}
