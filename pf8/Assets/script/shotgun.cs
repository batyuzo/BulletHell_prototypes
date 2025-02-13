using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotgun : weapon
{
    int i = 0;
    public override void Fire() {
        if (i == 0) { GameObject.FindGameObjectWithTag("mapLoader").GetComponent<mapLoader>().loadMap("medieval_japan"); i++; }
        else if (i == 1) { GameObject.FindGameObjectWithTag("mapLoader").GetComponent<mapLoader>().loadMap("ham_factory"); i++; }
        else { GameObject.FindGameObjectWithTag("mapLoader").GetComponent<mapLoader>().loadMap("practice"); i=0; }

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
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}
