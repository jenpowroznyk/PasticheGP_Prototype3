using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_test : MonoBehaviour
{
    public GameObject player;
    private Vector3 cameraPos;
    private Vector3 distance;

    // Start is called before the first frame update
    void Start()
    {
        cameraPos = transform.position;
        distance = new Vector3(0, -22, 10);
    }
    

    void LateUpdate()
    {
        transform.position = player.transform.position + cameraPos + distance;
    }
   
}
