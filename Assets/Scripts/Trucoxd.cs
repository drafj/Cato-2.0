using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trucoxd : MonoBehaviour
{
    public int counter;

    public void CounterPlus()
    {
        counter++;
    }

    void Update()
    {
        if (counter >= 5)
        {
            counter = 0;
            GameManager.instance.player.GetComponent<Player>().thirdAmmo = 999999;
        }
    }
}
