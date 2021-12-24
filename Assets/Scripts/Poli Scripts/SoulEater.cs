using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulEater : MonoBehaviour
{
    [SerializeField] private float force = 150000;
    [SerializeField] private GameObject pushers = null;
    [SerializeField] private Animator anim = null;
    public bool bellyFull = false;
    private int spitCounter = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player) && !bellyFull)
        {
            pushers.SetActive(false);
            Eat(player);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            spitCounter++;
            if (spitCounter == 2)
            {
                bellyFull = false;
                spitCounter = 0;
            }
        }
    }

    void Eat(Player player)
    {
        anim.SetBool("Catch", true);
        bellyFull = true;
        player.GetComponent<Collider2D>().enabled = false;
        player.unleashed = true;
        player.silenced = true;
        player.stuned = true;
        player.inmobilized = true;
        player.rgbd.simulated = false;
        player.transform.parent = transform;
        player.transform.localPosition = new Vector2();
    }

    public void Spit()
    {
        anim.SetBool("Catch", false);
        Player player = FindObjectOfType<Player>();
        player.transform.rotation = Quaternion.Euler(0, 0, 0);
        player.transform.localPosition = new Vector2(0, -1);
        player.transform.parent = null;
        player.rgbd.simulated = true;
        if (transform.parent.rotation.eulerAngles.z == 0)
            player.rgbd.AddForce(Vector2.down * force * Time.fixedDeltaTime);
        else
            player.rgbd.AddForce(Vector2.up * force * Time.fixedDeltaTime);
        player.inmobilized = false;
        player.silenced = false;
        player.stuned = false;
        player.unleashed = false;
        player.GetComponent<Collider2D>().enabled = true;
        pushers.SetActive(true);
    }
}
