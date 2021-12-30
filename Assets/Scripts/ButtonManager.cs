using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    private Sprite
        piercing = null,
        laser = null,
        plasma = null,
        venom = null;

    private void Start()
    {
        SetCannonSprites(0);
    }

    public void ShotEnabler()
    {
        GameManager.instance.player.GetComponent<Player>().onShooting = true;
    }

    public void AbilityEnabler()
    {
        GameManager.instance.player.GetComponent<Player>().UseAbility();
    }

    public void ShotDisabler()
    {
        GameManager.instance.player.GetComponent<Player>().onShooting = false;
    }

    public void ChangeGun()
    {
        Player player = GameManager.instance.player.GetComponent<Player>();
        if (!player.silenced)
        {
            if (player.gunz == (Gunz)System.Enum.Parse(typeof(Gunz), player.config[0]))
            {
                player.gunz = (Gunz)System.Enum.Parse(typeof(Gunz), player.config[1]);
                SetCannonSprites(1);
            }
            else
            {
                player.gunz = (Gunz)System.Enum.Parse(typeof(Gunz), player.config[0]);
                SetCannonSprites(0);
            }
        }
    }

    public void SetCannonSprites(int chose)
    {
        Player player = GameManager.instance.player.GetComponent<Player>();
        if (player.config[chose] == "PLASMA")
        {
            player.rCAnim.gameObject.SetActive(false);
            player.lCAnim.gameObject.SetActive(false);
            player.mCAnim.gameObject.SetActive(true);
        }
        else
        {
            player.rCAnim.gameObject.SetActive(true);
            player.lCAnim.gameObject.SetActive(true);
            player.mCAnim.gameObject.SetActive(false);
        }

        if (player.config[chose] == "PIERCING")
        {
            player.rCAnim.GetComponent<SpriteRenderer>().sprite = piercing;
            player.lCAnim.GetComponent<SpriteRenderer>().sprite = piercing;
        }
        else if (player.config[chose] == "LASER")
        {
            player.rCAnim.GetComponent<SpriteRenderer>().sprite = laser;
            player.lCAnim.GetComponent<SpriteRenderer>().sprite = laser;
            player.rCAnim.GetComponent<SpriteRenderer>().flipX = true;
            player.lCAnim.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (player.config[chose] == "PLASMA")
        {
            player.mCAnim.GetComponent<SpriteRenderer>().sprite = plasma;
        }
        else if (player.config[chose] == "VENOM")
        {
            player.rCAnim.GetComponent<SpriteRenderer>().sprite = venom;
            player.lCAnim.GetComponent<SpriteRenderer>().sprite = venom;
            player.lCAnim.GetComponent<SpriteRenderer>().flipX = true;
            player.rCAnim.GetComponent<SpriteRenderer>().flipX = false;
        }
    }


    /*IEnumerator ShootEnabler()
    {
        GameManager.instance.player.GetComponent<Player>().OnShooting = true;
        yield return new WaitForSeconds(0.1f);
        GameManager.instance.player.GetComponent<Player>().OnShooting = false;
    }

    IEnumerator AbilityEnabler()
    {
        GameManager.instance.player.GetComponent<Player>().ability = true;
        yield return new WaitForSeconds(0.1f);
        GameManager.instance.player.GetComponent<Player>().ability = false;
    }*/

    /*private void OnMouseDown()
    {
        switch (m_buttonType)
        {
            case ButtonType.Shot:
                GameManager.instance.player.GetComponent<Player>().OnShooting = true;
                break;
            case ButtonType.Ability:
                StartCoroutine("AbilityEnabler");
                break;
        }
    }



    private void OnMouseUp()
    {
        switch (m_buttonType)
        {
            case ButtonType.Shot:
                GameManager.instance.player.GetComponent<Player>().OnShooting = false;
                break;
            case ButtonType.Ability:
                break;
        }
    }*/
}

public enum ButtonType {Shot, Ability}