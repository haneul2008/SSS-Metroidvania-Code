using System.Collections;
using UnityEngine;

public class LightningBossPattern_3State : BossState
{
    private LightningBoss _lightningBoss;

    public float upBlockCount = 3;
    public LightningBossPattern_3State(StateMachine<BossEnum, BossState> stateMachine, string animName, Agent agent) : base(stateMachine, animName, agent)
    {
        _lightningBoss = agent as LightningBoss;
    }

    public override void Enter()
    {
        base.Enter();
        _lightningBoss.OnCameraShakingEvent?.Invoke();
        m_BossMovement.StopX();
        _lightningBoss.isPatternTrigger = false;
        _lightningBoss.OnBoomEvent?.Invoke();
        Pattern_3_Start();
    }


    private void Pattern_3_Start()
    {
        
        WarningSign warningSign = GameObject.Instantiate(_lightningBoss.warningSign, m_Boss.targetTrm.position, Quaternion.identity);
        warningSign.Initialize(_lightningBoss);
        GameManager.Instance.StartCoroutine(BossAppear(warningSign));
        m_Boss.gameObject.SetActive(false);
    }


    private IEnumerator BossAppear(WarningSign warningSign)
    {
        yield return new WaitForSeconds(3f);

        m_Boss.transform.position = (Vector2)warningSign.transform.position;
        m_Boss.gameObject.SetActive(true);
        GameManager.Destroy(warningSign.gameObject);
        ChangeDownAttackState();
    }

    private void ChangeDownAttackState()
    {
        _stateMachine.ChangeState(BossEnum.DownAttack);
    }

    public override void Exit()
    {
        base.Exit();
        _lightningBoss.OnCameraShakingEvent?.Invoke();
        m_Boss.IsCanPattern = false;
        _lightningBoss.isPattern_3End = false;
        m_Boss.IsCanPatternSetTrue(5f);
    }
}
