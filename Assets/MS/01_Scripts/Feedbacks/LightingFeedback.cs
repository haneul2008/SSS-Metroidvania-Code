using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightingFeedback : Feedback
{
    [SerializeField] private List<Light2D> _lights = new List<Light2D>();
    private List<Light2D> _lightObjs = new List<Light2D>();
    [SerializeField] private Transform _parent;
    [SerializeField] private float _lifeTime = 0.1f;

    public override void PlayFeedback()
    {

        if (_lights.Count <= 0) return;
        foreach (var item in _lights)
        {
            Light2D light = Instantiate(item, _parent);
            Debug.Log(light.name + " 이새끼 살아남");
            _lightObjs.Add(light);
        }

        StartCoroutine(DestroyLight());
    }

    private IEnumerator DestroyLight()
    {
        yield return new WaitForSeconds(_lifeTime);

        foreach (var item in _lightObjs)
        {
            Debug.Log(item.name + " 이새끼 뒤졌음");
            Destroy(item);
        }
        _lightObjs.Clear();
    }

    public override void StopFeedback()
    {

    }
}