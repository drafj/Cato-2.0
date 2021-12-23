using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minime : MonoBehaviour
{
    [SerializeField] private BulletsPooler mPool = null;
    [SerializeField] private Transform gap = null;
    [HideInInspector] public bool catchable;

    private void Awake()
    {
        mPool = FindObjectOfType<BulletsPooler>();
    }

    private void OnEnable()
    {
        catchable = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null && catchable)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            catchable = true;
        }
    }

    public void Shoot()
    {
        if (mPool.QueueFilled())
        {
            mPool.SpawnMinimeB(gap.position, transform.rotation);
        }
    }
}
