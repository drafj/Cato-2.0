using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBoss : MonoBehaviour
{
    public Rigidbody2D rgbd;
    float velocity;
    displacement m_displacement;

    void Start()
    {
        
    }

    void BossDisplacement()
    {
        if (transform.position.y <= 9.5f)
        {
            if (transform.position.x >= 5.18f)
            {
                m_displacement = displacement.L;
                Debug.Log("+verificando si es mayor a 5");
            }
            else if (transform.position.x <= -5.18f)
            {
                m_displacement = displacement.R;
                Debug.Log("-verificando si es menor a 5");
            }

            if (m_displacement == displacement.R)
            {
                rgbd.AddForce(transform.right * (velocity + 15));
            }
            else if (m_displacement == displacement.L)
            {
                rgbd.AddForce(transform.right * (velocity - 15));
            }
        }
        else
            rgbd.AddForce(transform.up * (velocity - 15));
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        BossDisplacement();
    }
}
