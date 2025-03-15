using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class pistol : weapon
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
            cooldown = 60/firerate;//60=1s
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
    public override void SetValues()
    {
        handCloseOffset = new float[] { -0.6f, .1f, -5f };
        handFarOffset = new float[] { -0.75f, -0.03f, 35 };
        weaponOffset = new float[] { -0.8f, .3f, 0 };
    }
    private void Awake()//defaults
    {
        //SET WEAPON STATS
        weaponName = "pistol";
        weaponHands = new char[] { 'a', 'b' };
        gameObject.layer = 8;
        magazine = 12;
        rarity = 1;
        ranged = true;
        firerate = 1f;

        //TEMPORARILY AUTOMATIC
        auto = true;

        //SET IN PROJECTILE
        //projSpeed = 30f;
        //damage = 35;
    }
}
