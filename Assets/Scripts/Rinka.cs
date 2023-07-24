using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rinka : MonoBehaviour
{
    public float speed;
    private Vector2 direction;

    private float leftBound;
    private float rightBound;
    private float upperBound;
    private float lowerBound;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            direction = (player.transform.position - transform.position).normalized;
        }
        else
        {
            direction = Vector2.right;
        }

        float camHalfWidth = Camera.main.aspect * Camera.main.orthographicSize;
        float camHalfHeight = Camera.main.orthographicSize;

        leftBound = Camera.main.transform.position.x - camHalfWidth - 1;
        rightBound = Camera.main.transform.position.x + camHalfWidth + 1;
        lowerBound = Camera.main.transform.position.y - camHalfHeight - 1;
        upperBound = Camera.main.transform.position.y + camHalfHeight + 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime;

        if (transform.position.x < leftBound || transform.position.x > rightBound ||
            transform.position.y < lowerBound || transform.position.y > upperBound)
        {
            Destroy(gameObject);
        }
    }
}
