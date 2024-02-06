using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheese : MonoBehaviour
{
   
    void Start()
    {
        
    }

   
    void Update()
    {
        transform.Rotate(0 , 0 , 20 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerManager.numberOfCheese += 1;
            Destroy(gameObject);
        }
    }
}
