using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinScript : MonoBehaviour
{
    public GameObject location;
    public GameObject cameraObj;
    public GameObject uiScreen;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreNameText;
    

    private Vector3 velocity = Vector3.zero;
    private float time = 0.5f;
    
    void Update()
    {
        Vector3 pos = location.transform.position;
        Quaternion rot = cameraObj.transform.rotation;

        transform.position = Vector3.SmoothDamp(
            transform.position, pos, ref velocity,time
        );
        transform.rotation = Quaternion.Lerp(transform.rotation,rot,0.1f);
        
        int score = uiScreen.GetComponent<UIHandler>().score;

        scoreText.text = "Score: "+ score.ToString();
        scoreName(score);
    }

    void scoreName(int score){
        string text = "";
        switch(score){
            case 1:
                text = "Hole-In-One";
                break;
            case 2:
                text = "Double Eagle";
                break;
            case 3:
                text = "Eagle";
                break;
            case 4:
                text = "Birdie";
                break;
            case 5:
                text = "On Par";
                break;
            case 6:
                text = "Bogey";
                break;
            case 7:
                text = "Double Bogey";
                break;
        }
        scoreNameText.text = text;
    }
}
