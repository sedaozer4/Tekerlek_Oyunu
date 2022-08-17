using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Events : MonoBehaviour
{
  /* private void Start()
    {
        AdManager.instance.RequestInterstitial();
    }*/
    public void ReplayGame()
   {
       SceneManager.LoadScene("Level");
     
    }

   public void QuitGame()
   {
        Application.Quit();
   }
}
