using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RackScript : MonoBehaviour
{
    public GameObject Putter;
    public GameObject Wood;
    public GameObject Driver;

    private Transform c;
    

    void Start()
    {
        c = gameObject.GetComponent<Collider>().transform;
    }

    void OnTriggerStay(Collider other){
        if(other.tag != "GameController") return;
        Vector3 objPos = c.InverseTransformPoint(other.gameObject.transform.position);
        
        GameObject selected;
        int clubNum = 0;

        if(objPos.x < -0.15){
            selected = Driver;
            clubNum = 2;
        } else if(objPos.x > 0.15){
            selected = Putter;
            clubNum = 0;
        } else {
            selected = Wood;
            clubNum = 1;
        }
        other.GetComponent<Grab>().GrabClub(selected,clubNum);

    }
}
