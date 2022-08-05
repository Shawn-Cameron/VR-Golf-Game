using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorHandler : MonoBehaviour
{

    public GameObject cameraView;
    public GameObject hole;
    private bool visible;

    void Update()
    {
        Vector3 cameraPos = cameraView.transform.position;
        Vector3 holePos = hole.transform.position;
        Vector3 dir = (cameraPos - holePos).normalized;
        float dis = Vector3.Distance(cameraPos,holePos);
        transform.position = holePos + dir * (dis - 2); 
        transform.LookAt(holePos);

        if(dis < 30) makeInvisible();
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
