using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    [SerializeField] float movementSpeed;
    [SerializeField] float attackRate;
    [SerializeField] float attackDamage;
    [SerializeField] float attackRange;
    [SerializeField] Vector3 direction;

    private float cooldown;
    bool moving = true;
    private void Update()
    {
        if (moving)
        {
            transform.Translate(Time.deltaTime * movementSpeed * direction.normalized);
        }
        ActionCondition();
    }

    protected override void ActionCondition()
    {

        Collider[] hits = Physics.OverlapBox(transform.position + attackRange * direction, Vector3.one / 10);
        Unit u;
        if (hits.Length > 0)
        {
            foreach (Collider c in hits)
            {
                u = c.gameObject.GetComponent<Unit>();
                if (u != null && u.Faction != faction)
                {
                    cooldown -= Time.deltaTime;
                    if (cooldown <= 0)
                    {
                        u.TakeDamage(attackDamage);
                        cooldown = attackRate;
                    }
                    moving = false;
                    return;
                }
            }
            return;
        }
        moving = true;
        cooldown = 0;
    }


}
