using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartHealer : MonoBehaviour
{
    [SerializeField] private int lifeRef = 0;
    [SerializeField] private Animator anim = new Animator();
    private int life;

    private void OnEnable()
    {
        life = lifeRef;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BulletController bulletController))
        {
            life -= 5;
            if (life <= 0)
            {
                transform.parent.GetComponent<Heart>().CheckIfStopHeal();
                anim.SetTrigger("Death");
            }
        }
    }

    public void Die()
    {
        transform.parent.GetComponent<Heart>().CheckIfStopHealAbility();
        gameObject.SetActive(false);
    }
}
