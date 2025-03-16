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

            //BULLET
            for (int i = 0; i < pelletCount; i++)//7 pellets
            {

                Instantiate(bullet, shootingPoint.transform.position, Quaternion.Euler(shootingPoint.transform.eulerAngles.x, shootingPoint.transform.eulerAngles.y, shootingPoint.transform.eulerAngles.z * UnityEngine.Random.Range(.9f, 1.1f)));
            }
            //MUZZLE FLASH
            Instantiate(muzzleFlash, shootingPoint.transform.position, shootingPoint.transform.rotation);

            //RECOIL
            currentRecoil = recoil;
        }

    }
    public override void recoilAnim(float speed)
    {
        if (currentRecoil > 0)
        {
            currentRecoil -= speed;
        }
        else
        {
            currentRecoil = 0;
        }
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        frame++;
        if (frame > 1000)
        {
            frame = 0;
        }
        recoilAnim(recoilSpeed);
    }
}