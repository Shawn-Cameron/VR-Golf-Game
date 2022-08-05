using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject ball;
    private Vector3 offset;
    
    void Start()
    {
        Vector3 pos = transform.position;
        pos.x = ball.transform.position.x;
        transform.position = pos;
        
        offset = transform.position - ball.transform.position;
    }

    
    void Update()
    {
        transform.position = ball.transform.position + offset;
    }
}
