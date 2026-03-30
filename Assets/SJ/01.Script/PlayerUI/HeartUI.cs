using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class HeartUI : MonoBehaviour
{
    private Stack<Heart> _hearts = new();
    [SerializeField] private Sprite _fullHealth, _emptyhealth;
    [SerializeField] private Heart _heartPrefab;

    private void Start()
    {
        Initialized(5);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            OnDamage();
        }
    }

    private void OnDamage()
    {
        if (_hearts.Count == 0) return;

        Heart heart = _hearts.Pop();
        heart.SetSprite(_emptyhealth);
    }

    public void Initialized(int maxHealth)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < maxHealth; i++)
        {
            Heart heart = Instantiate(_heartPrefab, transform);
            _hearts.Push(heart);
        }
    }
}
