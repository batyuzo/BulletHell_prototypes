using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class cleaver : weapon
{
    public meleeCheck meleeCheck;
    public List<GameObject> collisionWith;

    public Sprite clean;
    public Sprite ink;
    public SpriteRenderer weaponRenderer;

    private int current;
    public List<Vector3> weaponAnim;//x, y, rotation
    private int animDuration;

    public override void Fire()
    {
        //firing happens
        if (cooldown <= 0 && magazine > 0)
        {
            cooldown = 60 / firerate;
            //GetComponent<AudioSource>().Play();

            //
            if (meleeCheck.getColl() != null)
            {
                meleeCheck.getColl().GetComponent<playerHealth>().playerDamaged(damage, "ink");
                weaponRenderer.sprite = ink;
            }

            //ANIMATION
            current = 0;
            animDuration = weaponAnim.Count;
        }
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
                current++;
                Debug.Log("hey i reach this");
            }
        }
    }
    public override void equip(GameObject parent)//parent is gunHolder
    {
        base.equip(parent);
        meleeCheck.setOwner(parent.GetComponentInParent<playerHealth>().gameObject);
    }
    public override void flip(bool flip)
    {
        base.flip(flip);
        meleeCheck.flip(flip);
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
        weaponRenderer = GetComponent<SpriteRenderer>();
    }
}