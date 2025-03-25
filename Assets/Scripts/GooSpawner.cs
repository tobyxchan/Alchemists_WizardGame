using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooSpawner : MonoBehaviour
{

    public GameObject gooPrefab; //assign goo to this
    public Transform spawnPoint;//where projectile spawns
    public float shootInterval = 2f;//time between shots

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Shoot", 0f, shootInterval); //call for shot every 2 seconds

    }

    void Shoot()
    {
        if(gooPrefab !=null && spawnPoint !=null)
        {
            Instantiate(gooPrefab, spawnPoint.position, spawnPoint.rotation);

        }
    }

}
