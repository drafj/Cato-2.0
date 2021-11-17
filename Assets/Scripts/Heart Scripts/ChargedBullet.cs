using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedBullet : Bullet
{
    [SerializeField] private Rigidbody2D rgbd;
    void Start()
    {
        
    }

    IEnumerator FirstStage()
    {
        while (true)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, GetRotToPlayer(), 2.5f * Time.deltaTime);
            rgbd.velocity = transform.up * 500 * Time.deltaTime;
            yield return null;
        }

    }

    void Update()
    {
        
    }
}
