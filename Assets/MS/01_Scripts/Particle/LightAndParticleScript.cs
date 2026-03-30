using System.Collections;
using UnityEngine;

public class LightAndParticleScript : MonoBehaviour
{
    public bool isLight, isParticle;

    public float time;

    private void Awake()
    {
        if (isLight)
        {
            StartCoroutine(DestroyLight(time));
        }
    }

    private IEnumerator DestroyLight(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    public void Update()
    {

    }
}
