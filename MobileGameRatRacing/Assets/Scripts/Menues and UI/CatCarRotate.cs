using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatCarRotate : MonoBehaviour
{
  
    void Update()
    {
        transform.Rotate(0, 0.4f, 0 * Time.deltaTime);
    }
}
