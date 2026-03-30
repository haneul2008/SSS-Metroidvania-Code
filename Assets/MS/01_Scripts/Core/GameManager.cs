using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private SoundSo _bgmSO;

    private Player _player;
    public List<string> RecoveryItems { get; private set; } = new List<string>();

    public Player Player 
    { 
        get 
        { 
            return _player; 
        } 
    }

    public void Awake()
    {
        _player = FindAnyObjectByType<Player>();
    }

    private void Start()
    {
        SoundManager.Instance.PlaySound(_bgmSO);
    }

    public void LoadPlayerPosition(Vector3 position)
    {
        if (_player == null) return;

        _player.transform.position = position;
        _player.GetCompo<PlayerMovement>().Rigid.linearVelocity = Vector2.zero;
    }

    public void CollectRecoveryItem(string key)
    {
        RecoveryItems.Add(key);
    }
}
