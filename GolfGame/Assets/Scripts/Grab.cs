using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Grab : MonoBehaviour
{
    public SteamVR_Action_Boolean grab;
    public GameObject spawnLoc;
    public GameObject UIwindow;

    private SteamVR_Behaviour_Pose pose;
    private GameObject clubInstance;

    void Start(){
        pose = gameObject.GetComponent<SteamVR_Behaviour_Pose>();
    }

    void Update() {
        if(grab.stateUp){
            Destroy(clubInstance);
        }
    }

    public void GrabClub(GameObject club, int clubNum){
        if(grab.stateDown && clubInstance == null){    
            clubInstance = Instantiate(club, spawnLoc.transform) as GameObject;
            UIwindow.GetComponent<UIHandler>().setClub(clubNum);
        }

    }
}
