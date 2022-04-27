using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    // 데미지 양, 맞은 곳, 맞은 표면 방향을 구현해야하는 인터페이스
    void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal);
}
