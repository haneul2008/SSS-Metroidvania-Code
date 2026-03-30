using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class CrumblingWall : MonoBehaviour, ICrumblingWall
{
    public int MaxCrumblingCount 
    {
        get { return maxCrumblingCount; }
        set { maxCrumblingCount = value;}
    }
    public int maxCrumblingCount = 1;
    public int CurrentCrumblingCount { get; set; }

    private void Awake()
    {
        CurrentCrumblingCount = MaxCrumblingCount;
    }

    public UnityEvent OnHitEvent;
    public UnityEvent OnDestroyEvent;

    public virtual void Crumbling(int count)
    {
        OnHitEvent?.Invoke();
        CurrentCrumblingCount -= count;

        if (CurrentCrumblingCount <= 0)
        {
            OnDestroyEvent?.Invoke();
            Destroy();
        }
    }


    public virtual void Destroy()
    {
        StartCoroutine(Destroy(0.15f));
    }

    public IEnumerator Destroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
