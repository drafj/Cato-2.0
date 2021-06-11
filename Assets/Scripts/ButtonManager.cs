using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void ShotEnabler()
    {
        GameManager.instance.player.GetComponent<Player>().OnShooting = true;
    }

    public void AbilityEnabler()
    {
        GameManager.instance.player.GetComponent<Player>().ability = true;
        GameManager.instance.player.GetComponent<Player>().UseAbility();
    }

    public void ShotDisabler()
    {
        GameManager.instance.player.GetComponent<Player>().OnShooting = false;
    }

    public void AbilityDisabler()
    {
        GameManager.instance.player.GetComponent<Player>().ability = false;
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