using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] GameObject player;  //get player object

    [SerializeField] float smoothSpeed = 0.125f;

    [SerializeField] Vector3 offset;

    // Start is called before the first frame update
   
   private void LateUpdate()
   {
        offset.z = -10f;
        Vector3 finalPosition = player.transform.position + offset;

        //smoothly follows player movement after offset applied
        Vector3 smoothedPosition = Vector3.Lerp(transform.position,finalPosition,smoothSpeed);
        transform.position = smoothedPosition;
   }
}
