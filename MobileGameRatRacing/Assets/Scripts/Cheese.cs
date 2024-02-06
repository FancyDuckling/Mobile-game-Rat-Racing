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
        transform.Rotate(0 , 0 , 40 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FindAnyObjectByType<AudioManager>().PlaySound("PickUpCheese");
            PlayerManager.numberOfCheese += 1;
            Destroy(gameObject);
        }
    }
}
