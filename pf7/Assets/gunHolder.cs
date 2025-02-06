using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunHolder : MonoBehaviour
{
    //variables
    private Transform gunTransform;



    //functions
    private void lookAtMouse()
    {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y * -1, dir.x * -1) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }




    //defaults
    private void FixedUpdate()
    {

        lookAtMouse();
        
    }
    void Start()
    {
        gunTransform=transform;
    }
}
