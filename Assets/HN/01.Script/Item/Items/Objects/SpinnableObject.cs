using UnityEngine;

public class SpinnableObject : ItemObject
{
    [SerializeField] protected float _radius;
    [SerializeField] protected float _speed = 1.5f;

    protected Transform _targetTrm;
    protected float _degree;

    public virtual void SetUp(Item item, Transform centerTrm)
    {
        base.SetUp(item);

        _targetTrm = centerTrm;
    }

    public virtual void FixedUpdate()
    {
        if (_targetTrm == null) return;

        _degree += Time.fixedDeltaTime + _speed;

        if(_degree > 360)
            _degree = 0;

        float radian = Mathf.Deg2Rad * _degree;

        float cos = Mathf.Cos(radian) * _radius;
        float sin = Mathf.Sin(radian) * _radius;

        transform.position = _targetTrm.position + new Vector3(cos, sin, 0);
    }
}
