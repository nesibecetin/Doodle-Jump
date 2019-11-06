using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Controller : MonoBehaviour
{
    public void Play_Button()
    {
        SceneManager.LoadScene(1);
    }
    public void Exit_Button()
    {
        Application.Quit();
    }
}
