using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> CloudAssests = new List<GameObject>();

    [Header("Time Attrivuts")]
    [SerializeField] float randomTimerStart = 0.2f;
    [SerializeField] float randomTimerStop = 0.4f;
    float timer;
    float currentTimer = 0f;

    [Header("CloudCounter")]
    [SerializeField] int cloudToSpawn;
    int currentCloud;

    [Header("Offset")]
    [SerializeField] float yOffSet;

    int maxCloud;

    void Start()
    {
        maxCloud = CloudAssests.Count;
        timer = Random.Range(randomTimerStart, randomTimerStart);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTimer >= timer)
        {
            SpawnCloud();
        }
        currentTimer += Time.deltaTime;
    }

    void SpawnCloud()
    {
        currentCloud = (int)Random.Range(1, cloudToSpawn);

        for(int i = 0; i < currentCloud; i++)
        {
            int randomCloud = Random.Range(0, maxCloud);
            float randomYOffSet = Random.Range(-yOffSet, yOffSet);

            Vector3 position = new Vector3(transform.position.x, transform.position.y - randomYOffSet, transform.position.z);

            Instantiate(CloudAssests[randomCloud], position, Quaternion.identity);

        }

        timer = Random.Range(randomTimerStart, randomTimerStart);
        currentTimer = 0f;

    }
}
