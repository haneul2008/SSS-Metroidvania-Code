using System;
using UnityEngine;

public class ShockWave : Item
{
    private ShockWaveDataSO _shockWaveData;
    private PlayerMovement _playerMovement;

    private bool _isFallState;
    private bool _canAttack;
    private bool _isDetectRay;

    public override void Initialize(ItemDataSO itemData, Player player)
    {
        base.Initialize(itemData, player);

        _shockWaveData = itemData as ShockWaveDataSO;
    }

    public override void OnSpawn()
    {
        base.OnSpawn();

        Player.OnPlayerStateChange += HandleStateChange;
        _playerMovement = Player.GetCompo<PlayerMovement>();
        _playerMovement.OnLandingEvent.AddListener(HandleLanding);
    }

    private void HandleStateChange(PlayerState state)
    {
        _isFallState = state.GetType() == typeof(PlayerFallState);
    }

    public override void Update()
    {
        base.Update();

        CheckVelocity();
    }

    private void CheckVelocity()
    {
        if (!_isFallState || _canAttack || _isDetectRay) return;

        if(Mathf.Abs(_playerMovement.Rigid.linearVelocity.y) > _shockWaveData.limitY)
        {
            if(Physics2D.Raycast(Player.transform.position, Vector2.down, _shockWaveData.rayDistance, _shockWaveData.whatIsGround))
            {
                _isDetectRay = true;
                return;
            }

            _canAttack = true;

            _playerMovement.Rigid.linearVelocity *= _shockWaveData.force;
        }
    }

    private void HandleLanding(Vector2 velocity)
    {   
        if (_canAttack)
        {
            _canAttack = false;

            Vector2 pos = (Vector2)Player.transform.position + _shockWaveData.offset;

            //PlayDistotionEffect(pos);
            SpawnShockWaveObject(pos);
        }

        _isDetectRay = false;
    }

    private void SpawnShockWaveObject(Vector2 pos)
    {
        ShockWaveObject shockWaveObject = PoolManager.Instance.Pop("ShockWaveObject") as ShockWaveObject;
        shockWaveObject.SetUp(this, pos, _shockWaveData.damage, _shockWaveData.knockbackPower);
    }

    private void PlayDistotionEffect(Vector2 pos)
    {
        DistotionEffect distotion = PoolManager.Instance.Pop("DistotionEffect") as DistotionEffect;
        distotion.PlayEffect(pos, _shockWaveData.distotionDuration, _shockWaveData.distotionEndSize);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();

        Player.OnPlayerStateChange -= HandleStateChange;
        _playerMovement = Player.GetCompo<PlayerMovement>();
        _playerMovement.OnLandingEvent.RemoveListener(HandleLanding);
    }
}
