using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public float startingHealth = 100f;
    public float health { get; protected set; }
    public bool dead { get; protected set; }

    public virtual void OnEnable()
    {
        dead = false;
        health = startingHealth;
    }
    public virtual void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        health -= damage;

        if(health <= 0 && !dead)
        {
            Die();
        }
    }
    public virtual void RechargeHealth(float newHealth)
    {
        if(dead)
        {
            return;
        }

        health += newHealth;
    }

    public virtual void Die()
    {
        dead = true;
    }
}
