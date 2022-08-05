using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleScript : MonoBehaviour
{
    public GameObject ball;
    public GameObject winScreen;
    [HideInInspector] public bool won;

    private GolfBallHandler ballScript;

     
    void Start()
    {
        ballScript = ball.GetComponent<GolfBallHandler>();
        won = false;
    }

    
    void Update()
    {
        //checks if golf ball is in the hole
        if(gameObject.transform.position.y > ball.transform.position.y && ballScript.closeToHole){
            won = true;
            winScreen.SetActive(true);
        }
    }

    void OnCollisionEnter(Collision other){
        ballScript.closeToHole = true;
    }

    void OnCollisionExit(Collision other){
        ballScript.closeToHole = false;
    }
}
