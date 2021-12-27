using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject menu;
    public GameObject zoomCamera;
    public GameObject SceneToLoadGO;
    public Toggle musicToggle;
    public Dropdown dropdown;
    public Dropdown primaryDropdown;
    public Dropdown secondaryDropdown;
    public Text points;
    public static int selection;
    public int index;
    public static bool blockPause;
    public bool firstValueChaged;
    public List<string> abilitiesString;
    public List<string> gunListReference;
    public List<string> primary;
    public List<string> secondary;
    [SerializeField] private Dropdown.OptionData tempValue;
    [SerializeField]
    private AudioClip pauseAudio = null,
        buttonAudio = null;


    void Start()
    {
        SetPoints();
        blockPause = false;
        abilitiesString = new List<string>() { "Shield", "Flash", "Minime"};
        gunListReference = new List<string>() { "PIERCING", "LASER", "PLASMA", "VENOM" };
        primary = new List<string>();
        secondary = new List<string>();

        if (dropdown != null)
        {
            dropdown.ClearOptions();
            dropdown.AddOptions(abilitiesString);
            dropdown.value = selection;
        }

        if (SceneToLoadGO != null)
        SceneToLoadGO.GetComponent<Loading>().sceneToLoad = PlayerPrefs.GetInt("actualLevel", 1);

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

    public void GunSelection(int type = 1)
    {
        if (!firstValueChaged)
        {
            firstValueChaged = true;
            if (type == 1)
            {
                tempValue = secondaryDropdown.options[secondaryDropdown.value];
                secondaryDropdown.ClearOptions();
                secondary.Clear();
                for (int i = 0; i < gunListReference.Count; i++)
                {
                    if (gunListReference[i] != primaryDropdown.transform.GetChild(0).GetComponent<Text>().text)
                        secondary.Add(gunListReference[i]);
                }
                secondaryDropdown.AddOptions(secondary);
                for (int i = 0; i < secondaryDropdown.options.Count; i++)
                {
                    if (secondaryDropdown.options[i].text == tempValue.text)
                        secondaryDropdown.value = i;
                }
            }
            else
            {
                tempValue = primaryDropdown.options[primaryDropdown.value];
                primaryDropdown.ClearOptions();
                primary.Clear();
                for (int i = 0; i < gunListReference.Count; i++)///                               comparar si el label del primer dropdown es igual al del segundo dropdown
                {
                    if (gunListReference[i] != secondaryDropdown.transform.GetChild(0).GetComponent<Text>().text)
                        primary.Add(gunListReference[i]);
                }
                primaryDropdown.AddOptions(primary);
                for (int i = 0; i < primaryDropdown.options.Count; i++)
                {
                    if (primaryDropdown.options[i].text == tempValue.text)
                        primaryDropdown.value = i;
                }
            }
        }
        else
        {
            firstValueChaged = false;
        }
    }

    public void GunsSelected()
    {
        PlayerPrefs.SetString("Primary", primaryDropdown.transform.GetChild(0).GetComponent<Text>().text);
        PlayerPrefs.SetString("Secondary", secondaryDropdown.transform.GetChild(0).GetComponent<Text>().text);
    }

    public void SetPoints()
    {
        if (points != null)
            points.text = " POINTS: " + PlayerPrefs.GetInt("Points");
    }

    public void NextScene()
    {
        Player.bossPhase = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void EnterPause()
    {
        if (!GameManager.instance.gameOver && !blockPause)
        {
            AudioSource.PlayClipAtPoint(pauseAudio, Camera.main.transform.position);
            Time.timeScale = 0;
            GameManager.instance.player.GetComponent<Player>().anim.enabled = false;
            GameManager.instance.pauseMenu.SetActive(true);
            GameManager.instance.pause = true;
        }
    }

    public void ExitPause()
    {
        Time.timeScale = 1;
        AudioSource.PlayClipAtPoint(pauseAudio, Camera.main.transform.position);
        GameManager.instance.player.GetComponent<Player>().anim.enabled = true;
        GameManager.instance.pause = false;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ButtonSound()
    {
        Debug.Log("sound sonado");
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            AudioSource.PlayClipAtPoint(buttonAudio, Camera.main.transform.position);
            Time.timeScale = 0;
        }
        else
            AudioSource.PlayClipAtPoint(buttonAudio, Camera.main.transform.position);
    }

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
        ButtonSound();
        GameManager.instance.player.GetComponent<Collider2D>().enabled = false;
        Player.bossPhase = false;
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
}
