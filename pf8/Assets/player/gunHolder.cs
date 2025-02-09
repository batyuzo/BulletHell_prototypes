using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunHolder : MonoBehaviour
{
    [Header("offset-sheet")]
    //values: posY, posX, angleZ
    public float[] handCloseOffset = new float[3];
    public float[] handFarOffset = new float[3];
    public float[] weaponOffset = new float[3];


    private void lookAtMouse()
    {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y * -1, dir.x * -1) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void setOffset()
    {
        //grab values from 'weapon'
        handCloseOffset = gameObject.GetComponentInChildren<shotgun>().handCloseOffset;
        handFarOffset = gameObject.GetComponentInChildren<shotgun>().handFarOffset;
        weaponOffset= gameObject.GetComponentInChildren<shotgun>().weaponOffset;


        //set grabbed values to sprites
        foreach (Transform child in transform)
        {
            if (child.name == "handClose")
            {
                child.localRotation = Quaternion.Euler(0, 0, handCloseOffset[2]);
                child.localPosition = new Vector3(handCloseOffset[0], handCloseOffset[1], 0);
            }
            else if (child.name == "handFar")
            {
                child.localRotation = Quaternion.Euler(0, 0, handFarOffset[2]);
                child.localPosition = new Vector3(handCloseOffset[0], handCloseOffset[1], 0);
            }
            else if (child.name == "weapon")
            {
                child.localRotation = Quaternion.Euler(0, 0, weaponOffset[2]);
                child.localPosition = new Vector3(weaponOffset[0], weaponOffset[1], 0);
            }
        }
    }

    private void Update()
    {
        lookAtMouse();

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos.x < transform.position.x)
        {
            foreach (Transform child in transform)
            {
                child.GetComponentInChildren<SpriteRenderer>().flipY = false;
                if (child.name == "handClose")
                {
                    child.localRotation = Quaternion.Euler(0, 0, 0);
                }
            }
        }
        else
        {
            foreach (Transform child in transform)
            {
                child.GetComponentInChildren<SpriteRenderer>().flipY = true;
                if (child.name == "handClose")
                {
                    child.localRotation = Quaternion.Euler(0, 0, 53);
                    //can also just offset the transform
                    //child.localPosition += new Vector3(1, 1, 1);
                }
            }
        }
    }
}