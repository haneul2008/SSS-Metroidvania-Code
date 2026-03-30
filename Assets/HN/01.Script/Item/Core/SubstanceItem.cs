using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SubstanceItem : MonoBehaviour
{
    public UnityEvent OnTriggerdEvent;
    public UnityEvent OnEffectUpdateEvent;
    public UnityEvent OnEffectEndEvent;

    [Header("Info setting")]
    [SerializeField] private ItemDataSO _itemData;
    [SerializeField] private float _effectCnt;
    [SerializeField] private float _effectDuration;
    [SerializeField] private float _effectUpdateDelay;

    [Header("distotion setting")]
    [SerializeField] private float _distotionDuration;
    [SerializeField] private Vector3 _distotionEndSize;

    private Collider2D _collider;
    private Coroutine _waitEffectCoroutine;
    private Tween _scaleTween;
    private WaitForSeconds _effectUpdateSeconds;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        SaveManager.Instance.OnDataLoaded += HandleDataLoad;

        _effectUpdateSeconds = new WaitForSeconds(_effectUpdateDelay);
    }

    private void HandleDataLoad()
    {
        SaveManager.Instance.OnDataLoaded -= HandleDataLoad;

        foreach (string key in ItemManager.Instance.GetCollectedItemsKey())
        {
            if (_itemData.itemKey.Trim().Equals(key))
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            OnTriggerdEvent?.Invoke();

            PlayItemGetEffect(collision);

            DistotionEffect distotion = PoolManager.Instance.Pop("DistotionEffect") as DistotionEffect;
            distotion.PlayEffect(transform.position, _distotionDuration, _distotionEndSize);

            PlayerMovement playerMovement = player.GetCompo<PlayerMovement>();
            playerMovement.ForceStop(false, true);

            Health health = player.GetCompo<Health>();
            health.IgonreDamage(_effectDuration + 0.5f);

            _waitEffectCoroutine = StartCoroutine(WaitEffectCoroutine(playerMovement));
            _scaleTween = transform.DOScale(0, _effectDuration);
            _collider.enabled = false;
        }
    }

    private void PlayItemGetEffect(Collider2D collision)
    {
        for (int i = 0; i < _effectCnt; i++)
        {
            ItemGetEffect getEffect = PoolManager.Instance.Pop("ItemGetEffect") as ItemGetEffect;
            getEffect.PlayEffect(transform.position);

            Vector2 targetPos = collision.transform.position;
            float randX = targetPos.x + Random.Range(-7.5f, 4f);

            Vector2 controlPos1 = new Vector3(randX, targetPos.y - Random.Range(-7.5f, 4f));
            Vector2 controlPos2 = new Vector3(randX, targetPos.y - Random.Range(0, 7.5f));

            getEffect.Initialize(transform.position, controlPos1, controlPos2, targetPos, _effectDuration);
        }
    }

    private IEnumerator WaitEffectCoroutine(PlayerMovement playerMovement)
    {
        int cnt = Mathf.RoundToInt(_effectDuration / _effectUpdateDelay);

        for (int i = 0; i < cnt; i++)
        {
            yield return _effectUpdateSeconds;

            OnEffectUpdateEvent?.Invoke();
        }

        OnEffectEndEvent?.Invoke();

        ItemManager.Instance.GetItem(_itemData);

        playerMovement.ForceStop(true, true);

        Destroy(gameObject);
    }

    private void OnDisable()
    {
        if (_waitEffectCoroutine != null)
            StopCoroutine(_waitEffectCoroutine);

        if (_scaleTween != null)
            _scaleTween.Kill();
    }
}
