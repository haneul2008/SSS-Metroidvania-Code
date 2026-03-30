using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Events;

public class RecoveryItem : MonoBehaviour
{
    public UnityEvent OnRecoveryEvent;

    [SerializeField] private string _key;
    [SerializeField] private float _endSize;
    [SerializeField] private float _sizeDuration;
    [SerializeField] private int _increaseHp;
    [SerializeField] private ParticleSystem _particle;

    private SpriteRenderer _spriteRenderer;
    private Tween _sizeTween;
    private Collider2D _collider;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
        _collider.enabled = false;
    }

    private void Start()
    {
        DOVirtual.DelayedCall(0.1f, CheckKey);
    }

    private void CheckKey()
    {
        if(GameManager.Instance.RecoveryItems.Contains(_key))
        {
            Destroy(gameObject);
            return;
        }

        _collider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            _particle.Stop();

            Health health = player.GetCompo<Health>();
            health.RecoveryHealth(_increaseHp);

            GameManager.Instance.CollectRecoveryItem(_key);

            SaveManager.Instance.JsonSave();

            _sizeTween = transform.DOScale(_endSize, _sizeDuration).OnComplete(() => Destroy(gameObject));

            OnRecoveryEvent?.Invoke();
        }
    }

    private void OnDisable()
    {
        if (_sizeTween != null)
            _sizeTween.Kill();
    }

    private void OnValidate()
    {
        if (string.IsNullOrEmpty(_key))
        {
            _key = Guid.NewGuid().ToString();
        }
    }
}
