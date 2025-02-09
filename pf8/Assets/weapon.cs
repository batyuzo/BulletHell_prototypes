using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public Rigidbody2D rb;
    public BoxCollider2D coll;
    public float[] handCloseOffset = new float[3];
    public float[] handFarOffset = new float[3];
    public float[] weaponOffset = new float[3];
    public bool active;

    public virtual void Fire()
    {
        //use firing function of a weapon
    }

    public virtual void AltFire()
    {
        //use alt firing function of a weapon
    }

    public virtual void Throw()
    {
        Drop();
        coll.isTrigger = true;
        transform.parent = null;
    }

    public virtual void SetValues()
    {
        //set offset of a weapon
    }

    public virtual void Equip()
    {
        //equip weapon
        active = true;
        Destroy(rb);
        Destroy(coll);
        SetValues();
    }

    public virtual void Drop()
    {
        //drop weapon
        rb.WakeUp();
        SetValues();
    }


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        active = false;
    }
}
