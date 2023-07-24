using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RinkaSpawner : MonoBehaviour
{
    public GameObject rinkaPrefab;
    public float spawnDistance = 5f;

    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(SpawnRinkas());
    }

    IEnumerator SpawnRinkas()
    {
        while (true)
        {
            if(Vector3.Distance(transform.position, player.position) <= spawnDistance)
            {
                GameObject newRinka = Instantiate(rinkaPrefab, transform.position, Quaternion.identity, transform);

                yield return new WaitUntil(() => newRinka == null);
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
