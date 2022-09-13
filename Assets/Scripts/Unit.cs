using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//INHERITANCE
//Parent Class
public class Unit : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] float damageModifier = 1;

    protected bool placed;
    [SerializeField] protected bool faction;

    public bool Faction
    {
        // ENCAPSULATION
        get
        {
            return faction;
        }
    }
    //POLYMORPHISM
    public virtual void OnCreate()
    {
        placed = true;
    }

    //POLYMORPHISM
    protected virtual void ActionCondition()
    {

    }
    //ABSTACTION
    public void TakeDamage(float dmg)
    {
        health -= dmg * damageModifier;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
