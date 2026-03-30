using System.Collections;
using UnityEngine;

public class StepFeedback : Feedback
{
    [SerializeField] private float _distance;
    [SerializeField] private LayerMask _crumblingWallLayerMask;
    [SerializeField] private string _particleName = "DestroyWallStepEffect";
    [SerializeField] private string _playerStepName = "PlayerStepEffect";
    [SerializeField] private Vector2 _playerStepOffset;
    [SerializeField] private Player _player;
    [SerializeField] private float _delay;
    [SerializeField] private SoundSo _stepSound;
    [SerializeField] private bool _isPlayerMoveStep;

    private bool _isPlaying = false;
    private WaitForSeconds _ws;

    private void Start()
    {
        _ws = new WaitForSeconds(_delay);
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        while (true)
        {
            yield return _ws;

            _isPlaying = true;
        }
    }

    public override void PlayFeedback()
    {
        if (!_isPlaying) return;

        _isPlaying = false;

        RaycastHit2D ray = Physics2D.Raycast(_player.transform.position, Vector2.down, _distance, _crumblingWallLayerMask);
        if (_isPlayerMoveStep)
        {
            Effect playerStepEffect = PoolManager.Instance.Pop(_playerStepName) as Effect;
            playerStepEffect.PlayEffect(ray.point + _playerStepOffset);
            SoundManager.Instance.PlaySound(_stepSound);
        }

        if (!ray) return;

        if (ray.collider.gameObject.TryGetComponent<CrumblingWall>(out CrumblingWall component))
        {
            Effect effect = PoolManager.Instance.Pop(_particleName) as Effect;
            effect.PlayEffect(ray.point);
        }
    }

    public override void StopFeedback()
    {

    }
}