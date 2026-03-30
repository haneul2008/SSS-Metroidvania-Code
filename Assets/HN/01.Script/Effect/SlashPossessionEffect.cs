using UnityEngine;

public class SlashPossessionEffect : Effect
{
    [SerializeField] private Vector2 _offset;

    private Transform _targetTrm;

    public void PlayEffect(Vector2 pos, Transform targetTrm)
    {
        base.PlayEffect(pos, false);

        _targetTrm = targetTrm;
    }

    private void Update()
    {
        if (_targetTrm == null) return;

        transform.position = (Vector2)_targetTrm.position + _offset;
    }
}
