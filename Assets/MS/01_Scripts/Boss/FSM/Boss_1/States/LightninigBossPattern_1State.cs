using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightninigBossPattern_1State : BossState
{
    private int RECOVERY_COUNT = 1;
    private float PATTERN_1_DELAY_TIME = 6;
    private int LIFE_CIRCLE_COUNT = 2;

    private float startTime;

    private LightningBoss lightningBoss;

    public List<LifeCircle> lifeCircleList = new List<LifeCircle>();

    public LightninigBossPattern_1State(StateMachine<BossEnum, BossState> stateMachine, string animName, Agent agent) : base(stateMachine, animName, agent)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log(_stateMachine.CurrentState + " 들어감");
        startTime = Time.time;
        m_Boss.StartCoroutine(RecoveryTime());

        m_BossMovement.StopX();

        lightningBoss = m_Boss as LightningBoss;
        m_Boss.StartCoroutine(SpawnLifeCircle(lightningBoss.minTransform, lightningBoss.maxTransform, LIFE_CIRCLE_COUNT));
    }

    //카운트 수에 맞춰서 라이프 서클을 랜덤 좌표로 생성한다
    private IEnumerator SpawnLifeCircle(Transform minTransform, Transform maxTransform, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector2 spawnPoint = new Vector2(Random.Range(minTransform.position.x, maxTransform.position.x),
            Random.Range(minTransform.position.y, maxTransform.position.y));

            LifeCircle lifeCircle = GameObject.Instantiate(lightningBoss.lifeCirclePrefab, spawnPoint, Quaternion.identity);
            lifeCircle.OnDeadCircleEvent += LifeCircleDestroy;

            lifeCircleList.Add(lifeCircle);
            yield return new WaitForSeconds(0.65f);
        }
    }

    public override void Exit()
    {
        base.Exit();
        LifeCircleDestroyAll();
        Debug.Log(_stateMachine.CurrentState + " 나감");
        m_Boss.IsCanPattern = false;
        m_Boss.StopAllCoroutines();
        m_Boss.IsCanPatternSetTrue(4f);
    }

    public override void Update()
    {
        base.Update();


        if(lifeCircleList.Count <= 0)
        {
            ChangeHitState();
        }


        if (startTime + (PATTERN_1_DELAY_TIME * LIFE_CIRCLE_COUNT) < Time.time)
        {
            //회복이 끝났으니 아이들로 돌아간다
            ChangeIdleState();
        }

    }

    private void ChangeHitState()
    {
        _stateMachine.ChangeState(BossEnum.Hit);
    }

    public IEnumerator RecoveryTime()
    {
        yield return new WaitForSeconds(1);
        m_Boss.bossHealth.RecoveryHP(RECOVERY_COUNT);
        m_Boss.StartCoroutine(RecoveryTime());
    }

    private void ChangeIdleState()
    {
        _stateMachine.ChangeState(BossEnum.Idle);
    }

    public void LifeCircleDestroy(LifeCircle lifeCircle)
    {
        lifeCircleList.Remove(lifeCircle);
    }

    public void LifeCircleDestroyAll()
    {
        foreach(LifeCircle item in lifeCircleList)
        {
            GameObject.Destroy(item.gameObject);
        }

        lifeCircleList.Clear();
    }
}
