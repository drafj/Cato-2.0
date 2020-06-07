using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public Animator anim;

    public void Attack()
    {
        anim.SetTrigger("attack");
    }

    public void Growl()
    {
        anim.SetTrigger("rawr");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            anim.SetTrigger("attack");
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            anim.SetTrigger("rawr");
        }
    }
}
