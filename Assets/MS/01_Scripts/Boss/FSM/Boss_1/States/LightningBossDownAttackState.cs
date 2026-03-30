using UnityEngine;

public class LightningBossDownAttackState : BossState
{
    private float DELAY_TIME = 0.2f;
    private float _startTime;

    public float UpBlockCount = 3;

    private Rigidbody2D _rbCompo;
    private float startGravityScail;

    private bool isYMove;
    public LightningBossDownAttackState(StateMachine<BossEnum, BossState> stateMachine, string animName, Agent agent) : base(stateMachine, animName, agent)
    {
        _rbCompo = m_Boss.GetComponent<Rigidbody2D>();
        startGravityScail = _rbCompo.gravityScale;
    }

    public override void Enter()
    {
        base.Enter();
        m_BossMovement.StopX();
        MoveY(UpBlockCount);
        _startTime = Time.time;
    }

    private void MoveY(float upBlockCount)
    {
        m_Boss.transform.position += new Vector3(0, upBlockCount * 2, 0);
        isYMove = true;
        _rbCompo.gravityScale = 30;
    }

    public override void Exit()
    {
        base.Exit();
    }


    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if ((_startTime + DELAY_TIME < Time.time) && isYMove && m_BossMovement.IsGround)
        {
            ChangeRandingState();
        }
    }

    private void ChangeRandingState()
    {
        _stateMachine.ChangeState(BossEnum.Randing);
    }
}
