using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : AgentMovement, IAgentComponent<Player>
{
    public UnityEvent<Vector2> OnLandingEvent;
    public UnityEvent OnDashEvent;
    public UnityEvent OnRollEvent;

    [SerializeField] private float _rollPower;
    [SerializeField] private float _dashPower;
    [SerializeField] private float _dashTime;

    private bool _isFlip;
    private bool _isDash;
    private Coroutine _dashCorutine;

    private Player _player;
    private Rigidbody2D _rigid;
    private InputReader _inputReader;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    public void AnchorY(bool anchor = true)
    {
        if (anchor == true)
        {
            _rigid.constraints = RigidbodyConstraints2D.FreezePositionY;
        }
        else
        {
            _rigid.constraints = RigidbodyConstraints2D.None;
            _rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
    public override void Initialize(Agent agent)
    {
        base.Initialize(agent);

        Initialize(agent as Player);

        _inputReader = _player.GetCompo<InputReader>();
    }

    public void TryToRoll(bool Flip)
    {
        if (!CanMove) return;

        OnRollEvent?.Invoke();

        Rigid.AddForce((Flip ? Vector2.left : Vector2.right) * _rollPower, ForceMode2D.Impulse);
    }
    public void TryToDash(bool Flip, Action onCancel = null)
    {
        _isFlip = Flip;
        if (!CanMove|| _isDash) return;

        OnDashEvent?.Invoke();

        _isDash = true;

        _dashCorutine = StartCoroutine(DashCoroutine(onCancel));
    }

    private IEnumerator DashCoroutine(Action onCancel = null)
    {
        float _prevGravity = Rigid.gravityScale, currentTime = 0, percent = 0;
        Rigid.gravityScale = 0;
        Rigid.linearVelocityY = 0;

        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / _dashTime;
            Rigid.linearVelocityX = _isFlip ? -_dashPower : _dashPower;
            yield return null;

            bool isCancel = Mathf.Sign(_inputReader.InputVector.x) == Mathf.Sign(-Rigid.linearVelocityX);

            if (_isForceStop || isCancel)
            {
                if(isCancel) onCancel?.Invoke();

                break;
            }
        }

        Rigid.gravityScale = _prevGravity;

        ClearDash();
    }

    public void ClearDash()
    {
        Rigid.linearVelocity = Vector2.zero;

        _isDash = false;
    }

    public void Initialize(Player player)
    {
        _player = player;
    }
}
