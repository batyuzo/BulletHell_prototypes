using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public float[] handCloseOffset = new float[3];
    public float[] handFarOffset = new float[3];
    public float[] weaponOffset = new float[3];
    public int magazine;
    public int rarity;
    public bool ranged;
    public int damage;
    public float projSpeed;
    public float firerate, cooldown;
    //cooldown = 1/firerate -> firerate is given in shots per second
    //firerate = [weapon specific]


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
    }

    public virtual void SetValues()
    {
    }

    public virtual void equip(GameObject parent)
    {
        transform.SetParent(parent.transform);
        SetValues();
    }

    public virtual void FixedUpdate()
    {
        if (cooldown > 0) { cooldown -= .016f; }
    }
}
