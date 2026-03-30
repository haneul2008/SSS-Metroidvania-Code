using UnityEngine;
using UnityEngine.Events;

public class BossAnimationTrigger : MonoBehaviour, IAgentComponent<Boss>
{
    private Boss _boss;

    public UnityEvent OnStepEvent;

    public void Initialize(Agent agent) => Initialize(agent as Boss);

    public void Initialize(Boss boss)
    {
        _boss = boss;
    }
    private void AnimationEndTrigger()
    {
        _boss.AnimationEndTrigger();
    }

    private void AnimationAttackTrigger()
    {
        _boss.Attack();
    }

    private void AnimationPatternTrigger()
    {
        _boss.PatternTrigger();
    }

    private void BossStepTrigger()
    {
        OnStepEvent?.Invoke();
    }

}
