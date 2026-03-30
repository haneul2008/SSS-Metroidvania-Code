using System.Collections;
using UnityEngine;

public class ElectricPole : MonoBehaviour, IAgentComponent<LightningBoss>
{
    private LightningBoss _lightningBoss;
    private Rigidbody2D _rbCompo;

    [HideInInspector] public int currentIndex = 0;
    public int damage = 30;
    public int knockbackPower = 2;

    private Vector2 _direction;


    public void Initialize(LightningBoss lightningBoss)
    {
        _lightningBoss = lightningBoss;
        _rbCompo = GetComponent<Rigidbody2D>();
        GameManager.Instance.StartCoroutine(CreatNextElectricPole());
        GetComponentInChildren<BoxDamageCaster>().Initialize(_lightningBoss);
        GetComponentInChildren<BoxDamageCaster>().CastDamage(damage, knockbackPower,true);
    }

    public void SetTargetVec(Vector2 dir)
    {
        _direction = dir;
    }

    public IEnumerator CreatNextElectricPole()
    {
        LightningBoss lightningBoss = _lightningBoss;

        yield return new WaitForSeconds(0.25f);
        

        if (currentIndex + 1 > 3)
        {
            Destroy();
        }
        else
        {
            ElectricPole pole = GameManager.Instantiate(lightningBoss.electricPolePrefab,
            new Vector3(transform.position.x + _direction.x, transform.position.y, transform.position.z)
            , Quaternion.identity);
            pole.currentIndex = currentIndex + 1;

            pole.Initialize(_lightningBoss);
            pole.SetTargetVec(_direction);
            Destroy();
        }

    }



    public void Initialize(Agent agent) => Initialize(agent as LightningBoss);


    public void Destroy()
    {
        Destroy(gameObject);
    }
}
