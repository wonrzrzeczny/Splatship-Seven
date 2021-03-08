using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static int LastLevel { get; set; } = 1;


    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Continue()
    {
        SceneManager.LoadScene(LastLevel);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void OpenItch()
    {
        Application.OpenURL("https://wonrzrzeczny.itch.io/");
    }
}
