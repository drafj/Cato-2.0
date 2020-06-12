using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject menu;
    public GameObject zoomCamera;
    public Toggle musicToggle;
    public Dropdown dropdown;
    public Text points;
    public static int selection;
    public int index;
    public static bool blockPause;
    public List<string> abilitiesString;

    void Start()
    {
        if (points != null)
            points.text = " POINTS: " + PlayerPrefs.GetInt("Points");
        blockPause = false;
        abilitiesString = new List<string>() { "Shield", "Flash"};

        if (dropdown != null)
        {
            dropdown.ClearOptions();
            dropdown.AddOptions(abilitiesString);
            dropdown.value = selection;
        }

        if (PlayerPrefs.HasKey("music"))
        {
            if (PlayerPrefs.GetInt("music") == 1 && GameManager.instance != null)
            {
                GameManager.instance.ambientSound.gameObject.SetActive(true);
                musicToggle.GetComponent<Toggle>().isOn = true;
            }
            else if (GameManager.instance != null)
            {
                GameManager.instance.ambientSound.gameObject.SetActive(false);
                musicToggle.GetComponent<Toggle>().isOn = false;
            }
        }
    }

    public void EnterPause()
    {
        if (!GameManager.instance.gameOver && !blockPause)
        {
            Time.timeScale = 0;
            GameManager.instance.pauseMenu.SetActive(true);
            GameManager.instance.pause = true;
        }
    }

    public void ExitPause()
    {
        Time.timeScale = 1;
        GameManager.instance.pause = false;
    }

    public void Exit()
    {
        Application.Quit();
    }

    //public void Play()
    //{
    //    SceneManager.LoadScene(1);
    //}

    public void TutorialButton()
    {
        SceneManager.LoadScene(2);
    }

    public void MusicController(bool controller)
    {
        GameManager.instance.ambientSound.gameObject.SetActive(controller);
        int musicPrefs;
        if (controller)
            musicPrefs = 1;
        else
            musicPrefs = 0;
        
        PlayerPrefs.SetInt("music", musicPrefs);
    }

    public void AbilitySelector(int index)
    {
        selection = index;
    }

    public void BackToMenu()
    {
        Time.timeScale = 1;
        StartCoroutine("MenuDelay");
        zoomCamera.SetActive(true);
        menu.SetActive(false);
    }

    IEnumerator MenuDelay()
    {
        GameManager.instance.ambientSound.GetComponent<AudioSource>().enabled = false;
        yield return new WaitForSeconds(1.85f);
        SceneManager.LoadScene(0);
    }


    void Update()
    {

    }
}
