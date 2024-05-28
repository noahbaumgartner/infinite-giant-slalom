using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public Scene gameScene;
    public TextMeshProUGUI highScoreText;

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Reset()
    {
        PlayerPrefs.SetInt("highScore", 0);
        highScoreText.text = "High Score: 0";
    }

    void OnEnable()
    {
        int highScore = PlayerPrefs.GetInt("highScore", 0);
        highScoreText.text = "High Score: " + highScore.ToString();
    }
}
