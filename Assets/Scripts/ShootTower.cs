using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTower : Unit
{
    [SerializeField] float shootRate;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform spawn;
    GameObject[] bullets = new GameObject[20];
    bool placed = false;

    float sinceLastShot;
    int curBullet = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (bullet.GetComponent<Bullet>() != null)
        {
            for (int i = 0; i < bullets.Length; i++)
            {
                bullets[i] = Instantiate(bullet, spawn.position, spawn.rotation);
                bullets[i].SetActive(false);
                bullets[i].GetComponent<Bullet>().direction = Vector3.up;
            }
        }
        else
        {
            Debug.Log("no viable bulelt");
        }
        sinceLastShot = shootRate;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!placed)
        {
            return;
        }
        sinceLastShot -= Time.deltaTime;
        if (sinceLastShot <= 0)
        {
            bullets[curBullet].SetActive(true);
            bullets[curBullet].transform.SetPositionAndRotation(spawn.position, spawn.rotation);
            curBullet++;
            curBullet %= 20;
            sinceLastShot = shootRate;
        }
        
    }
}
