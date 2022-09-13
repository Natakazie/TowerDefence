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
        if (hits.Length > 0)
        {
            foreach (Collider c in hits)
            {
                if (c.gameObject.GetComponent<Unit>().Faction != faction)
                {
                    moving = false;
                    return;
                }
            }
            return;
        }
        moving = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + attackRange * direction, Vector3.one / 10);

    }
}
