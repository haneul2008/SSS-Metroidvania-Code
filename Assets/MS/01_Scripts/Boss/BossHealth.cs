using UnityEngine;
using UnityEngine.Events;

public class BossHealth : Health
{
    public UnityEvent OnBossRecoveryEvent;

    public void RecoveryHP(int recoveryCount)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + recoveryCount, 0, _maxHealth);
        OnBossRecoveryEvent?.Invoke();
    }


}
