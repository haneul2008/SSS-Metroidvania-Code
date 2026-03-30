using UnityEngine;

public class Summoner : Enemy
{
    public override void Attack()
    {
        MiniGhoul miniGhoul = PoolManager.Instance.Pop("MiniGhoul") as MiniGhoul;
    }
}
