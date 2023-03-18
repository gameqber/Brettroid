using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Zeb : MonoBehaviour
{
    public GameObject player;
    public float speed = 5f;
    public float distanceThreshold = 20f;

    private bool hasReachedTarget;
    private bool canMoveHorizontally;
    private float spawnDirection;
    public UnityEvent OnDestroyed;

    void Start() {
        float playerX = player.transform.position.x;
        float currentX = transform.position.x;

        if (playerX < currentX) {
            transform.localScale = new Vector3(-1, 1, 1);
            spawnDirection = -1f;
        } else {
            spawnDirection = 1f;
        }
    }

    void Update() {
        if (!hasReachedTarget) {
            float playerHeight = player.transform.position.y;
            float currentHeight = transform.position.y;

            if (currentHeight < playerHeight + 0.7f) {
                transform.Translate(Vector3.up * speed * Time.deltaTime);
            } else {
                hasReachedTarget = true;
                StartCoroutine(WaitBeforeMoving());
            }
        } else if(canMoveHorizontally) {
            transform.Translate(Vector3.right * speed * spawnDirection * Time.deltaTime);

            if ((transform.localScale.x == -1 && transform.position.x < player.transform.position.x - distanceThreshold) || (transform.localScale.x == 1 && transform.position.x > player.transform.position.x + distanceThreshold)) {
                Destroy(gameObject);
            }
        }
    }

    IEnumerator WaitBeforeMoving() {
        // Wait for 0.3 seconds before continuing
        yield return new WaitForSeconds(0.2f);

        // Move in the X direction
        canMoveHorizontally = true;
    }
}
