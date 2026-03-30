using System.Collections;
using UnityEngine;
public class LightningBossPattern_2_2State : BossState
{
    public LightningBossPattern_2_2State(StateMachine<BossEnum, BossState> stateMachine, string animName, Agent agent) : base(stateMachine, animName, agent)
    {
        _lightningBoss = m_Boss as LightningBoss;
        _electricPole = _lightningBoss.electricPolePrefab;
    }

    private int CREATE_COUNT = 4;

    private LightningBoss _lightningBoss;
    private ElectricPole _electricPole;

    private Vector3 direction;


    public override void Enter()
    {
        base.Enter();
        m_BossMovement.StopX();
        direction = new Vector3(
                (m_Boss.targetTrm.position - m_Boss.transform.position).x, 0, 0).normalized;
    }

    public IEnumerator CreateElectricPole()
    {
        _lightningBoss.OnCameraShakingEvent?.Invoke();

        ElectricPole electricPole = GameObject.Instantiate(
            _electricPole,
            m_Boss.transform.position + (direction * 1.5f), Quaternion.identity);
        electricPole.Initialize(_lightningBoss);
        electricPole.SetTargetVec(direction * 3);

        ElectricPole electricPole2 = GameObject.Instantiate(
            _electricPole,
           m_Boss.transform.position + -(direction * 1.5f), Quaternion.identity);
        electricPole2.Initialize(_lightningBoss);
        electricPole2.SetTargetVec(-direction * 3);

        yield return new WaitForSeconds(0.2f);
        ChageIdleState();
    }

    private void ChageIdleState()
    {
        _stateMachine.ChangeState(BossEnum.Idle);
    }

    public override void Exit()
    {
        base.Exit();
        m_Boss.IsCanPattern = false;
        m_Boss.IsCanPatternSetTrue(5f);
    }

    public override void Update()
    {
        base.Update();
        if (_lightningBoss.isPatternTrigger)
        {
            _lightningBoss.StartCoroutine(CreateElectricPole());
            _lightningBoss.OnTakeDownEvent?.Invoke();
            _lightningBoss.isPatternTrigger = false;
        }
    }
}
