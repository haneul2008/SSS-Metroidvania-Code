using System.Collections;
using UnityEngine;

public class BlinkFeedback : Feedback
{
    [SerializeField] private SpriteRenderer _targetRenderer;
    [SerializeField] private float _blinkTime = 0.1f;

    private Material _targetMat;
    private Coroutine _blinkCoroutine;

    private readonly int _isHitHash = Shader.PropertyToID("_IsHit");

    private void Awake()
    {
        _targetMat = _targetRenderer.material;
    }

    public override void PlayFeedback()
    {
        _targetMat.SetInt(_isHitHash, 1);
        _blinkCoroutine = StartCoroutine(DelayBlink());
    }

    private IEnumerator DelayBlink()
    {
        yield return new WaitForSeconds(_blinkTime);
        _targetMat.SetInt(_isHitHash, 0);
    }

    public override void StopFeedback()
    {
        StopAllCoroutines();
        _targetMat.SetInt(_isHitHash, 0);
    }

    private void OnDisable()
    {
        if (_blinkCoroutine != null) 
            StopCoroutine(_blinkCoroutine);
    }
}
