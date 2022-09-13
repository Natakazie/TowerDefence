using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float shootSpeed;
    [SerializeField] float damage = 3;
    private GameObject primaryTarget;

    private bool faction;
    public Vector3 direction
    {
        get;
        set;
    }
    private void Awake()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Time.deltaTime * shootSpeed* direction );
        HitCheck();
    }
    private void HitCheck()
    {
       Collider[] hit = Physics.OverlapSphere(transform.position, 0.05f);
        Unit u;
        foreach(Collider c in hit)
        {
            u = c.GetComponent<Unit>();
            if(u != null && u.Faction != faction)
            {
                u.TakeDamage(damage);
                gameObject.SetActive(false);
                return;
            }
        }
    }
    public void Allign(bool side)
    {
        faction = side;
    }
}
