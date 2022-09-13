using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//INHERITANCE
public class ShootTower : Unit
{
    [SerializeField] float shootRate;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform spawn;
    GameObject[] bullets = new GameObject[5];

    float sinceLastShot;
    int curBullet = 0;
    bool shooting = false;

    // Update is called once per frame
    void Update()
    {
        if (!placed)
        {
            return;
        }
        ActionCondition();
        //ABSTACTION
        Shoot();
    }
    //ABSTACTION
    void Shoot()
    {
        if (!shooting)
        {
            sinceLastShot = 0;
            return;
        }
        sinceLastShot -= Time.deltaTime;
        if (sinceLastShot <= 0)
        {
            bullets[curBullet].transform.SetPositionAndRotation(spawn.position, transform.rotation);
            bullets[curBullet].SetActive(true);
            curBullet++;
            curBullet %= bullets.Length;
            sinceLastShot = shootRate;
        }


    }
    //POLYMORPHISM
    public override void OnCreate()
    {
        base.OnCreate();
        if (bullet.GetComponent<Bullet>() != null)
        {
            for (int i = 0; i < bullets.Length; i++)
            {
                bullets[i] = Instantiate(bullet, spawn.position, transform.rotation);
                bullets[i].GetComponent<Bullet>().direction = transform.forward;
                bullets[i].GetComponent<Bullet>().Allign(faction);
                bullets[i].SetActive(false);
            }
        }
        sinceLastShot = shootRate;
    }
    //POLYMORPHISM
    protected override void ActionCondition()
    {
        Collider[] hits = Physics.OverlapBox(transform.position + transform.forward * 5, new Vector3(0.5f, 0.5f, 5),transform.rotation);
        
        if (hits.Length > 0)
        {
            Unit u;
            foreach (Collider c in hits)
            {
                u = c.gameObject.GetComponent<Unit>();
                if (u != null && u.Faction != faction)
                {
                    shooting = true;
                    return;
                }
            }
        }
        shooting = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + transform.forward * 5, new Vector3(1, 1, 10));
    }
}
