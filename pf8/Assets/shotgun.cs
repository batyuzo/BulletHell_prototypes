using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotgun : weapon
{
    public override void Fire() {
        //firing script, let it be 10 damage inflicted for self, now
        gameObject.GetComponentInParent<playerHealth>().playerDamaged(10);
        Debug.Log("shotgun fired with 'shotgun.cs'");
    }

    public override void AltFire()
    {
        Debug.Log("shotgun altFired with 'shotgun.cs'");
    }

    public override void SetValues()
    {
        handCloseOffset = new float[] { 0,0, 53 };
        handFarOffset = new float[] { -0.863f, -0.162f, 0 };
        weaponOffset = new float[] { -0.446f, -0.061f, 0 };
        transform.localPosition= Vector3.zero;
        transform.localRotation = Quaternion.Euler(0,0,0);
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}
