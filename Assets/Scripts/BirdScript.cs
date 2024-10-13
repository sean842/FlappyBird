using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{

    public Rigidbody2D RB;
    public float flapStrangth = 10;
    public LogicManagerScript Logic;
    public bool isBirdAlive = true;
    public float deadZone = -45;


    // Start is called before the first frame update
    void Start()
    {
        Logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) == true && isBirdAlive == true)
        {
            RB.velocity = Vector2.up * flapStrangth;
        }

        // Destroy the bird when it goes past the deadZone
        if (transform.position.y < deadZone) {
            Destroy(gameObject);
            Logic.GameOver();

        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the bird is colliding with the platform before the game starts
        if (collision.gameObject.CompareTag("Platform") ) {
            return;
        }
        Logic.GameOver();
        isBirdAlive = false;
    }


}
