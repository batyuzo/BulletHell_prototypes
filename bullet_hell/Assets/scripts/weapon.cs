using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public GameObject shootingPoint;
    public bool flipped;

    [Header("WEAPON SETTINGS")]
    public float[] handCloseOffset = new float[3];
    public float[] handFarOffset = new float[3];
    public float[] weaponOffset = new float[3];
    public string weaponName;
    public char[] weaponHands;
    public int farLayer;
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
    public virtual void flip(bool flip)//doesn't affect sprite
    {
        flipped = flip;
        if (flip)
        {
            coll.offset = new Vector2(coll.offset.x, -collOffset.y);
            if (ranged)
            {
                shootingPoint.transform.localPosition = new Vector2(shootingPointOffset.x, -shootingPointOffset.y);
            }
        }
        else
        {
            coll.offset = new Vector2(coll.offset.x, collOffset.y);
            if (ranged)
            {
                shootingPoint.transform.localPosition = new Vector2(shootingPointOffset.x, shootingPointOffset.y);
            }
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
    public virtual bool getShootingPoint()//get object + position
    {
        foreach (Transform child in transform)
        {
            if (child.name == "shootingPoint")
            {
                shootingPoint = child.gameObject;
                shootingPointOffset = shootingPoint.transform.localPosition;
                ranged = true;
                return true;
            }
        }
        return false;

    }
    public virtual void Awake()
    {
        ranged = getShootingPoint();
        collOffset = coll.offset;
    }
}
