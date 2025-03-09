using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class scanner : MonoBehaviour
{
    public List<Collider2D> collisionWith = null;

    public GameObject getEquippable()
    {
        Collider2D toEquip = null;
        if (collisionWith.Count > 0)
        {
            toEquip = collisionWith[0];
            collisionWith.RemoveAt(0);
            return toEquip.gameObject;
        }
        return null;
    }

    public void addDropped(Collider2D dropped)
    {
        collisionWith.Add(dropped);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("weapon"))
        {
            collisionWith.Add(collision);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("weapon"))
        {
            collisionWith.Remove(collision);
        }
    }
}
