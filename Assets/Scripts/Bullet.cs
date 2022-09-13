using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float shootSpeed;

    private GameObject primaryTarget;

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
    }
    private void HitCheck()
    {
        
    }
}
