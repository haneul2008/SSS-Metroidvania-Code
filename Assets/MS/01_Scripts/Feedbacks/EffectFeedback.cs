using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EffectFeedback : Feedback
{
    [SerializeField] private List<ParticleSystem> _particleSystem;
    [SerializeField] private Transform _parent;

    public override void PlayFeedback()
    {
        Debug.Log(_parent.name + " ¿Ã∆Â∆Æ πﬂµø");
        if(_particleSystem.Count <= 0) return;
        foreach (var item in _particleSystem)
        {
            Instantiate(item, _parent);
        }
    }

    public override void StopFeedback()
    {

    }
}
