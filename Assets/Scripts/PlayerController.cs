using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float initialSpeed = 10.0f;
    public float speedIncreaseRate = 0.1f;
    public Transform cameraTransform;
    public LoopingBackground loopingBackground;
    public TextMeshProUGUI scoreText;
    public AudioSource gateSound;

    private Animator animator;
    private int score = 0;
    private float moveX;
    private bool isCrashed = false;
    private float timeSinceCrash = 0.0f;
    private bool hasFailed = false;
    private float elapsedTime = 0.0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        scoreText.text = "Score: " + score.ToString();
    }

    void FixedUpdate()
    {
        if (isCrashed)
        {
            timeSinceCrash += Time.deltaTime;

            if (timeSinceCrash > 1.0f)
            {
                LoadMenu();
            }
            return;
        }

        elapsedTime += Time.deltaTime;
        float currentSpeed = initialSpeed + speedIncreaseRate * elapsedTime;

        transform.rotation *= Quaternion.Euler(0, 0, moveX * 2);
        Vector3 moveDirection = currentSpeed * Time.deltaTime * transform.up;
        transform.position -= moveDirection;

        cameraTransform.position = new Vector3(cameraTransform.position.x, transform.position.y, cameraTransform.position.z);
        loopingBackground.backgroundSpeed = moveDirection.y;
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.CompareTag("FAIL")) HandleFail();
        if (trigger.CompareTag("PASS") && !hasFailed) HandlePassedGate();
        if (hasFailed) ShowGameOverText();
    }

    void HandleFail()
    {
        animator.SetTrigger("Crashed");
        isCrashed = true;
        hasFailed = true;
    }

    void HandlePassedGate()
    {
        score++;
        scoreText.text = "Score: " + score.ToString();
        gateSound.Play();
    }

    void ShowGameOverText()
    {
        scoreText.text = "Game Over";
    }

    void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    void OnMove(InputValue value)
    {
        moveX = value.Get<Vector2>().x;
    }

    void OnDisable()
    {
        int highScore = PlayerPrefs.GetInt("highScore", 0);
        if (score > highScore)
        {
            PlayerPrefs.SetInt("highScore", score);
        }
    }
}
