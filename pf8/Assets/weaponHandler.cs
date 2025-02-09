using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponHandler : MonoBehaviour
{
    [Header("weapon script ref")]
    public weapon weaponScript;




    public void Fire()
    {
        weaponScript.Fire();
    }

    public void AltFire()
    {
        weaponScript.AltFire();
    }

    public void Equip()
    {
        weaponScript.Equip();
    }

    public float[] grabHandCloseOffset()
    {
        return weaponScript.handCloseOffset;
    }

    public float[] grabHandFarOffset()
    {
        return weaponScript.handFarOffset;
    }

    public float[] grabWeaponOffset()
    {
        return weaponScript.weaponOffset;
    }

    public void Throw()
    {
        weaponScript.Throw();
    }

    public void Drop()
    {
        //base for throw and forfeiting control overall
        weaponScript.Drop();

    }


    // Update is called once per frame
    void Update()
    {
        
    }


}
