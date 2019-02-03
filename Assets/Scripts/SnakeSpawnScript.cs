using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeSpawnScript : MonoBehaviour
{
    [SerializeField]
    GameObject snakePrefab = null;

    [SerializeField]
    float initialSpawnFrequency = 30f;

    float spawnFrequency;

    Transform thisTransform;

    float timer;

    // Start is called before the first frame update
    void Start()
    {
        thisTransform = GetComponent<Transform>();
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= spawnFrequency)
        {
            timer = 0f;
            spawnFrequency = initialSpawnFrequency * Random.Range(0.5f, 1.5f);

            Instantiate(snakePrefab, thisTransform.position, thisTransform.rotation);
        }
        else
            timer += 0.1f;
    }
}
