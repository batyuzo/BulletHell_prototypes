using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class shotgun : weapon
{
    [Header("bullet refs")]
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

            //---BULLET---
            for (int i = 0; i < 7; i++)
            {
                Instantiate(bullet, shootingPointObj.transform.position,transform.rotation);
            }

            //---MUZZLE FLASH---
            Instantiate(muzzleFlash, shootingPointObj.transform.position, shootingPointObj.transform.rotation);

            //---RECOIL---
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

    public override void flip(bool right)
    {
        base.flip(right);

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