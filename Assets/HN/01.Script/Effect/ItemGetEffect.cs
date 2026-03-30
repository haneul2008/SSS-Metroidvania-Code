using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class ItemGetEffect : Effect
{
    [SerializeField] private Vector2 _endSize;

    private Vector2 _startPosition;
    private Vector2 _controlPoint1;
    private Vector2 _controlPoint2;
    private Vector2 _targetTrm;
    private float _duration;
    private float _timeElapsed;
    private List<Tween> _sizeTweens;
    private bool _isEnd;

    protected override void Awake()
    {
        base.Awake();

        _sizeTweens = new List<Tween>(transform.childCount);
    }

    public void Initialize(Vector2 start, Vector2 cp1, Vector2 cp2, Vector2 target, float duration)
    {
        _startPosition = start;

        _controlPoint1 = cp1;
        _controlPoint2 = cp2;

        _targetTrm = target;

        _duration = duration;

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform childTrm = transform.GetChild(i);
            _sizeTweens.Add(childTrm.DOScale(_endSize, _duration));
        }

        _timeElapsed = 0f;
    }

    private void Update()
    {
        if (_timeElapsed < _duration)
        {
            _timeElapsed += Time.deltaTime;
            float t = _timeElapsed / _duration;

            Vector3 position = Mathf.Pow(1 - t, 3) * _startPosition +
            3 * Mathf.Pow(1 - t, 2) * t * _controlPoint1 +
                               3 * (1 - t) * Mathf.Pow(t, 2) * _controlPoint2 +
                               Mathf.Pow(t, 3) * _targetTrm;

            transform.position = position;
        }
        else if (!_isEnd)
        {
            _isEnd = true;
            _pushCoroutine = StartCoroutine(PushCoroutine(new WaitForSeconds(0.5f)));
        }
    }

    public override void ResetItem()
    {
        base.ResetItem();

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform childTrm = transform.GetChild(i);
            childTrm.localScale = Vector3.one;
        }

        _isEnd = false;
    }

    private void OnDisable()
    {
        foreach (Tween tween in _sizeTweens)
        {
            if (tween != null)
                tween.Kill();
        }
    }
}
