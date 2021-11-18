using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartHealer : MonoBehaviour
{
    [SerializeField] private int lifeRef;
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
                gameObject.SetActive(false);
            }
        }
    }
}
