using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform pointA; //first point
    [SerializeField] private Transform pointB; //second point
    [SerializeField] private float speed = 3f; //move speed 

    private Vector3 targetPosition; 
 

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = pointB.position; //start at point a towards b
        gameObject.SetActive(false); //start disabled
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.activeSelf)// only move if active
        
       transform.position = Vector3.MoveTowards(transform.position,targetPosition, speed * Time.deltaTime);

       //switch traget when raching point
       if(Vector3.Distance(transform.position, targetPosition) < 0.1f)
       {
        targetPosition = (targetPosition == pointA.position) ? pointB.position : pointA.position;
       }
    }

    public void StartMoving()
    {
        gameObject.SetActive(true); //activate platform
    }

}
