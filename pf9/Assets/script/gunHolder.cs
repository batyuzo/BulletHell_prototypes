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

    public GameObject equipped;

    [Header("weapon script ref")]
    public weapon weaponScript;



    //title says it all
    private void lookAtMouse()
    {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y * -1, dir.x * -1) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //flip part

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos.x < transform.position.x)
        {
            foreach (Transform child in transform)
            {
                //check if 'spriterenderer exists'
                if (child.GetComponentInChildren<SpriteRenderer>() != null)
                {
                    child.GetComponentInChildren<SpriteRenderer>().flipY = false;
                }

                //handClose
                if (child.name == "handClose")
                {
                    child.localRotation = Quaternion.Euler(0, 0, HCO[2]);
                    child.localPosition = new Vector3(HCO[0], HCO[1], child.localPosition.z);
                }
                //handFar
                if (child.name == "handFar")
                {
                    child.localRotation = Quaternion.Euler(0, 0, HFO[2]);
                    child.localPosition = new Vector3(HFO[0], HFO[1], child.localPosition.z);
                }

                //weapon
                if (child.CompareTag("weapon"))
                {
                    child.localRotation = Quaternion.Euler(0, 0, WPO[2]);
                    child.localPosition = new Vector3(WPO[0], WPO[1], child.localPosition.z);
                }
            }
        }
        else
        {
            foreach (Transform child in transform)
            {
                if (child.GetComponentInChildren<SpriteRenderer>() != null)
                {
                    child.GetComponentInChildren<SpriteRenderer>().flipY = true;
                }

                //handClose
                if (child.name == "handClose")
                {
                    child.localRotation = Quaternion.Euler(0, 0, -HCO[2]);
                    child.localPosition = new Vector3(HCO[0], -HCO[1], child.localPosition.z);
                }
                //handFar
                if (child.name == "handFar")
                {
                    child.localRotation = Quaternion.Euler(0, 0, -HFO[2]);
                    child.localPosition = new Vector3(HFO[0], -HFO[1], child.localPosition.z);
                }

                //weapon
                if (child.CompareTag("weapon"))
                {
                    child.localRotation = Quaternion.Euler(0, 0, -WPO[2]);
                    child.localPosition = new Vector3(WPO[0], -WPO[1], child.localPosition.z);
                }
            }
        }
    }


    //executes on Equip
    public void setOffset()
    {

        //grab values from children
        HCO = weaponScript.handCloseOffset;
        HFO = weaponScript.handFarOffset;
        WPO = weaponScript.weaponOffset;


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
            else if (child.CompareTag("weapon"))
            {
                child.localPosition = new Vector3(WPO[0], WPO[1], 0);
                child.localRotation = Quaternion.Euler(0, 0, WPO[2]);
            }
        }
    }



    public bool Fire()
    {
        if (equipped != null)
        {
            weaponScript.Fire();
            return true;
        }
        else return false;

    }

    public bool AltFire()
    {
        if (equipped != null)
        {
            weaponScript.AltFire();
            return true;
        }
        else return false;
    }

    public void Equip()
    {

        equipped = gameObject.GetComponentInChildren<scanner>().getEquippable();
        //set parent
        equipped.transform.SetParent(transform, false);
        //set script reference from child
        weaponScript = equipped.GetComponent<weapon>();
        //prepare offsets
        weaponScript.SetValues();
        equipped.transform.localPosition = Vector3.zero;
        equipped.transform.localRotation = Quaternion.Euler(0, 0, 0);
        //execute offsets
        setOffset();
        Debug.Log(WPO[0] + WPO[1] + WPO[2]);

    }



    public void Throw()
    {
        weaponScript.Throw();
    }

    public void Drop()
    {
        //base for throw and forfeiting control overall
        //clear parent
        weaponScript.Drop();

    }

    private void Update()
    {
        lookAtMouse();

    }
}