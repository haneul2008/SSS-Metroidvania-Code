using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class IntensityFeedback : Feedback
{
    [SerializeField] private Light2D _targetLight;
    [SerializeField] private float _endIntensity;
    [SerializeField] private float _playDuration;
    [SerializeField] private float _endDuration;

    private float _originIntensity;
    private Tween _tween;

    private void Awake()
    {
        _originIntensity = _targetLight.intensity;
    }

    public override void PlayFeedback()
    {
        _tween = DOTween.To(() => _targetLight.intensity, intensity => _targetLight.intensity = intensity, _endIntensity, _playDuration)
            .OnComplete(() =>
            {
                _tween = DOTween.To(() => _targetLight.intensity, intensity => _targetLight.intensity = intensity, _originIntensity, _endDuration);
            });
    }

    public override void StopFeedback()
    {

    }

    private void OnDisable()
    {
        if(_tween != null) 
            _tween.Complete();
    }
}
