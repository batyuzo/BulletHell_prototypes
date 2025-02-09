using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunHolder : MonoBehaviour
{
    [Header("offset-sheet")]
    //values: posY, posX, angleZ
    public float[] HCO = new float[3];//hand close offset
    public float[] HFO = new float[3];//hand far offset
    public float[] WPO = new float[3];//weapon offset

    weaponHandler wh;


    private void lookAtMouse()
    {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y * -1, dir.x * -1) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    }


    //executes on Equip
    public void setOffset()
    {
        wh = gameObject.GetComponentInChildren<weaponHandler>();

        //grab values from children
        HCO = wh.grabHandCloseOffset();
        HFO = wh.grabHandFarOffset();
        WPO = wh.grabWeaponOffset();


        //set grabbed values to sprites
        foreach (Transform child in transform)
        {
            if (child.name == "handClose")
            {
                child.localPosition = new Vector3(HCO[0], HCO[1], 0);
                child.localRotation = Quaternion.Euler(0, 0, HCO[2]);
            }
            else if (child.name == "handFar")
            {
                child.localPosition = new Vector3(HFO[0], HFO[1], 0);
                child.localRotation = Quaternion.Euler(0, 0, HFO[2]);
            }
            else if (child.name == "weapon")
            {
                child.localPosition = new Vector3(WPO[0], WPO[1], 0);
                child.localRotation = Quaternion.Euler(0, 0, WPO[2]);
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
                //if (child.name == "handClose")
                //{
                //    child.localRotation = Quaternion.Euler(0, 0, 0);
                //}
            }
        }
        else
        {
            foreach (Transform child in transform)
            {
                child.GetComponentInChildren<SpriteRenderer>().flipY = true;
                //if (child.name == "handClose")
                //{
                //    child.localRotation = Quaternion.Euler(0, 0, 53);
                //    //can also just offset the transform
                //    //child.localPosition += new Vector3(1, 1, 1);
                //}
            }
        }
    }
}