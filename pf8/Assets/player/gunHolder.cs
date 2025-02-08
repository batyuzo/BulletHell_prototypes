using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunHolder : MonoBehaviour
{
    void Start()
    {
        Transform gunTransform = transform;
    }

    private void lookAtMouse()
    {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y * -1, dir.x * -1) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void Update()
    {
        lookAtMouse();

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos.x < transform.position.x)
        {
            foreach (Transform child in transform)
            {
                child.GetComponent<SpriteRenderer>().flipY = false;
                if (child.GetSiblingIndex() == 1)
                {
                    child.localRotation = Quaternion.Euler(0, 0, 53);
                }
            }
        }
        else
        {
            foreach (Transform child in transform)
            {
                child.GetComponent<SpriteRenderer>().flipY = true;
                if (child.GetSiblingIndex() == 1)
                {
                    child.localRotation = Quaternion.Euler(0, 0, -53);
                    //can also just offset the transform
                    //child.localPosition += new Vector3(1, 1, 1);
                }
            }
        }
    }
}