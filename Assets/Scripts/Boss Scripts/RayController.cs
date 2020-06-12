using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayController : MonoBehaviour
{
    public int velocity;

    void Start()
    {
        
    }

    void  FixedUpdate()
    {
        transform.position += transform.right * velocity * Time.deltaTime;
    }
}
