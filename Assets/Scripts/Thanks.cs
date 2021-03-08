using UnityEngine;
using UnityEngine.SceneManagement;

public class Thanks : MonoBehaviour
{
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(0);
        }
    }
}
