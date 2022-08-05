using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class TeleportScript : MonoBehaviour
{
    public GameObject golfBall;
    public GameObject hole;

    private float fadeDuration = 0.7f;
    private bool canTeleport;
    private Vector3 teleportPoint;
    private GolfBallHandler ballScript;
    private HoleScript holeHandler;
    

    void Start()
    {
        ballScript = golfBall.GetComponent<GolfBallHandler>();
        holeHandler = hole.GetComponent<HoleScript>();
        recenter();
    }

    
    void Update()
    {
        if(ballScript.moved && !ballScript.moving){
            ballScript.moved = false;
            recenter();
        }
    }

    void calcTeleportPoint(){
        Vector3 ballPos = golfBall.transform.position;
        Vector3 holePos = hole.transform.position;
        holePos.y = ballPos.y;
        Vector3 dir = (holePos - ballPos).normalized;
        teleportPoint = Quaternion.Euler(0,-90,0) * dir * 0.5f + ballPos;
        
    }

    void TeleportToPoint()
    { 
        gameObject.transform.position = teleportPoint;
        gameObject.transform.LookAt(golfBall.transform,Vector3.up);
        Invoke("RemoveFade", 0.05f); 
    }

    void RemoveFade()
    {
        SteamVR_Fade.View(Color.clear,fadeDuration);
    }

    public void recenter(){
        if(holeHandler.won) return;
        calcTeleportPoint();
        SteamVR_Fade.View(Color.black,fadeDuration);
        Invoke("TeleportToPoint",fadeDuration);
    }

}
