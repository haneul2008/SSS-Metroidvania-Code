using UnityEngine;

public class SoundFeedback : Feedback
{
    [SerializeField] private SoundSo _soundSO;

    public override void PlayFeedback()
    {
        SoundManager.Instance.PlaySound(_soundSO);
    }

    public override void StopFeedback()
    {

    }
}
