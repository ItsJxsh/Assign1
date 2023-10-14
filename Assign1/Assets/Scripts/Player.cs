using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 0.0f;
    Rigidbody2D rb;
    public GameObject bombPrefab;
    public float explosionTime = 2.5f;
    public int bombCount = 1;
    private int bombsRemaining;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bombsRemaining = bombCount;
    }


    // Challenge: Rotate the player with E and Q, then move the player in that direction!
    void Update()
    {
        float dt = Time.deltaTime;
        float xDir = 0.0f;
        float yDir = 0.0f;

        if (Input.GetKeyDown(KeyCode.Space) && bombsRemaining > 0)
        {
            StartCoroutine(PlaceBomb());
        }
        if (Input.GetKey(KeyCode.W))
        {
            gameObject.GetComponent<AnimationScript>().idle = false;
            yDir = 1.0f;
        }

        else if (Input.GetKey(KeyCode.S))
        {
            gameObject.GetComponent<AnimationScript>().idle = false;
            yDir = -1.0f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            gameObject.GetComponent<AnimationScript>().idle = false;
            xDir = -1.0f;
        }

        else if (Input.GetKey(KeyCode.D))
        {
            gameObject.GetComponent<AnimationScript>().idle = false;
            xDir = 1.0f;
        }
        else
        {
            gameObject.GetComponent<AnimationScript>().idle = true;
        }


        Vector2 direction = new Vector2(xDir, yDir).normalized;
        rb.velocity = direction * speed;
    }


    private IEnumerator PlaceBomb()
    {
        Vector2 position = transform.position;
        GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity);
        bombsRemaining--;
        Debug.Log("Bomb Placed");

        yield return new WaitForSeconds(explosionTime);
        Destroy(bomb);
        bombsRemaining++;
    }
}