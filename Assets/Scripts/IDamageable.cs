using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    // ������ ��, ���� ��, ���� ǥ�� ������ �����ؾ��ϴ� �������̽�
    void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal);
}
