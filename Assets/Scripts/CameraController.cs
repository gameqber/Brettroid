using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private GameObject currentMap;
    private float leftXBound, rightXBound;
    
    // Start is called before the first frame update
    void Start()
    {
        currentMap = GameObject.Find("Brinstar Map 1");
        leftXBound = currentMap.GetComponent<MapController>().leftXBound;
        rightXBound = currentMap.GetComponent<MapController>().rightXBound;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(player.transform.position.x > rightXBound){
            transform.position = new Vector3(rightXBound, 0, -10);
        }
        else if(player.transform.position.x < leftXBound){
            transform.position = new Vector3(leftXBound, 0, -10);
        }
        else{
            transform.position = new Vector3(player.transform.position.x, 0, -10);
        }
        
    }
}
