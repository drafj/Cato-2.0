using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    int counter;

    public List<GameObject> arrows = new List<GameObject>();
    public List<GameObject> text = new List<GameObject>();

    public TutoState m_tuto;

    private void Start()
    {
        MenuController.blockPause = true;
    }

    public void TutoPass()
    {
        if (counter >= 6)
        {
            MenuController.blockPause = false;
            SceneManager.LoadScene(1);
        }

        m_tuto = (TutoState) counter;

        text[0].SetActive(false);

        switch (m_tuto)
        {
            case TutoState.pointLife:
                for (int i = 0; i < arrows.Count; i++)
                {
                    arrows[i].SetActive(false);
                    text[i].SetActive(false);
                    arrows[0].SetActive(true);
                    text[1].SetActive(true);
                }
                break;
            case TutoState.pointFood:
                for (int i = 0; i < arrows.Count; i++)
                {
                    arrows[i].SetActive(false);
                    text[i].SetActive(false);
                    arrows[1].SetActive(true);
                    text[2].SetActive(true);
                }
                break;
            case TutoState.pointPause:
                for (int i = 0; i < arrows.Count; i++)
                {
                    arrows[i].SetActive(false);
                    text[i].SetActive(false);
                    arrows[2].SetActive(true);
                    text[3].SetActive(true);
                }
                break;
            case TutoState.pointJoystick:
                for (int i = 0; i < arrows.Count; i++)
                {
                    arrows[i].SetActive(false);
                    text[i].SetActive(false);
                    arrows[3].SetActive(true);
                    text[4].SetActive(true);
                }
                break;
            case TutoState.pointShotB:
                for (int i = 0; i < arrows.Count; i++)
                {
                    arrows[i].SetActive(false);
                    text[i].SetActive(false);
                    arrows[4].SetActive(true);
                    text[5].SetActive(true);
                }
                break;
            case TutoState.pointAbilityB:
                for (int i = 0; i < arrows.Count; i++)
                {
                    arrows[i].SetActive(false);
                    text[i].SetActive(false);
                    arrows[5].SetActive(true);
                    text[6].SetActive(true);
                }
                break;
            default:
                break;
        }

        counter++;
    }

    void Update()
    {
        
    }
}

public enum TutoState {pointLife, pointFood, pointPause, pointJoystick, pointShotB, pointAbilityB }