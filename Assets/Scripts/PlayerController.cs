using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{   
    public float speed = 10.0f;
    public Transform cameraTransform;
    public LoopingBackground loopingBackground;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;

    private Animator animator;
    private int score = 0;
    private int highscore = 0;
    private float moveX;
    private bool isCrashed = false;
    private float timeSinceCrash = 0.0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        scoreText.text = "Score: " + score.ToString();
    }

    void FixedUpdate()
    {
        if (isCrashed) {
            timeSinceCrash += Time.deltaTime;

            if (timeSinceCrash > 1.0f) {
                ReloadLevel();
            }
            return;
        }

        float moveSpeed = speed * Time.deltaTime;

        transform.rotation *= Quaternion.Euler(0, 0, moveX*2);
        Vector3 moveDirection = speed * Time.deltaTime * transform.up;
        transform.position -= moveDirection;

        cameraTransform.position = new Vector3(cameraTransform.position.x, transform.position.y, cameraTransform.position.z);
        loopingBackground.backgroundSpeed = moveDirection.y;
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.CompareTag("FAIL")) {
            animator.SetTrigger("Crashed");
            isCrashed = true;
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

    void OnMove(InputValue value)
    {
        moveX = value.Get<Vector2>().x;
    }
}
