using UnityEngine;
using Transform = UnityEngine.Transform;

public static class UtilityClass
{
    public static float GetRandom_100()
    {
        return Random.Range(0.0f, 100.0f);
    }

    public static Vector2 TargetDirection(Transform targetTrm, Transform transform)
    {
        return targetTrm.position - transform.position;
    }

    public static Vector2 TargetDirectionXORY(Vector2 xy,Transform targetTrm, Transform transform)
    {
        if (xy != new Vector2(1, 0) && xy != new Vector2(0, 1))
        {
            Debug.LogError("벡터값 제대로 입력하셈 나 이상한거 보낼거임");
            return Vector2.zero;
        }

        return (targetTrm.position - transform.position) * xy;
    }

    public static float TargetDirectionXORY(bool isX, Transform targetTrm, Transform transform)
    {

        if (isX)
        {
            return (targetTrm.position - transform.position).x;
        }
        else
        {
            return (targetTrm.position - transform.position).y;
        }
    }
}
