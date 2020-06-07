using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Animator anim;

    public string nombreCaminar;
    public string nombreAbrir;

    public bool Caminar;

    void Start()
    {
        
    }

    void Abrir()
    {
        anim.SetTrigger(nombreAbrir);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Abrir();
        }

        if (Input.GetKey(KeyCode.W))
        {
            Caminar = true;
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            Caminar = false;
        }

        anim.SetBool(nombreCaminar, Caminar);
    }
}
