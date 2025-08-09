using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void ViewCatFamily()
    {
        SceneManager.LoadSceneAsync(1); // AR Scene
    }

    public void PlayQuiz()
    {
        SceneManager.LoadSceneAsync(0); // Quiz Scene
    }

    public void Quit()
    {
        Application.Quit();
    }
}
