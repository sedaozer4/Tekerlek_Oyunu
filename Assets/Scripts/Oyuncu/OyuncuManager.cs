using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class OyuncuManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;
    public static bool isGameStarted;
    public GameObject startingText;
    public static float altinNumarasi;
    public Text coinsText;
    public Text metresText;
    public bool isTime;
    public static float metres;
    public int maximum;
    public int current;
    public Image mask;
    public int minimum;
    public GameObject pauseMenu;
    public bool isPause;
    public int stop;
    public float highscore;
    public float score;
    public GameObject highscoreText;
    void Start()
    {
        if (!PlayerPrefs.HasKey("highscore"))
        {
            PlayerPrefs.SetFloat("highscore", 0);
        }

       
        Time.timeScale = 1f;
        isGameStarted = false;
        altinNumarasi = 30;
        isTime = true;
        isPause = false;
        // metres=0;
       
    }
    void Update()
    {

        if (isTime == true&& isGameStarted==true && isPause==false)
        {
            altinNumarasi -=  0.5f * Time.fixedDeltaTime;
            metres += 0.5f * Time.fixedDeltaTime;
        }

          
        if ((int)altinNumarasi == 0)
        {
            OyuncuManager.gameOver = true;
            isTime = false;
            FindObjectOfType<SesYoneticisi>().PlaySound("OyunBitimi");
        }
        coinsText.text =  (int)altinNumarasi +"s";
        metresText.text = Math.Round(metres, 1)  +"m" ;

        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
            isTime = false;
            highscore= PlayerPrefs.GetFloat("highscore");
            if (metres > highscore)
            {
                PlayerPrefs.SetFloat("highscore", metres);
                highscoreText.SetActive(true);
             
            }
            metres = 0;
            OyuncuManager.gameOver = false;
        }

        if(SwipeManager.tap)
        {
            isGameStarted = true;
            Destroy(startingText);
        }
        GetCurrentFill();
    }
    void GetCurrentFill()
    {
        float currentOffset = (int)altinNumarasi - minimum;
        float maximumOffset = maximum - minimum;
        float fillAmount = currentOffset / maximumOffset;
        mask.fillAmount = fillAmount;
    }
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPause = true;
        PlayerPrefs.SetInt("pause", 1);
       
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPause = false;
        PlayerPrefs.SetInt("pause", 0);
    }
    public void Home()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 0f;
        isPause = false;
        PlayerPrefs.SetInt("pause", 0);
        SceneManager.LoadScene("Menu");
        metres=0;
    }

}
