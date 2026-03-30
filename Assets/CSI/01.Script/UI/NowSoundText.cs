using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NowSoundText : MonoBehaviour
{
    private TextMeshProUGUI _text;
    [SerializeField] private Slider _slider;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (Mathf.Approximately(_slider.value, 1))
        {
            _text.text = "100";
            return;
        }
        if (_slider.value <= 0.001f)
        {
            _text.text = "0";
            return;
        }
        _text.text = (_slider.value*100).ToString("F1");
    }
}
