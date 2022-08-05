using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


public class RaySelect : MonoBehaviour
{
    public GameObject pointStart;
    public GameObject UIWindow;
    public GameObject selectIndicator;
    public SteamVR_Action_Single grip;
    public SteamVR_Action_Boolean touchTrigger;
    public SteamVR_Action_Boolean selectButton;
    
    private LineRenderer laserLine;
    private Transform rayTransform;
    private float range = 10;
    private bool touching;
    private TeleportScript tp;
    
    private UIHandler ui;

    void quit_game()
    {
        #if UNITY_STANDALONE
            Application.Quit();
        #endif
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        rayTransform = pointStart.transform;
        touching = false;
        selectIndicator.SetActive(false);
        ui = UIWindow.GetComponent<UIHandler>();
        tp = gameObject.transform.parent.GetComponent<TeleportScript>();
    }

    
    void Update()
    {
        if(touchTrigger.stateDown) touching = true;
        if(touchTrigger.stateUp) touching = false;

        if (grip.axis != 1 || touching) {
            laserLine.enabled = false;
            selectIndicator.SetActive(false);
            return;
        }

        laserLine.enabled = true;
        RaycastHit hit;
        
        Ray theRay = new Ray(rayTransform.position, rayTransform.forward);
        laserLine.SetPosition(0,rayTransform.position);

        if(Physics.Raycast(theRay, out hit,range)){
            laserLine.SetPosition(1,hit.point);

            selectIndicator.transform.position = hit.point;
            
            if(hit.collider.tag == "Button") {
                selectIndicator.SetActive(true);
            } else selectIndicator.SetActive(false);

            if(selectButton.stateDown){  

                if(hit.collider.name == "RecenterButton"){
                    tp.recenter();
                }else if (hit.collider.name == "QuitButton"){
                    quit_game();
                }
            }
        }else {
            laserLine.SetPosition(1,rayTransform.position + rayTransform.forward * range);
            selectIndicator.SetActive(false);
        }
    }
}
