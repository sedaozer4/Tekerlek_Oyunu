using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using Random = System.Random;

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
    public  bool isPause;
    public int stop;
    public float highscore;
    public Text scoreText;
    public GameObject highscorePanel;
    public Text highMetres;
    public Text placement;
    public static float road;
    public GameObject inGamePanel;
    public ParticleSystem confetti;
    public bool confettiActive;
    public GameObject soundIcon;
    void Start()
    {
      
        if (!PlayerPrefs.HasKey("pause"))
        {
            PlayerPrefs.SetFloat("pause", 0);
        }
        if (!PlayerPrefs.HasKey("highscore"))
        {
            PlayerPrefs.SetFloat("highscore", 0);
        }
        if (!PlayerPrefs.HasKey("place"))
        {
            PlayerPrefs.SetInt("place", 4000);
        }
        Time.timeScale = 1f;
        isGameStarted = false;
        altinNumarasi = 30;
        isTime = true;
        isPause = false;
        // metres=0;
        inGamePanel.SetActive(true);
        road = 0;
        confettiActive = false;
        PlayerPrefs.SetFloat("pause", 0);
    }
    void Update()
    {

        if (confettiActive == true)
        {
            confetti.Play();
        }
        else
        {
            confetti.Stop();
        }

        if (isTime == true&& isGameStarted==true && isPause==false)
        {
            altinNumarasi -=   1.1f*Time.fixedDeltaTime;
        //    metres += 0.5f * Time.fixedDeltaTime;
        }
          
        if ((int)altinNumarasi == 0&&isTime == true && isGameStarted == true )
        {
            OyuncuManager.gameOver = true;
            isTime = false;
            FindObjectOfType<SesYoneticisi>().PlaySound("OyunBitimi");
        }
        coinsText.text = (int)altinNumarasi + "s";
        metresText.text = Math.Round(road,2)  +" km" ;
        highMetres.text =  Math.Round(PlayerPrefs.GetFloat("highscore"), 2)+"km";

        if (gameOver){

            Time.timeScale = 0;
            isTime = false;
            highscore= PlayerPrefs.GetFloat("highscore");
            if (road > highscore)
            {
                confettiActive = true;
                PlayerPrefs.SetFloat("highscore", road);
                highscorePanel.SetActive(true);
                Random rnd = new Random();
                int place = rnd.Next(PlayerPrefs.GetInt("place")-100, PlayerPrefs.GetInt("place"));
                PlayerPrefs.SetInt("place", place);
                highscorePanel.SetActive(true);
                placement.text = PlayerPrefs.GetInt("place") + " - You";
                inGamePanel.SetActive(false);
                OyuncuManager.gameOver = false;
            }
            else
            {   
                inGamePanel.SetActive(false);
                gameOverPanel.SetActive(true);
                OyuncuManager.gameOver = false;
                scoreText.text = "Score: "+Math.Round(road, 2) + " km";
              
            }
            road = 0;
          
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
        soundIcon.SetActive(true);

    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPause = false;
        PlayerPrefs.SetInt("pause", 0);
        soundIcon.SetActive(false);
    }
    public void Home()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 0f;
        isPause = false;
        PlayerPrefs.SetInt("pause", 0);
        SceneManager.LoadScene("Menu");
        road = 0;
    }

}
