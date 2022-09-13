using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] float damageModifier;


    bool faction;

    public virtual void OnCreate()
    {

    }
}
