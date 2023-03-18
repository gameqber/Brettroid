using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multiviola : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody2D rb;
    private Vector2 direction;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        direction = new Vector2(1, 1).normalized;
    }

    void FixedUpdate() {
        // Move the enemy in the current direction
        rb.velocity = direction * speed;

        // Check for collisions with floors
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 0.5f, LayerMask.GetMask("Ground"));
        if (hit.collider != null) {
            // Change direction of movement
            Vector2 normal = hit.normal;
            direction = Vector2.Reflect(direction, normal);
        }
    }
}
