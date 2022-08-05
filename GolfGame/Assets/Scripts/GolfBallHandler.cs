using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfBallHandler : MonoBehaviour
{
    
    public Terrain land;
    public int surface;
    
    [HideInInspector] public bool closeToHole;
    [HideInInspector] public bool moved;
    [HideInInspector] public bool moving;
    [HideInInspector] public bool hit;
    
    private float heightFromGround = 0.031f;
    private float lowestSpeed = 0.001f;

    private Rigidbody rigid;
    private Vector3 lastpos;

    private TerrainData tData;
    private int alphaWidth, alphaHeight;
    private float[,,] aMap;
    private int numText;

    private float height;

    [HideInInspector] public Vector3 windDir;
    [HideInInspector] public float windStrength;
    private Vector3 windForce;

    void Start()
    {
        moving = false;
        moved = false;
        closeToHole = false;
        rigid = gameObject.GetComponent<Rigidbody>();

        tData = land.terrainData;
        alphaWidth = tData.alphamapWidth;
        alphaHeight = tData.alphamapHeight;
        aMap = tData.GetAlphamaps(0,0,alphaWidth,alphaHeight);
        numText = aMap.Length/(alphaWidth*alphaHeight);

        windDir = new Vector3(
            Mathf.Round(Random.Range(-1f,1f) * 10f) * 0.1f,
            0,
            Mathf.Round(Random.Range(-1f,1f) * 10f) * 0.1f
        );

        windStrength = Mathf.Round(Random.Range(3f,10f) * 10f) * 0.1f;
        windForce = windDir * windStrength;
        
    }

    
    void Update()
    {
        surface = currentSurfaceTexture();
        ModifyBall();

        if(rigid.velocity.magnitude >= lowestSpeed){
            moving = true;
            moved = true;
            hit = true;
        }else if (rigid.velocity.magnitude < lowestSpeed){
            rigid.velocity = new Vector3(0,0,0);
            moving = false;
            hit = false;
            lastpos = transform.position;
        }
        
    }

    void FixedUpdate(){
        if(moving && transform.position.y > height + 1){
            rigid.AddForce(windForce);
        }
    }

    //determines what texture the ball is currently on
    private int currentSurfaceTexture(){
        Vector3 coords = new Vector3();
        Vector3 landPos = land.transform.position;
        Vector3 ballPos = gameObject.transform.position;
        
        coords.x = ((ballPos.x - landPos.x) / tData.size.x) * alphaWidth;
        coords.z = ((ballPos.z - landPos.z) / tData.size.z) * alphaHeight;
        
        int ret = 0;
        for(int i = 0; i< numText; i++){
            if( aMap[(int)coords.z, (int)coords.x, i] > 0 ){
                ret = i;
            }
        }
        return ret;
    }


    void ModifyBall(){
        Vector3 ballPos = transform.position;
        height = land.SampleHeight(transform.position);
        float difference = ballPos.y - height;
        
        if(surface == 2 && difference < 0.05 && hit){
            rigid.velocity = new Vector3(0,0,0);
            rigid.useGravity = false;
        }

        if(ballPos.y < height && !closeToHole){
            ballPos.y = height + heightFromGround; 
            transform.position = ballPos;
        }
    }

    void OnCollisionEnter(Collision other){
        if(other.gameObject.tag == "Water"){
            rigid.velocity = new Vector3(0,0,0);
            transform.position = lastpos;
            moving = false;
            hit = false;
        }
    }


}
