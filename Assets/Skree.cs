using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skree : MonoBehaviour
{
    public GameObject explosionPrefab;
    private float playerX, skreeX, differenceX;
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float explosionSpeed = 30.0f;
    private bool moveLeft = true, diving = false, onFloor = false;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null){
            playerX = player.transform.position.x;
            skreeX = transform.position.x;

            if(playerX < skreeX){ // To the left
                moveLeft = true;
            }
            else if(playerX > skreeX){
                moveLeft = false;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(player != null){
            playerX = player.transform.position.x;
            if(playerX < skreeX){ // To the left
                moveLeft = true;
            }
            else if(playerX > skreeX){
                moveLeft = false;
            }
        }
        differenceX = Mathf.Abs(playerX - skreeX);
        if(differenceX <= 3 && !diving){
            Debug.Log("Player is within 3 units on X coordinate. Diving!");
            diving = true;
        }
        if(diving && !onFloor){
            transform.position += (Vector3.down * speed) * Time.deltaTime;
            if(differenceX < 0.5f){
            }
            else if(moveLeft){
                transform.position += (Vector3.left * speed * 0.3f) * Time.deltaTime;
            }
            else{
                transform.position += (Vector3.right * speed * 0.3f) * Time.deltaTime;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Floor"){
            Debug.Log("Collided with floor.");
            onFloor = true;
            Invoke("DestroyObject", 1.5f);
        }
    }

    void DestroyObject(){
        Destroy(gameObject);
        GameObject explosionParticle1 = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        GameObject explosionParticle2 = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        GameObject explosionParticle3 = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        GameObject explosionParticle4 = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        explosionParticle1.GetComponent<Rigidbody2D>().velocity = new Vector2(explosionSpeed, 0f);
        explosionParticle2.GetComponent<Rigidbody2D>().velocity = new Vector2(explosionSpeed * 0.3f, explosionSpeed * 0.9f);
        explosionParticle3.GetComponent<Rigidbody2D>().velocity = new Vector2(explosionSpeed * -0.3f, explosionSpeed * 0.9f);
        explosionParticle4.GetComponent<Rigidbody2D>().velocity = new Vector2(-explosionSpeed, 0f);
        Destroy(explosionParticle1, 0.14f);
        Destroy(explosionParticle2, 0.14f);
        Destroy(explosionParticle3, 0.14f);
        Destroy(explosionParticle4, 0.14f);
    }
}
