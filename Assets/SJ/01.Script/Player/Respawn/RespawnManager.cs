using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using Unity.Cinemachine;
using UnityEngine;

public class RespawnManager : MonoSingleton<RespawnManager>
{
    [SerializeField] private Player _player;
    [SerializeField] private CinemachineCamera _cam;

    public List<RespawnPoint> RespawnPoints { get; private set; } = new List<RespawnPoint>();
    private RespawnPoint _currentPoint;
    private Tween _tween;

    private void Awake()
    {
        RespawnPoints = GetComponentsInChildren<RespawnPoint>().ToList();
        _currentPoint = RespawnPoints[0];
    }

    public void LoadRespawnPoint(string pointKey)
    {
        RespawnPoints.ForEach(p =>
        {
            if (p.key == pointKey)
            {
                _currentPoint = p;
            }
        });
    }

    public string GetCurrentPointkey()
    {
        return _currentPoint.key;
    }

    public void UpdateRespawnPoint(RespawnPoint newRespawnPoint)
    {
        _currentPoint.DisablePespawnPoint();
        _currentPoint = newRespawnPoint;
    }
    public void Respawn(GameObject objectToRespawn)
    {
        _player.SetDead(false);

        _player.GetCompo<AgentRenderer>().SpriteRenderer.DOFade(1, 1f).OnComplete(() =>
        {
            _player.StateMachine.ChangeState(PlayerStateEnum.Idle);
        });

        _currentPoint.RespawnPlayer();
        objectToRespawn.SetActive(true);

        _cam.enabled = false;
        _tween = DOVirtual.DelayedCall(0.5f, () => _cam.enabled = true);
    }

    private void OnDisable()
    {
        if (_tween != null)
            _tween.Kill();
    }
}
