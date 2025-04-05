using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    //setup invisible markers
    public GameObject posA;
    public GameObject posB;

   //how long move takes
   [SerializeField] float duration =4f;

   private Vector3 startPoint;
   private Vector3 endPoint;
   float elapsedTime = 0f;
   private bool isMoving = false; 


    private void Start()
    {
        startPoint = posA.transform.position;
        endPoint = posB.transform.position;
    }
    // Update is called once per frame
    void Update()
    {

        if(!isMoving) return; //only move if activated

        //counts up how long the move has been going
        elapsedTime += Time.deltaTime;

        float tS = elapsedTime /duration;

        transform.position = Vector3.Lerp(startPoint, endPoint, tS);

        if(tS >=1f)
        {
            //swap transforms
            (startPoint,endPoint) = (endPoint, startPoint);
            elapsedTime = 0f;
        }
        
    }
    

    public void StartMoving() //called by lever
    {
        isMoving = true;
    }
}
