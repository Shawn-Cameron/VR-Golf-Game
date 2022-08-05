using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBall : MonoBehaviour
{
    public float upForce;
    public float forceFactor;
    public float maxForce;
    public float minForce;

    private float speed;
    private Vector3 oldPos;
    private float forcePercent;

    private GameObject golfBall;
    private GolfBallHandler ballScript;
    private UIHandler ui;
    
    private float sandDampen = 0.3f;
    private float minSpeed = 0;
    private float maxSpeed = 20;

    private float lastAngle;
    private Vector3 colliderSize;
    private bool firstPass = true;


    void Start(){
        oldPos = transform.position;
        golfBall = GameObject.Find("Ball");
        ballScript = golfBall.GetComponent<GolfBallHandler>();
        ui = GameObject.Find("Window").GetComponent<UIHandler>();
        colliderSize = gameObject.GetComponent<BoxCollider>().size;
    }

    void Update(){
        speed = Vector3.Distance(oldPos,transform.position) / Time.deltaTime;
        oldPos = transform.position;
        if(ballScript.surface == 2) forcePercent = sandDampen;
        else forcePercent = 1;
        
        checkCollision();
        
    }

    //Checks for normal collision with golf ball
    private void OnTriggerEnter(Collider other){
        if(other.gameObject.tag != "GolfBall") return;
        if(other.GetComponent<GolfBallHandler>().hit) return;

        Vector3 pos = gameObject.transform.position;
        Vector3 ballPos = other.gameObject.transform.position;
        Vector3 ballDir = (ballPos - pos).normalized;
        Vector3 dir = Quaternion.Euler(0,-90,0) * gameObject.transform.forward;
        
        Vector3 appliedForce = speedToMag(new Vector3(dir.x, upForce, dir.z)) * forcePercent * forceFactor; 
        float angle = Vector3.Angle(dir,ballDir);
        
        if (angle > 60) return;
        hitBall(dir);
        
    }

    //Converts the clubs speed to the force applied in each direction
    private Vector3 speedToMag(Vector3 v){
        if(speed > maxSpeed) speed = maxSpeed;
        if(speed < minSpeed) speed = minSpeed;
        float mag = (speed - minSpeed) / (maxSpeed - minSpeed) * (maxForce - minForce) + minForce;
        v.x = v.x * mag;
        v.y = v.y * speed;
        v.z = v.z * mag;
        
        return v;
    }

    //To prevent the golf ball from passing through the club
    private void checkCollision(){
        bool inRange = false;

        Vector3 forward = Quaternion.Euler(0,-90,0) * gameObject.transform.forward;
        Vector3 DirToBall = golfBall.transform.position - gameObject.transform.position;
        
        Vector3 collPos = gameObject.GetComponent<Collider>().transform.position;

        float angle = Vector3.Angle(DirToBall,forward);
        
        if(Mathf.Abs(golfBall.transform.position.y - collPos.y) <= colliderSize.y / 2) inRange = true;

        if(angle - lastAngle > 90 && inRange && !firstPass){
            hitBall(forward);
        }

        if(firstPass) firstPass = false;
        lastAngle = angle;        
    }

    //applies the force to the golf ball
    private void hitBall(Vector3 dir){
        if (golfBall.GetComponent<GolfBallHandler>().hit) return;

        Vector3 appliedForce = speedToMag(new Vector3(dir.x, upForce, dir.z)) * forcePercent * forceFactor;

        golfBall.GetComponent<Rigidbody>().useGravity = true;
        golfBall.GetComponent<Rigidbody>().AddForce(appliedForce);
        ui.addToScore();
    }
}
