using UnityEngine;

public static class TransformExtensions
{
    /// <summary>
    /// 대상 Transform까지의 월드 방향 벡터 (normalized)
    /// </summary>
    public static Vector3 DirectionTo(this Transform from, Transform to)
    {
        return (to.position - from.position).normalized;
    }
    
    /// <summary>
    /// 대상 Position까지의 월드 방향 벡터 (normalized)
    /// </summary>
    public static Vector3 DirectionTo(this Transform from, Vector3 to)
    {
        return (to - from.position).normalized;
    }

    /// <summary>
    /// 대상 Transform까지의 XZ 평면 방향 벡터 (Y = 0)
    /// </summary>
    public static Vector3 FlatDirectionTo(this Transform from, Transform to)
    {
        Vector3 dir = to.position - from.position;
        dir.y = 0f;
        return dir.normalized;
    }
    
    /// <summary>
    /// 대상 Position까지의 XZ 평면 방향 벡터 (Y = 0)
    /// </summary>
    public static Vector3 FlatDirectionTo(this Transform from, Vector3 to)
    {
        Vector3 dir = to - from.position;
        dir.y = 0f;
        return dir.normalized;
    }

    /// <summary>
    /// 대상까지의 거리
    /// </summary>
    public static float DistanceTo(this Transform from, Transform to)
    {
        return Vector3.Distance(from.position, to.position);
    }
    
    /// <summary>
    /// 대상까지의 거리 (X,Z만 계산)
    /// </summary>
    public static float FlatDistanceTo(this Transform from, Transform to)
    {
        Vector3 a = from.position;
        a.y = 0.0f;
        
        Vector3 b = to.position;
        b.y = 0.0f;
        
        return Vector3.Distance(a, b);
    }
    

    /// <summary>
    /// 대상까지의 거리 제곱 (루트 연산 X, 최적화용)
    /// </summary>
    public static float SqrDistanceTo(this Transform from, Transform to)
    {
        return (to.position - from.position).sqrMagnitude;
    }

    /// <summary>
    /// 대상이 내 시야 각 안에 있는지 확인
    /// </summary>
    public static bool IsInViewAngle(this Transform from, Transform target, float viewAngle)
    {
        Vector3 toTarget = (target.position - from.position).normalized;
        float dot = Vector3.Dot(from.forward, toTarget);
        float angleBetween = Mathf.Acos(dot) * Mathf.Rad2Deg;
        return angleBetween <= viewAngle * 0.5f;
    }

    /// <summary>
    /// 대상 쪽을 향하는 회전 값 (Y축 포함)
    /// </summary>
    public static Quaternion RotationTo(this Transform from, Transform to)
    {
        Vector3 dir = (to.position - from.position).normalized;
        return Quaternion.LookRotation(dir);
    }

    /// <summary>
    /// 대상 쪽을 향하는 회전 값 (Y축 고정)
    /// </summary>
    public static Quaternion FlatRotationTo(this Transform from, Transform to)
    {
        Vector3 dir = to.position - from.position;
        dir.y = 0f;
        if (dir == Vector3.zero) return from.rotation;
        return Quaternion.LookRotation(dir.normalized);
    }

    /// <summary>
    /// 부드럽게 회전하여 대상 바라보기 (Y축 포함)
    /// </summary>
    public static void SmoothLookAt(this Transform from, Transform target, float rotateSpeed)
    {
        Quaternion targetRot = from.RotationTo(target);
        from.rotation = Quaternion.Slerp(from.rotation, targetRot, rotateSpeed * Time.deltaTime);
    }

    /// <summary>
    /// 부드럽게 회전하여 대상 바라보기 (Y축 고정)
    /// </summary>
    public static void SmoothLookAtFlat(this Transform from, Transform target, float rotateSpeed)
    {
        Quaternion targetRot = from.FlatRotationTo(target);
        from.rotation = Quaternion.Slerp(from.rotation, targetRot, rotateSpeed * Time.deltaTime);
    }
}
