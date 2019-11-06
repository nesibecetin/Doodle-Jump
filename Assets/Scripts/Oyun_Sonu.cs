using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Oyun_Sonu : MonoBehaviour
{
    public GameObject panel;
    public Text h_score;

    private void Start()
    {
        h_score.text = "High Score:" + PlayerPrefs.GetInt("High_Score").ToString();
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        panel.SetActive(true);
        Time.timeScale = 0;

        if (PlayerPrefs.GetInt("Score") > PlayerPrefs.GetInt("High_Score"))
        {
            PlayerPrefs.SetInt("High_Score", PlayerPrefs.GetInt("Score"));
            h_score.text = "High Score:" + PlayerPrefs.GetInt("High_Score").ToString();
        }
        
    }

    public void tekrar()
    {
        SceneManager.LoadScene("Main");
        Time.timeScale = 1;
    }
    public void ana_menu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }
}
