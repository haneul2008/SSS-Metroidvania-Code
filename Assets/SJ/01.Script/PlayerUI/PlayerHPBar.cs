using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPBar : MonoSingleton<PlayerHPBar>
{
    [SerializeField] private Player _player;
    [SerializeField] private Image fillImage;

    private Health _playerHealth;

    private void Start()
    {
        _playerHealth = _player.GetCompo<Health>();

        _player.OnDeadEvent += isDead => SetFillAmount();
        _playerHealth.OnDamagedEvent.AddListener(SetFillAmount);
        _playerHealth.OnRecoveryEvent += SetFillAmount;

        Invoke("SetFillAmount", 0.1f);
    }

    private void OnDisable()
    {
        _playerHealth.OnDamagedEvent.RemoveListener(SetFillAmount);
        _playerHealth.OnRecoveryEvent -= SetFillAmount;
        _player.OnDeadEvent -= isDead => SetFillAmount();
    }

    public void InitilaizeFillAmount()
    {
        fillImage.fillAmount = 1;
    }

    private void SetFillAmount()
    {
        fillImage.DOFillAmount((float)_playerHealth.CurrentHealth /_playerHealth.MaxHealth, 0.5f);
    }
}
