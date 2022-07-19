using UnityEngine.SceneManagement;
using UnityEngine;

public class AnaMenu : MonoBehaviour
{
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
        Application.Quit();
    }

}
