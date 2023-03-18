using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZebSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject player;

    private GameObject currentEnemy;
    private bool playerInsideSpawnerArea = false;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject == player){
            playerInsideSpawnerArea = true;
            if (currentEnemy == null) {
                StartCoroutine(SpawnEnemy());
            }
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject == player) {
            playerInsideSpawnerArea = false;
        }
    }

    IEnumerator SpawnEnemy() {
        while (playerInsideSpawnerArea && currentEnemy == null) {
            currentEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            currentEnemy.GetComponent<Zeb>().player = player;
            currentEnemy.GetComponent<Zeb>().OnDestroyed.AddListener(OnEnemyDestroyed);

            yield return new WaitUntil(() => currentEnemy == null);
        }
    }

    void OnEnemyDestroyed() {
        currentEnemy = null;
    }
}
