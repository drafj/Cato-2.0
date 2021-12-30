using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartHealer : MonoBehaviour
{
    [SerializeField] private int lifeRef = 0;
    [SerializeField] private Animator anim = new Animator();
    public int life;

    private void OnEnable()
    {
        life = lifeRef;
        GetComponent<Collider2D>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BulletController bulletController))
        {
            life -= 5;
            if (life <= 0)
            {
                transform.parent.GetComponent<Heart>().CheckIfStopHeal();
                GetComponent<Collider2D>().enabled = false;
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
