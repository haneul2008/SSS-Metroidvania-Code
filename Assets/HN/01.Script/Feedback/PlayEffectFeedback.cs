using System;
using System.Collections;
using UnityEngine;

public class PlayEffectFeedback : Feedback
{
    [SerializeField] private string _effectName;

    public override void PlayFeedback()
    {
        Effect effect = PoolManager.Instance.Pop(_effectName) as Effect;
        effect.PlayEffect(transform.position);
    }

    public override void StopFeedback()
    {

    }
}
