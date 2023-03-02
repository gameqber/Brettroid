using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoomer : MonoBehaviour
{
    public float speed = 2.0f;
    public bool enableTracking = false;
    public LayerMask IgnorePlayer;
    private GameObject downRay, diagRay;
    private bool initialRotation = true;
    [SerializeField] private int moveRight = -1; // When -1, it will move left

    // Start is called before the first frame update
    void Start()
    {
        downRay = transform.GetChild(0).gameObject;
        diagRay = transform.GetChild(1).gameObject;
        if(moveRight == -1)
        {
            FlipSprite();
        }
    }

    // Update is called once per frame
    void Update(){
    }
    void FixedUpdate()
    {
        RaycastHit2D downhit = Physics2D.Raycast(downRay.transform.position, transform.up * -0.5f, 0.5f, ~IgnorePlayer);
        if(enableTracking){
            Debug.DrawRay(downRay.transform.position, transform.up * -0.5f, Color.green);
            Debug.DrawRay(diagRay.transform.position, transform.up * -0.5f + transform.right * 0.5f, Color.yellow);
            
        }

        // If something is below Zoomer's downward ray, detect for floor Zoomer is moving onto.
        if(downhit.collider != null) {
            if(downhit.collider.tag == "Floor"){
                initialRotation = true;
            }
            else{
                if(enableTracking){
                    Debug.Log("Down ray is colliding with non-floor object tagged " + downhit.collider.tag);
                }
                SpriteRotationHandler();
            }
        }

        // Nothing is below Zoomer's downward ray
        else{
            SpriteRotationHandler();
        }
        MoveSprite();
        
    }

    void OnCollisionEnter2D(Collision2D collision){
        Collider2D collider = collision.collider;
        if(enableTracking)
            Debug.Log("Collision detected between " + this.name + " and " + collision.gameObject.name);
        if(collision.gameObject.tag == "Floor"){
            if(enableTracking){
                Debug.Log("Collision with floor. Rotating to topside.");
                // Debug.Break();
            }

            transform.Rotate(new Vector3(0, 0, 90));
        }
    }

    void MoveSprite(){
        transform.position += (transform.rotation * Vector3.right * speed) * Time.deltaTime;
    }

    void FlipSprite()
    {
        if (enableTracking)
        {
            Debug.Log("Flipping the sprite.");
        }
        transform.Rotate(new Vector3(0, 180, 0));
    }

    void SpriteRotationHandler(){
        if(initialRotation){
            if(enableTracking){
                Debug.Log("Down ray hit nothing or non-floor object. Rotating to underside. Initial rotation.");
                // Debug.Break();
            }
            transform.Rotate(new Vector3(0, 0, -90));
            initialRotation = false;
        }

        RaycastHit2D diaghit = Physics2D.Raycast(diagRay.transform.position, transform.up * -0.5f + transform.right * 0.5f, 0.5f, ~IgnorePlayer);
        if(diaghit.collider != null){
            if(diaghit.collider.tag == "Floor"){
                return;
            }
            else{
                if(enableTracking){
                    Debug.Log("Diag ray hit object tagged " + diaghit.collider.tag + ". Rotating to underside.");
                    // Debug.Break();
                }
                transform.Rotate(new Vector3(0, 0, -90));
            }
        }
        else{
            if(enableTracking){
                Debug.Log("No hit by diag ray and down ray hit nothing or non-floor object. Rotating to underside.");
                // Debug.Break();
            }
            transform.Rotate(new Vector3(0, 0, -90));
        }
    }
}
