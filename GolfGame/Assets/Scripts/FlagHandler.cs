using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagHandler : MonoBehaviour
{
    public GameObject golfball;
    private bool visible;
    
    void Update()
    {
        float dist = Vector3.Distance(transform.position,golfball.transform.position);
        if(dist < 10) makeInvisible();
        else makeVisible();

    }

    void makeInvisible(){
        if(!visible) return;
        foreach(Transform child in transform) 
            child.GetComponent<MeshRenderer>().enabled = false;
        visible = false;
    }
    
    void makeVisible(){
        if(visible) return;
        foreach(Transform child in transform) 
            child.GetComponent<MeshRenderer>().enabled = true;
        visible = true;
    }
}
