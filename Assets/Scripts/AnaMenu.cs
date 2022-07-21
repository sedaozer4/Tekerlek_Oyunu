using UnityEngine.SceneManagement;
using UnityEngine;

public class AnaMenu : MonoBehaviour
{
    private void Start()
    {
       
    }
    public void Oyna()
    {
        SceneManager.LoadScene("Level");
    }
    public void Home()
    {
        SceneManager.LoadScene("Menu");
    }
    public void OyundanCik()
    {
        PlayerPrefs.SetInt("pause", 0);
        Application.Quit();
    }

}
