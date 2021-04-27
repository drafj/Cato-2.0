using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minime : MonoBehaviour
{
    [SerializeField] private float cadence;
    [SerializeField] private Pooler mPool;
    void Start()
    {
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            Vector3 pos = new Vector3(transform.position.x, transform.position.y + 1);
            mPool.Spawner(pos, transform.rotation);
            yield return new WaitForSeconds(cadence);
        }
    }

    void Update()
    {
        
    }
}
