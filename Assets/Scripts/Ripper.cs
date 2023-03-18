using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ripper : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += (transform.rotation * Vector3.right * speed) * Time.deltaTime;
    }

    void FlipSprite()
    {
        transform.Rotate(new Vector3(0, 180, 0));
    }

    void OnCollisionEnter2D(Collision2D collision){
        Collider2D collider = collision.collider;
        if(collision.gameObject.tag == "Floor"){
            FlipSprite();
        }
    }
}
