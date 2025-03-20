using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class knife : weapon
{
    [Header("dependencies")]
    public meleeCheck meleeCheck;
    public List<GameObject> collisionWith;
    public meleeTrail trail;
    public Sprite clean;
    public Sprite ink;
    public SpriteRenderer weaponRenderer;

    [Header("logs n settings")]
    public bool check;
    public bool projectileMode;
    private int current;
    public List<Vector3> weaponAnim;//x, y, rotation
    public Vector2 hcAnimOffset;//x,y,rota orignal hcOffset
    public Vector2 hfAnimOffset;//x,y,rota orignal hfOffset
    private int animDuration;

    public override void Fire()
    {
        //firing happens
        if (cooldown <= 0 && magazine > 0)
        {
            check = true;
            cooldown = 60 / firerate;
            //GetComponent<AudioSource>().Play();

            //ANIMATION
            current = 0;
            animDuration = weaponAnim.Count;
        }
    }
    public override void AltFire()
    {
        base.AltFire();
        projectileMode = true;
    }
    private void swing()
    {
        if (animDuration > 0)
        {
            if (animDuration > 0 && frame % 4 == 0)//15fps
            {
                animDuration--;
                weaponOffset[0] = weaponAnim[current].x;//x pos
                weaponOffset[1] = weaponAnim[current].y;//y pos
                weaponOffset[2] = weaponAnim[current].z;//rotation

                handCloseOffset[0] = weaponAnim[current].x + hcAnimOffset.x;
                handCloseOffset[1] = weaponAnim[current].y + hcAnimOffset.y;
                //rotation
                handCloseOffset[2] = weaponAnim[current].z;

                handFarOffset[0] = weaponAnim[current].x + hfAnimOffset.x;
                handFarOffset[1] = weaponAnim[current].y + hfAnimOffset.y;
                //rotation
                handFarOffset[2] = weaponAnim[current].z;
                current++;
            }
        }
        else
        {
            trail.on(false);
        }
        if (animDuration == 3 && check)
        {

            trail.on(true);
            if (meleeCheck.getColl() != null)
            {
                check = false;
                meleeCheck.getColl().GetComponent<playerHealth>().playerDamaged(damage, "ink");
                weaponRenderer.sprite = ink;
            }
        }
    }
    public override void equip(GameObject parent)//parent is gunHolder
    {
        base.equip(parent);
        projectileMode = false;
        meleeCheck.setOwner(parent.GetComponentInParent<playerHealth>().gameObject);
        animDuration = 0;
    }
    public override void flip(bool flip)
    {
        base.flip(flip);
        meleeCheck.flip(flip);
        trail.flip(flip);
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        frame++;
        if (frame > 1000)
        {
            frame = 0;
        }
        swing();
    }
    public override void Awake()
    {
        base.Awake();
        meleeCheck = GetComponentInChildren<meleeCheck>();
        trail = GetComponentInChildren<meleeTrail>();
        weaponRenderer = GetComponent<SpriteRenderer>();
        animDuration = 0;
    }
}