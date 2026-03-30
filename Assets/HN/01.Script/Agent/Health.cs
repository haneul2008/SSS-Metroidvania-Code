using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IAgentComponent
{
    [SerializeField] protected int _maxHealth = 100;
    public int CurrentHealth { get; protected set; }
    public int MaxHealth => _maxHealth;

    public UnityEvent OnDeadEvent;
    public UnityEvent OnDamagedEvent;
    public event Action OnRecoveryEvent;

    public bool isCanHit = true;

    public Agent Agent {  get; protected set; }

    private Tween _tween;

    protected virtual void Awake()
    {
        ResetHealth();
    }

    public virtual void ResetHealth()
    {
        CurrentHealth = _maxHealth;
        isCanHit = true;
    }

    public virtual bool CastDamage(int damage, float knockbackPower)
    {
        if(isCanHit && Agent == null)
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, _maxHealth);

            OnDamagedEvent?.Invoke();

            if (CurrentHealth == 0)
                OnDeadEvent?.Invoke();
        }
        else
        {
            if (!isCanHit || Agent.IsDead) return false;

            CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, _maxHealth);

            OnDamagedEvent?.Invoke();

            if (CurrentHealth == 0)
                OnDeadEvent?.Invoke();
        }

        

        return true;
    }

    public virtual void RecoveryHealth(int health)
    {
        if (Agent.IsDead) return;

        CurrentHealth = Mathf.Clamp(CurrentHealth + health, 0, _maxHealth);

        OnRecoveryEvent?.Invoke();
    }

    public void SetHp(int hp)
    {
        CurrentHealth = hp;
    }

    public void IgonreDamage(float duration)
    {
        isCanHit = false;

        _tween = DOVirtual.DelayedCall(duration, () => isCanHit = true);
    }

    public void Initialize(Agent agent)
    {
        Agent = agent;
    }

    private void OnDisable()
    {
        if(_tween != null) 
            _tween.Kill();
    }
}
