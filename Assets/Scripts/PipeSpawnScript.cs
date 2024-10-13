using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawnScript : MonoBehaviour
{

    public GameObject Pipe;
    public float spawnRate = 3; //how meny sec it will spawn.
    private float timer = 0;
    public float heightOffset = 10;

    private LogicManagerScript logicManager; // Reference to the LogicManager
    // Start is called before the first frame update
    void Start()
    {
        // Find and store a reference to the LogicManager
        logicManager = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManagerScript>();
        //SpawnPipe();

    }

    // Update is called once per frame
    void Update()
    {
        
        //if (timer < spawnRate)
        //{
        //    timer += Time.deltaTime;// count everyframe and will work the same in every computer.
        //}
        //else
        //{
        //    SpawnPipe();
        //    timer = 0;
        //}


        // Only spawn pipes if the game has started
        if (logicManager.currentTime == 0) {
            if (timer < spawnRate) {
                timer += Time.deltaTime; // count every frame
            }
            else {
                SpawnPipe();
                timer = 0;
            }
        }
    }

    void SpawnPipe()
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;

        Instantiate(Pipe, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
    }

    // Method to dynamically adjust the spawn rate
    public void SetSpawnRate(float newRate) {
        spawnRate = newRate;
    }


}
