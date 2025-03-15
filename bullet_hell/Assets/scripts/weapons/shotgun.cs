using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class shotgun : weapon
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
            cooldown = 60 / firerate;
            magazine--;
            //GetComponent<AudioSource>().Play();
            for (int i = 0; i < pelletCount; i++)//7 pellets
            {

                Instantiate(bullet, shootingPoint.transform.position, Quaternion.Euler(shootingPoint.transform.eulerAngles.x, shootingPoint.transform.eulerAngles.y, shootingPoint.transform.eulerAngles.z * UnityEngine.Random.Range(.9f, 1.1f)));
            }
            Instantiate(muzzleFlash, shootingPoint.transform.position, shootingPoint.transform.rotation);//muzzleFlash
        }
        else
        {
            //play click sound
            Debug.Log("*click*");
        }
    }
    public override void AltFire()
    {
        //no altfire
    }
}