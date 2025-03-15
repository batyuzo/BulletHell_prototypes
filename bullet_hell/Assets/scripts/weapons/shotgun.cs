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
    public override void SetValues()
    {
        handCloseOffset = new float[] { 0, 0, 53 };
        handFarOffset = new float[] { -0.863f, -0.162f, 0 };
        weaponOffset = new float[] { -0.446f, -0.061f, 0 };
    }
    private void Awake()//defaults
    {
        //SET WEAPON STATS
        weaponName = "shotgun";
        weaponHands = new char[] { 'a', 'a' };
        gameObject.layer = 8;
        magazine = 7;
        pelletCount = 7;
        rarity = 2;
        ranged = true;
        firerate = 0.85f;
        auto = false;

        //SET IN PROJECTILE
        //projSpeed = 25f;
        //damage = 15;
    }
}
