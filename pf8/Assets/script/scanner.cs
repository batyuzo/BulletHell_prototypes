using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scanner : MonoBehaviour
{
    GameObject me;
    GameObject collisionWith;

    private void Awake()
    {
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    public GameObject getEquippable()
    {
        if (collisionWith != null && collisionWith.CompareTag("weapon"))
        {
            return collisionWith;
        }
        return null;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        collisionWith = collision.gameObject;

    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        collisionWith = null;
    }



    // Update is called once per frame
    void Update()
    {
    }
}
