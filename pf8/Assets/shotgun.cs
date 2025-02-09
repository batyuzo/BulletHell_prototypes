using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotgun : MonoBehaviour
{
    //weapon-specific offset values
    public float[] handCloseOffset = { 0, 0, 53 };
    public float[] handFarOffset = { -0,863f, -0,162f, 0 };
    public float[] weaponOffset = { -0,446f, -0,061f, 0 };
    // Start is called before the first frame update

    public void fire() { }

    public void altfire() { }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}
