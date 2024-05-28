using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateSpawner : MonoBehaviour
{
    public GameObject player;
    public GameObject redGates;
    public GameObject blueGates;

    public float minRedX;
    public float maxRedX;
    public float minBlueX;
    public float maxBlueX;
    public float distanceBetweenGates;

    private float spawnY;

    private enum colors
    {
        red,
        blue
    }

    private colors currentColor;

    void Start()
    {
        spawnY = player.transform.position.y;
    }

    void Update()
    {
        Debug.Log(player.transform.position.y - spawnY);
        if (player.transform.position.y - spawnY < distanceBetweenGates)
        {
            Spawn();
            spawnY = player.transform.position.y;
        }
    }

    void Spawn()
    {
        if (currentColor == colors.red)
        {
            float randomRedX = Random.Range(minRedX, maxRedX);

            Instantiate(redGates, new Vector3(randomRedX, player.transform.position.y - 30, 0), Quaternion.identity);
            currentColor = colors.blue;
        }
        else
        {
            float randomBlueX = Random.Range(minBlueX, maxBlueX);

            Instantiate(blueGates, new Vector3(randomBlueX, player.transform.position.y - 30, 0), Quaternion.identity);
            currentColor = colors.red;
        }
    }
}
