using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Nearsight Mode")]

    public bool gamemodeNearsight;

    public int maxTime;
    public float currentTime;
    public bool timerActive;

    [Header("Farsight Mode")]
    public bool gamemodeFarsight;

    public AudioSource pickUp;

    public bool hasGreenJogHurt;
    public bool hasSanta;
    public bool hasApple;
    public bool hasWindex;

    public bool hasPepper;
    public bool hasBlueJogHurt;

    [Header("Hard of hearing Mode")]

    public bool gamemodeHardofHearing;

    public bool boughtTheMilk;

    public GameObject goHome;

    [Header("General Stuff")]

    public GameObject introPanel;
    public GameObject pausePanel;
    public GameObject defeatScreen;
    public GameObject victoryScreen;

    public TextMeshProUGUI timerText;

    public bool pauseMode;

    public bool victory;
    public bool defeat;

    // Start is called before the first frame update
    void Start()
    {
        introPanel.SetActive(true);
        currentTime = maxTime;
        timerActive = false;
        victory = false;
        defeat = false;
        pauseMode = false;
        pickUp = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gamemodeNearsight == true)
        {
            boughtTheMilk = true;
            hasApple = true;
            hasGreenJogHurt = true;
            hasSanta = true;
            hasWindex = true;
        }

        if (gamemodeFarsight == true)
        {
            boughtTheMilk = true;
        }

        if (gamemodeHardofHearing == true)
        {
            hasApple = true;
            hasGreenJogHurt = true;
            hasSanta = true;
            hasWindex = true;
        }

        if (gamemodeNearsight == true && timerActive == false)
        {
            Time.timeScale = 0;
        }

        if (gamemodeFarsight == true && timerActive == false)
        {
            Time.timeScale = 0;
        }

        if (gamemodeHardofHearing == true && timerActive == false)
        {
            Time.timeScale = 0;
        }

        if (timerActive == true)
        {
            currentTime -= Time.deltaTime;
            Timer(currentTime);
        }

        if (currentTime <= 0 && victory == false)
        {
            timerActive = false;
            defeat = true;
        }

        if (defeat == true)
        {
            defeatScreen.SetActive(true);
        }

        if (victory == true)
        {
            victoryScreen.SetActive(true);
        }
    }


    public void NearSightBegin()
    {
        timerActive = true;
        Time.timeScale = 1;
        introPanel.SetActive(false);
        gamemodeNearsight = true;

    }

    public void FarSightBegin()
    {
        timerActive = true;
        Time.timeScale = 1;
        introPanel.SetActive(false);
        gamemodeFarsight = true;
    }

    public void HardOfHearingBegin()
    {
        timerActive = true;
        Time.timeScale = 1;
        introPanel.SetActive(false);
        gamemodeHardofHearing = true;
    }

    public void pauseMenuEnable()
    {
        Debug.Log("Enabling Pause Menu");
        pausePanel.SetActive(true);
        timerActive = false;
        Time.timeScale = 0;
        pauseMode = true;
    }

    public void pauseMenuDisable()
    {
        pausePanel.SetActive(false);
        timerActive = true;
        Time.timeScale = 1;
        pauseMode = false;
    }

    public void Timer(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void Inventory(int ID)
    {
        if (ID == 1 && hasGreenJogHurt == false)
        {
            hasGreenJogHurt = true;
            pickUp.Play();
        }
        if (ID == 2)
        {
            hasSanta = true;
            pickUp.Play();
        }
        if (ID == 3)
        {
            hasApple = true;
            pickUp.Play();
        }
        if (ID == 4)
        {
            hasWindex = true;
            pickUp.Play();
        }
    }
}
