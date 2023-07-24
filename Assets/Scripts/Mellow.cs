using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mellow : MonoBehaviour
{
    public float speed;
    public float xRange;
    private Transform player;
    private Vector2 originalPosition;
    private bool diving = false;
    private bool ascending = false;
    private Vector2 diveTarget;
    private Vector2 ascendTarget;
    private float diveDirection;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Mathf.Abs(transform.position.x - player.position.x) < xRange && !diving && !ascending)
        {
            diving = true;
            diveTarget = new Vector2(player.position.x, player.position.y);
            diveDirection = Mathf.Sign(diveTarget.x - transform.position.x);
            ascendTarget = new Vector2(originalPosition.x + 2 * (diveTarget.x - originalPosition.x), originalPosition.y);
        }

        if (diving)
        {
            transform.position = Vector2.MoveTowards(transform.position, diveTarget, speed * Time.deltaTime);

            if(Vector2.Distance(transform.position, diveTarget) < 0.1f)
            {
                diving = false;
                ascending = true;
            }
        }
        else if (ascending)
        {
            transform.position = Vector2.MoveTowards(transform.position, ascendTarget, speed * Time.deltaTime);

            if (Mathf.Abs(transform.position.y - originalPosition.y) < 0.1f)
            {
                ascending = false;
                originalPosition = transform.position;
            }
        }
    }
}
