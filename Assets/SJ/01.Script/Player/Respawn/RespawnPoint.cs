using DG.Tweening;
using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class RespawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject respawnTarget;

    public UnityEvent OnSpawnPointUpdate;
    public string key;

    private void Start()
    {
        OnSpawnPointUpdate.AddListener(() =>
        GetComponentInParent<RespawnManager>().UpdateRespawnPoint(this));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            respawnTarget = collision.gameObject; 
            OnSpawnPointUpdate?.Invoke(); 
        }
    }

    public void RespawnPlayer()
    {
        respawnTarget.transform.position = transform.position;
    }

    public void DisablePespawnPoint()
    {
        GetComponent<Collider2D>().enabled = false;
    }

    public void ResetRespawnPoint()
    {
        respawnTarget = null;
        GetComponent<Collider2D>().enabled = true;
    }
}
