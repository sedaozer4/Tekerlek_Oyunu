using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class OyuncuManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;
    public static bool isGameStarted;
    public GameObject startingText;
    public static float altinNumarasi;
    public Text coinsText;
    public bool isTime = false;

    public int maximum;
    public int current;
    public Image mask;
    public int minimum;

    void Start()
    {
        gameOver = false;
        Time.timeScale = 1f;
        isGameStarted = false;
        altinNumarasi = 30;
        isTime = true;
    }
    void Update()
    {
        if (isTime == true&& isGameStarted==true)
        {
            altinNumarasi -=  0.5f * Time.fixedDeltaTime;
          
        }
          
        if ((int)altinNumarasi == 0)
        {
            OyuncuManager.gameOver = true;
            isTime = false;
            FindObjectOfType<SesYoneticisi>().PlaySound("OyunBitimi");
        }
        coinsText.text =  (int)altinNumarasi +"s";
       

        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
            isTime = false;
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
}
