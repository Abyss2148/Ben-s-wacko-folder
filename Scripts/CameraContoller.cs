using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContoller : MonoBehaviour
{

    public Transform player;
    public Vector3 offset;
    private float ySet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.y > 0)
        {
            ySet = player.position.y + offset.y;
        }
        else
        {
            ySet = 0 + offset.y;
        }

        	transform.position = new Vector3 (player.position.x + offset.x, ySet, offset.z); 
    }
}
