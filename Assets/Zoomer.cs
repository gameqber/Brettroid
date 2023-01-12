using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoomer : MonoBehaviour
{
    public float speed = 2.0f;
    public bool enableTracking = false;
    private GameObject downRay, diagRay, wallCollision;
    private int counter = 0;
    private bool initialRotation = true;
    [SerializeField] private int moveRight = 1; // When -1, it will move left
    // private int moveSpriteDirection = 0; // 0 = right, 1 = up, 2 = left, 3 = down
    // Start is called before the first frame update
    void Start()
    {
        downRay = transform.GetChild(0).gameObject;
        diagRay = transform.GetChild(1).gameObject;
        wallCollision = transform.GetChild(2).gameObject;
    }

    // Update is called once per frame
    void Update(){
    }
    void FixedUpdate()
    {
        // RaycastHit2D downhit = Physics2D.Raycast(transform.position - new Vector3(0.55f, 0.55f, 0), transform.up * -0.5f, 0.5f);
        RaycastHit2D downhit = Physics2D.Raycast(downRay.transform.position, transform.up * -0.5f, 0.5f);
        if(enableTracking){
            Debug.DrawRay(downRay.transform.position, transform.up * -0.5f, Color.green);
            Debug.DrawRay(diagRay.transform.position, transform.up * -0.5f + transform.right * 0.5f, Color.yellow);
            /* if (downhit.collider == null && counter % 60 == 0)
            {
                //Did not hit something
                Debug.Log("No more surface!");
            }
            else if(counter % 60 == 0)
            {
                //Did not hit something
                Debug.Log("Hitting: " + downhit.collider.tag);
            }
            counter++; */
        }

        // If nothing is below, detect for floor Zoomer is moving onto. If there's no floor there either, rotate.
        if(downhit.collider == null) {
            if(initialRotation){
                if(enableTracking){
                    Debug.Log("Initial rotation");
                }
                
                transform.Rotate(new Vector3(0, 0, -90));
                initialRotation = false;
            }
            RaycastHit2D diaghit = Physics2D.Raycast(diagRay.transform.position, transform.up * -0.5f + transform.right * 0.5f, 0.5f);
            if(diaghit.collider == null){
                if(enableTracking)
                    Debug.Log("Rotating clockwise");
                transform.Rotate(new Vector3(0, 0, -90));
            }
        }
        else{
            initialRotation = true;
        }
        moveSprite();
        
    }

    void OnCollisionEnter2D(Collision2D collision){
        Collider2D collider = collision.collider;
        if(enableTracking)
            Debug.Log("Collision detected between " + this.name + " and " + collision.gameObject.name);
        if(collision.gameObject.tag == "Floor"){
            Vector3 contactPoint = collision.contacts[0].point;
            Vector3 center = collider.bounds.center;

            transform.Rotate(new Vector3(0, 0, 90));
        }
        /* if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player") {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        } */
    }

    void moveSprite(){
        transform.position += (transform.rotation * Vector3.right * speed * moveRight) * Time.deltaTime;
    }
}
