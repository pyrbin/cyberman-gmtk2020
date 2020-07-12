using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public int IndexSceneToLoad;

    public void LoadScene()
    {
        SceneManager.LoadScene(IndexSceneToLoad);
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
}