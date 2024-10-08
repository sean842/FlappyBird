using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMoveScript : MonoBehaviour
{

    public float moveSpeed;
    public float deadZone = -45;

    private LogicManagerScript logicManager;

    private void Start() {
        // Find the LogicManagerScript
        logicManager = FindObjectOfType<LogicManagerScript>();

        // Register this pipe with the LogicManagerScript
        logicManager.RegisterPipe(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.left*moveSpeed) * Time.deltaTime;   

        // Destroy the pipe when it goes past the deadZone
        if (transform.position.x < deadZone) {
            // Unregister this pipe before destroying
            logicManager.UnregisterPipe(gameObject);
            Destroy(gameObject);
        }
    }

    // Method to set the pipe's speed from the LogicManagerScript
    public void SetSpeed(float newSpeed) {
        moveSpeed = newSpeed;
    }

}
