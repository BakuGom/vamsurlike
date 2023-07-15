using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("# Game Control")]
    private bool isLive;
    public float maxGameTime = 30 * 60;
    public float currentTime = 0;
    public int killScore = 0;
    [SerializeField]
    private TextMeshProUGUI timeScoreText;
    [SerializeField]
    private TextMeshProUGUI killScoreText;
    public GameObject CoverImage;
    public GameObject MenuPanel;
    public GameObject PauseButton;
    public GameObject ResumeButton;
    [SerializeField]
    public GameObject SkillOption;
    private void Update()
    {
        if (currentTime < maxGameTime)
        {
            currentTime += Time.deltaTime;
            UpdateTimeScoreText();
        }
    }
    private void Awake()
    {
        Instance = this;
        UpdateTimeScoreText();
    }
    private void UpdateTimeScoreText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        string timeText = string.Format("{0:00}:{1:00}", minutes, seconds);
        timeScoreText.text = "" + timeText;
    }
    public void UpdateKillScoreText()
    {
        killScore++;
        killScoreText.text = "" + killScore;
    }
    public async void OnClickSignInButton()
    {
        await Task.Delay(500);
        if (UserManager.check==true)
        {
            CoverImage.SetActive(false);
            SceneManager.LoadScene("InGame");
        }
    }
    public void OnClickSignUpButton()
    {

    }
    public void OnTogglePauseButton()
    {
        if (Time.timeScale == 0) //¸ØÃçÀÖÀ¸¸é
        {
            Time.timeScale = 1f; //½ÃÀÛ
            MenuPanel.SetActive(false);
            PauseButton.SetActive(true);
            ResumeButton.SetActive(false);
        }
        else //¿òÁ÷ÀÌ¸é
        {
            Time.timeScale = 0; //¸ØÃß±â
            MenuPanel.SetActive(true);
            PauseButton.SetActive(false);
            ResumeButton.SetActive(true);
        }
    }
    

}