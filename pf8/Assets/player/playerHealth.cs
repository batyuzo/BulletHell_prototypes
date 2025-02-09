using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    public void Awake()
    {
        maxHealth=gameObject.GetComponent<playerController>().maxHealth;
        currentHealth = maxHealth;
    }

    //player gets damaged
    public void playerDamaged(int dmg)
    {
        if (currentHealth > 0)
        {
            currentHealth -= dmg;
        }
    }

    public void Death()
    {
        if (currentHealth <= 0) { Debug.Log(gameObject.name+"is dead"); }

    }

    public void Update()
    {
        Death();
    }
}
