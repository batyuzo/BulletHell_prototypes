using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

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
            //---SHOOTING POINT---
            Vector2 shootingPoint;
            if (flipped)
            {
                shootingPoint = new Vector2(shootingPointObj.transform.position.x, -shootingPointObj.transform.position.y);
            }
            else
            {
                shootingPoint = new Vector2(shootingPointObj.transform.position.x, shootingPointObj.transform.position.y);
            }

            cooldown = 60 / firerate;//60=1s
            magazine--;
            //GetComponent<AudioSource>().Play();

            //---BULLET---
            for (int i = 0; i < 7; i++)
            {
                Instantiate(bullet, shootingPoint, Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + UnityEngine.Random.Range(-15f, 15f)));
            }

            //---MUZZLE FLASH---
            Instantiate(muzzleFlash, shootingPoint, transform.rotation);

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