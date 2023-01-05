using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoomer : MonoBehaviour
{
    public float speed = 2.0f;
    private bool messagePrinted = false;
    private int counter = 0;
    [SerializeField] private int moveRight = 1; // When -1, it will move left
    // private int moveSpriteDirection = 0; // 0 = right, 1 = up, 2 = left, 3 = down
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update(){
    }
    void FixedUpdate()
    {
        Debug.DrawRay(transform.position - new Vector3(0.5f, 0.5f, 0), transform.up * -0.5f, Color.green);
        Debug.DrawRay(transform.position + new Vector3(0.5f, -0.5f, 0), new Vector3(0.5f, -0.5f, 0), Color.yellow);
        RaycastHit2D hit = Physics2D.Raycast(transform.position - new Vector3(0.55f, 0.55f, 0), transform.up * -0.5f, 5f);
        if (hit.collider == null)
        {
            //Did not hit something
            Debug.Log("No more surface!");
            messagePrinted = true;
        }
        else if(messagePrinted == false && counter % 60 == 0)
        {
            //Did not hit something
            Debug.Log("Hitting: " + hit.collider.tag);
            // messagePrinted = true;
        }
        counter++;
        moveSprite();
        
    }

    void moveSprite(){
        transform.position += (Vector3.right * speed * moveRight) * Time.deltaTime;
        /*
        switch (moveSpriteDirection){
                case 0:
                    // Increment X
                    break;
                case 1:
                    // Increment Y
                    break;
                case 2:
                    // Decrement X
                    break;
                case 3:
                    // Decrement Y
                    break;
                default:
                    // Don't move
                    break;
            }
        */
    }
}
