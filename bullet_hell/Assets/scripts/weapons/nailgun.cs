using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class nailgun : weapon
{
    [Header("bullet refs")]
    public GameObject shootingPoint;
    public GameObject muzzleFlash;
    public GameObject bullet;
    [SerializeField] int pelletCount;
    public override void Fire()
    {
        //firing happens
        if (cooldown <= 0 && magazine > 0)
        {
            cooldown = 60 / firerate;//60=1s
            magazine--;
            //GetComponent<AudioSource>().Play();

            //BULLET
            Instantiate(bullet, shootingPoint.transform.position, Quaternion.Euler(shootingPoint.transform.eulerAngles.x, shootingPoint.transform.eulerAngles.y, shootingPoint.transform.eulerAngles.z));
            //MUZZLE FLASH
            Instantiate(muzzleFlash, shootingPoint.transform.position, shootingPoint.transform.rotation);
        }
    }
    public override void AltFire()
    {
        //no altfire
    }
}
