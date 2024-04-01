using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{   
    public float speed = 10.0f;
    public Transform cameraTransform;
    public LoopingBackground loopingBackground;

    void FixedUpdate()
    {
        float input = Input.GetAxis("Horizontal");
        float moveSpeed = speed * Time.deltaTime;

        transform.rotation *= Quaternion.Euler(0, 0, input);
        Vector3 moveDirection = speed * Time.deltaTime * transform.up;
        transform.position -= moveDirection;

        cameraTransform.position = new Vector3(cameraTransform.position.x, transform.position.y, cameraTransform.position.z);

        loopingBackground.backgroundSpeed = moveDirection.y;
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        Debug.Log("Trigger entered");
        if (trigger.CompareTag("FAIL"))
        {
            Debug.Log("You lose");
            ReloadLevel();
        }
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
