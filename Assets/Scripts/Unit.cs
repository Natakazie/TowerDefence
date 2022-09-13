using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] float damageModifier = 1;

    protected bool placed;
    [SerializeField] protected bool faction;

    public bool Faction
    {
        get
        {
            return faction;
        }
    }
    public virtual void OnCreate()
    {
        placed = true;
    }

    protected virtual void ActionCondition()
    {

    }
    public void TakeDamage(float dmg)
    {
        health -= dmg * damageModifier;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
