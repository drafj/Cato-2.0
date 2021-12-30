using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    public GameObject menu;
    public GameObject zoomCamera;
    public GameObject SceneToLoadGO;
    public Toggle musicToggle;
    public Dropdown dropdown;
    public TMP_Dropdown primaryDropdown;
    public TMP_Dropdown secondaryDropdown;
    public TextMeshProUGUI points;
    public static int selection;
    public static bool blockPause;
    public bool firstValueChaged;
    public List<string> abilitiesString;
    public List<string> gunListReference;
    public List<string> primary;
    public List<string> secondary;
    [SerializeField] private TMP_Dropdown.OptionData tempValue;
    [SerializeField]
    private AudioClip pauseAudio = null,
        buttonAudio = null;
    [SerializeField] private List<GameObject> visualGuns = new List<GameObject>();


    void Start()
    {
        SetPoints();
        blockPause = false;
        abilitiesString = new List<string>() { "Shield", "Flash", "Minime"};
        gunListReference = new List<string>() { "PERFORANTE", "LASER", "PLASMA", "VENENO" };
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
                    if (gunListReference[i] != primaryDropdown.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text)
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
                    if (gunListReference[i] != secondaryDropdown.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text)
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

        if (type == 1)
        {
            switch (primaryDropdown.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text)
            {
                case "PERFORANTE":
                    for (int i = 0; i < visualGuns.Count; i++)
                    {
                        visualGuns[0].SetActive(true);
                        if (i != 0)
                            visualGuns[i].SetActive(false);
                    }
                    break;
                case "LASER":
                    for (int i = 0; i < visualGuns.Count; i++)
                    {
                        visualGuns[1].SetActive(true);
                        if (i != 1)
                            visualGuns[i].SetActive(false);
                    }
                    break;
                case "PLASMA":
                    for (int i = 0; i < visualGuns.Count; i++)
                    {
                        visualGuns[2].SetActive(true);
                        if (i != 2)
                            visualGuns[i].SetActive(false);
                    }
                    break;
                case "VENENO":
                    for (int i = 0; i < visualGuns.Count; i++)
                    {
                        visualGuns[3].SetActive(true);
                        if (i != 3)
                            visualGuns[i].SetActive(false);
                    }
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (secondaryDropdown.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text)
            {
                case "PERFORANTE":
                    for (int i = 0; i < visualGuns.Count; i++)
                    {
                        visualGuns[0].SetActive(true);
                        if (i != 0)
                            visualGuns[i].SetActive(false);
                    }
                    break;
                case "LASER":
                    for (int i = 0; i < visualGuns.Count; i++)
                    {
                        visualGuns[1].SetActive(true);
                        if (i != 1)
                            visualGuns[i].SetActive(false);
                    }
                    break;
                case "PLASMA":
                    for (int i = 0; i < visualGuns.Count; i++)
                    {
                        visualGuns[2].SetActive(true);
                        if (i != 2)
                            visualGuns[i].SetActive(false);
                    }
                    break;
                case "VENENO":
                    for (int i = 0; i < visualGuns.Count; i++)
                    {
                        visualGuns[3].SetActive(true);
                        if (i != 3)
                            visualGuns[i].SetActive(false);
                    }
                    break;
                default:
                    break;
            }
        }
    }

    public void GunsSelected()
    {
        switch (primaryDropdown.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text)
        {
            case "PERFORANTE":
                PlayerPrefs.SetString("Primary", "PIERCING");
                break;
            case "LASER":
                PlayerPrefs.SetString("Primary", "LASER");
                break;
            case "PLASMA":
                PlayerPrefs.SetString("Primary", "PLASMA");
                break;
            case "VENENO":
                PlayerPrefs.SetString("Primary", "VENOM");
                break;
            default:
                break;
        }

        switch (secondaryDropdown.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text)
        {
            case "PERFORANTE":
                PlayerPrefs.SetString("Secondary", "PIERCING");
                break;
            case "LASER":
                PlayerPrefs.SetString("Secondary", "LASER");
                break;
            case "PLASMA":
                PlayerPrefs.SetString("Secondary", "PLASMA");
                break;
            case "VENENO":
                PlayerPrefs.SetString("Secondary", "VENOM");
                break;
            default:
                break;
        }
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
