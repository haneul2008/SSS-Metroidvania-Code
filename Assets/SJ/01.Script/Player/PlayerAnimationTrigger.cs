using UnityEngine;

public class PlayerAnimationTrigger : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private SoundSo _attackSound;

    private void AttackTrigger()
    {
        _player.Attack();
    }
    private void PlayAttackSound()
    {
        SoundManager.Instance.PlaySound(_attackSound);
    }
    private void RollTriger()
    {
        
    }

    protected void EndTrigger()
    {
        _player.AnimationEndTrigger();
    }
}
