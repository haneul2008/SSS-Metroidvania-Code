using DG.Tweening;
using UnityEngine;
using Unity.Cinemachine;
using System.Collections;
using UnityEngine.Events;

public class CameraZoomFeedback : Feedback
{
    public UnityEvent OnZoomStartEvent;

    [SerializeField] private CinemachineCamera _cam;
    [SerializeField] private float _endSize;
    [SerializeField] private float _duration;
    [SerializeField] private float _startDelay;
    [SerializeField] private Ease _ease;

    private Coroutine _startCoroutine;
    private Tween _tween;
    private float _originSize;

    private void Awake()
    {
        if (_cam == null)
        {
            _cam = FindFirstObjectByType<CinemachineCamera>();
        }

        _originSize = _cam.Lens.FieldOfView;
    }

    public override void PlayFeedback()
    {
        _startCoroutine = StartCoroutine(ZoomStartCoroutine());
    }

    public override void StopFeedback()
    {

    }

    private IEnumerator ZoomStartCoroutine()
    {
        yield return new WaitForSeconds(_startDelay);

        _tween = DOTween.To(() => _cam.Lens.FieldOfView, size => _cam.Lens.FieldOfView = size, _endSize, _duration)
            .SetEase(_ease)
            .OnComplete(() => _cam.Lens.FieldOfView = _originSize);

        OnZoomStartEvent?.Invoke();
    }

    private void OnDisable()
    {
        if (_startCoroutine != null)
            StopCoroutine(_startCoroutine);

        if (_tween != null)
            _tween.Complete();
    }
}
