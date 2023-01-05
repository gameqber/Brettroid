using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoomer : MonoBehaviour
{
    public float speed = 2.0f;
    [SerializeField] private int moveRight = 1; // When -1, it will move left
    // private int moveSpriteDirection = 0; // 0 = right, 1 = up, 2 = left, 3 = down
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update(){
        Debug.DrawRay(transform.position, transform.right * 2, Color.green);
    }
    void FixedUpdate()
    {
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
