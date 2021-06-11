using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBoss : MonoBehaviour
{
    float velocity;
    Rigidbody2D rgbd;
    displacement m_displacement;

    void Start()
    {
        
    }

    void BossDisplacement()
    {
        if (transform.position.y <= 9.5f)
        {
            if (transform.position.x >= 0)
            {
                m_displacement = displacement.L;
            }
            else if (transform.position.x <= -1.26f)
            {
                m_displacement = displacement.R;
            }

            if (m_displacement == displacement.R)
            {
                rgbd.AddForceAtPosition(transform.right * (-velocity - 15), new Vector2(0, 9.5f));
            }
            else if (m_displacement == displacement.L)
            {
                rgbd.AddForceAtPosition(transform.right * (velocity + 15), new Vector2(-1.26f, 9.5f));
            }
        }
    }

    void Update()
    {
        
    }
}
