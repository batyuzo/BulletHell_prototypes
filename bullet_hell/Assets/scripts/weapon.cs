using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    [Header("refs and logs")]
    public Rigidbody2D rb;
    public BoxCollider2D coll;
    public int frame;
    public float currentRecoil;
    public float cooldown;//1 = 1 frame
    public Vector2 collOffset;
    public Vector2 shootingPointOffset;

    [Header("WEAPON SETTINGS")]
    public float[] handCloseOffset = new float[3];
    public float[] handFarOffset = new float[3];
    public float[] weaponOffset = new float[3];
    public string weaponName;
    public char[] weaponHands;
    public int[] handsLayer;
    public int magazine;//single magazine cap
    public int rarity;//unique, rare, common
    public bool ranged;//ranged or melee
    public int damage;//per projectile
    public bool auto;//semi or automatic
    public float recoil;
    public float recoilSpeed;
    public float projSpeed;
    public float firerate;//shots per second


    //FLIP SHOOTING POINT TOO

    public virtual void Fire()
    {
        //use firing function of a weapon
    }
    public virtual void recoilAnim(float speed)
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

    public virtual void flip(bool flip)
    {
        if (flip)
        {
            coll.offset = new Vector2(coll.offset.x, -collOffset.y);
            coll.offset = new Vector2(coll.offset.x, -collOffset.y);
        }
        else
        {
            coll.offset = new Vector2(coll.offset.x, collOffset.y);
        }

    }

    public virtual void AltFire()
    {
        //use alt firing function of a weapon
    }
    public virtual void equip(GameObject parent)
    {
        transform.SetParent(parent.transform);
    }
    public virtual void FixedUpdate()
    {
        if (cooldown > 0) { cooldown -= 1f; }
    }

    public virtual void Awake()
    {
        collOffset = coll.offset;
    }
}
