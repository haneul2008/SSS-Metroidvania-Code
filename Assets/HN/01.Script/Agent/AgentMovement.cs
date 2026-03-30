using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class AgentMovement : MonoBehaviour, IAgentComponent
{
    public UnityEvent OnJumpEvent;

    public bool IsGround { get; protected set; }
    public Rigidbody2D Rigid { get; protected set; }
    public bool CanMove { get; protected set; } = true;

    [SerializeField] protected Transform _groundCheckerTrm;
    [SerializeField] protected Vector2 _groundCheckerSize;
    [SerializeField] protected LayerMask _whatIsGround;

    [SerializeField] protected float _jumpPower = 8f;
    [SerializeField] protected float _extarGravity = 35f;
    [SerializeField] protected float _gravityDelay = 0.15f;
    [SerializeField] protected float _knockbackTime = 0.2f;

    public float moveSpeed = 5.5f;

    protected Coroutine _knockbackCoroutine;
    protected float _timeInAir;
    protected Agent _agent;
    protected bool _isKnockback;
    protected bool _isForceStop;
    protected float _originGravity;

    public virtual void Initialize(Agent agent)
    {
        _agent = agent;
        Rigid = GetComponent<Rigidbody2D>();
        _originGravity = Rigid.gravityScale;
    }

    protected void Update()
    {
        if (!IsGround)
        {
            _timeInAir += Time.deltaTime;
        }
        else
        {
            _timeInAir = 0;
        }
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void CheckGround() =>
        IsGround = Physics2D.OverlapBox(_groundCheckerTrm.position, _groundCheckerSize, 0, _whatIsGround);

    public void MoveX(float x)
    {
        if (!CanMove) return;

        Rigid.linearVelocityX = x * moveSpeed;
    }

    public void StopX(bool ignoreKnockback = false)
    {
        if (_isKnockback && !ignoreKnockback) return;

        Rigid.linearVelocityX = 0;
    }

    public void TryToJump()
    {
        OnJumpEvent?.Invoke();

        Rigid.linearVelocity = Vector2.zero;
        Rigid.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
        _timeInAir = 0;
    }

    public void ForceStop(bool canMove, bool stopY)
    {
        StopX(true);

        CanMove = canMove;
        _isForceStop = !canMove;

        if (stopY)
        {
            Rigid.gravityScale = canMove ? _originGravity : 0;
            Rigid.linearVelocityY = 0;
        }
    }

    #region Knockback

    public virtual void GetKnockback(Vector2 direction, float power)
    {
        if (_isForceStop) return;

        Vector3 difference = direction * power * Rigid.mass;
        Rigid.AddForce(difference, ForceMode2D.Impulse);

        _isKnockback = true;

        if (_knockbackCoroutine != null)
            StopCoroutine(_knockbackCoroutine);

        _knockbackCoroutine = StartCoroutine(KnockbackCoroutine());
    }

    private IEnumerator KnockbackCoroutine()
    {
        CanMove = false;
        yield return new WaitForSeconds(_knockbackTime);

        ClearKnockback();
    }

    public void ClearKnockback()
    {
        Rigid.linearVelocity = Vector2.zero;
        if (!_isForceStop) CanMove = true;
        _isKnockback = false;
    }

    #endregion

    private void OnDrawGizmosSelected()
    {
        if (_groundCheckerTrm == null) return;

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(_groundCheckerTrm.position, _groundCheckerSize);
    }
}
