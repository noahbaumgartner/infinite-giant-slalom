using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{   
    public float speed = 10.0f;
    public Transform cameraTransform;
    public LoopingBackground loopingBackground;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;

    private int score = 0;
    private int highscore = 0;

    void Start()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    void FixedUpdate()
    {
        float input = Input.GetAxis("Horizontal");
        float moveSpeed = speed * Time.deltaTime;

        transform.rotation *= Quaternion.Euler(0, 0, input*2);
        Vector3 moveDirection = speed * Time.deltaTime * transform.up;
        transform.position -= moveDirection;

        cameraTransform.position = new Vector3(cameraTransform.position.x, transform.position.y, cameraTransform.position.z);

        loopingBackground.backgroundSpeed = moveDirection.y;
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        Debug.Log("Trigger entered");
        if (trigger.CompareTag("FAIL")) {
            Debug.Log("You lose");
            ReloadLevel();
        }

        if(trigger.CompareTag("PASS")) {
            score++;
            highscore++;
            scoreText.text = "Score: " + score.ToString();
        }
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
