using UnityEngine.SceneManagement;
using UnityEngine;

public class AnaMenu : MonoBehaviour
{
    public void Oyna()
    {
        SceneManager.LoadScene("Level");
    }

    public void OyundanCik()
    {
        Application.Quit();
    }

}
