using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpwanScript : MonoBehaviour
{

    public GameObject Cloud;
    public float spawnRate = .5f; //how meny sec it will spawn.
    private float timer = 0;
    public float heightOffset = 20;

    // New size range variables for cloud scaling
    public float minCloudSize = 0.2f;
    public float maxCloudSize = 2f;

    // Start is called before the first frame update
    void Start()
    {
        SpawnCloud();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate) {
            timer += Time.deltaTime;// count everyframe and will work the same in every computer.
        }
        else {
            SpawnCloud();
            timer = 0;
        }

    }


    //void SpawnCloud2() {
    //    float lowestPoint = transform.position.y - heightOffset; 
    //    float highestPoint = transform.position.y + heightOffset;

    //    Instantiate(Cloud, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
    //}


    void SpawnCloud() {
        // Randomly pick a position for the cloud within the height offset
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;

        Vector3 spawnPosition = new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 1);

        // Instantiate the cloud
        GameObject newCloud = Instantiate(Cloud, spawnPosition, transform.rotation);

        // Apply random scaling to the cloud (uniform scaling in x, y, z)
        float randomScale = Random.Range(minCloudSize, maxCloudSize);
        newCloud.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
    }

}
