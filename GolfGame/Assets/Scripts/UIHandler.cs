using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Valve.VR;

public class UIHandler : MonoBehaviour
{
    public TextMeshProUGUI clubText;
    public TextMeshProUGUI powerText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI windStrText;
    public GameObject windArrow;

    public GameObject golfBall;
    public SteamVR_Action_Boolean toggleUI;

    private int clubNum;
    private bool setValues;
    
    [HideInInspector] public int score;
    

    void Start()
    {
        clubNum = 1;
        score = 0;
        setValues = false;
        clubSelector();
    }

    void Update(){
        if(toggleUI.stateDown) toggleVisiblility();
        if (setValues) return;
        GolfBallHandler gbh = golfBall.GetComponent<GolfBallHandler>();
        if(gbh.windStrength == 0) return;

        windStrText.text = "Strength: " + gbh.windStrength;
        Vector3 dir = new Vector3(gbh.windDir.x,gbh.windDir.z,0);
        
        float angle = Vector3.Angle(dir,transform.up);
        if(dir.x > 0) angle = 360 - angle;
        windArrow.transform.Rotate(0,0,angle);

        setValues = true;
    }

    public void addToScore(){
        score += 1;
        changeScore();
    }

    public void setClub(int num){
        clubNum = num;
        clubSelector();
    }

    void clubSelector(){
        switch(clubNum){
            case 0:
                clubText.text = "Putter";
                powerText.text = "Power: 150";
                break;
            case 1:
                clubText.text = "Wood";
                powerText.text = "Power: 500";
                break;
            case 2:
                clubText.text = "Driver";
                powerText.text = "Power: 2000";
                break;
            default:
                Debug.Log("Error selecting club: " + clubNum);
                break;
        }
    }

    void changeScore(){
        scoreText.text = "Score: " + score.ToString(); 
    }

    void toggleVisiblility(){
        foreach(Transform child in transform) 
            child.gameObject.SetActive(!child.gameObject.activeInHierarchy);
    }
}
