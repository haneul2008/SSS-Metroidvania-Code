using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeadState : PlayerState
{
    private const float DELAY_TIME = 0.5f;

    private float _startTime;

    public PlayerDeadState(StateMachine<PlayerStateEnum, PlayerState> stateMachine, string animName, Agent agent) : base(stateMachine, animName, agent)
    {
    }

    public override void Enter()
    {
        PlayerHPBar.Instance.InitilaizeFillAmount();
        _player.SetDead(true);
        _player.StartCoroutine(WaitRespawn());
        _agentRenderer.SpriteRenderer.DOFade(0, 0.35f).OnComplete(() =>
        {
            RespawnManager.Instance.Respawn(_player.gameObject);
        });
        _startTime = Time.time;
        base.Enter();
    }

    public IEnumerator WaitRespawn()
    {
        yield return new WaitForSeconds(5f);
    }


}
