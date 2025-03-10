using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class gunHolder : MonoBehaviour
{

    [Header("offset-sheet")]
    //values: posY, posX, angleZ
    public float[] HCO = new float[3];//hand close offset
    public float[] HFO = new float[3];//hand far offset
    public float[] WPO = new float[3];//weapon offset
    private char[] weaponHands;


    public GameObject handClose;
    public GameObject handFar;
    public GameObject equipped = null;
    public GameObject player;
    public GameObject head;

    public bodyAnim playerAnim;
    public scanner scan;
    public TextMeshProUGUI equippedText;

    //this is the little thing that shows your magazine-left
    public GameObject magInfo;
    public TextMeshProUGUI magInfoText;
    public Vector3 offset;

    [Header("weapon script ref")]
    public weapon weaponScript;

    private void updateMaginfo()
    {
        if (equipped != null)
        {
            magInfo.transform.position = player.transform.position + offset;
            magInfoText.SetText(weaponScript.magazine.ToString());
            magInfoText.color = new Color(1, 1, 1, 1);
        }
    }
    private void hideMagInfo()
    {
        magInfoText.color = new Color(1, 1, 1, 0);
    }
    public void lookAt(Vector2 direction)
    {
        transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.y * -1, direction.x * -1) * Mathf.Rad2Deg, Vector3.forward);
        head.transform.rotation = transform.rotation;
        flip(direction.x < 0);
    }
    public void flip(bool flip)
    {
        //Church: Well, give it a flip.
        //Tucker: I don't wanna flip it.
        //Church: What's the problem?
        //Tucker: It's in a weird place.
        //...
        //Church: Did you try wiggling it?
        //Tucker: No way, I'm not wiggling your dongle.
        //Church: Oh, stop being a baby. Just wiggle it.
        if (flip)
        {
            //head flip
            if (head.GetComponentInChildren<SpriteRenderer>() != null)
            {
                head.GetComponentInChildren<SpriteRenderer>().flipY = false;
            }
            //children flip
            foreach (Transform child in transform)
            {
                //check if 'spriterenderer exists'
                if (child.GetComponentInChildren<SpriteRenderer>() != null)
                {
                    child.GetComponentInChildren<SpriteRenderer>().flipY = false;
                }

                //handClose
                if (child.name == "handClose")
                {
                    child.localRotation = Quaternion.Euler(0, 0, HCO[2]);
                    child.localPosition = new Vector3(HCO[0], HCO[1], child.localPosition.z);
                }
                //handFar
                if (child.name == "handFar")
                {
                    child.localRotation = Quaternion.Euler(0, 0, HFO[2]);
                    child.localPosition = new Vector3(HFO[0], HFO[1], child.localPosition.z);
                }

                //weapon
                if (child.CompareTag("weapon"))
                {
                    child.localRotation = Quaternion.Euler(0, 0, WPO[2]);
                    child.localPosition = new Vector3(WPO[0], WPO[1], child.localPosition.z);
                }
            }
        }
        else
        {
            //head flip
            if (head.GetComponentInChildren<SpriteRenderer>() != null)
            {
                head.GetComponentInChildren<SpriteRenderer>().flipY = true;
            }
            //children flip
            foreach (Transform child in transform)
            {
                if (child.GetComponentInChildren<SpriteRenderer>() != null)
                {
                    child.GetComponentInChildren<SpriteRenderer>().flipY = true;
                }

                //handClose
                if (child.name == "handClose")
                {
                    child.localRotation = Quaternion.Euler(0, 0, -HCO[2]);
                    child.localPosition = new Vector3(HCO[0], -HCO[1], child.localPosition.z);
                }
                //handFar
                if (child.name == "handFar")
                {
                    child.localRotation = Quaternion.Euler(0, 0, -HFO[2]);
                    child.localPosition = new Vector3(HFO[0], -HFO[1], child.localPosition.z);
                }

                //weapon
                if (child.CompareTag("weapon"))
                {
                    child.localRotation = Quaternion.Euler(0, 0, -WPO[2]);
                    child.localPosition = new Vector3(WPO[0], -WPO[1], child.localPosition.z);
                }
            }
        }
    }
    public void setOffset()//executes on Equip
    {

        //grab values from children
        HCO = weaponScript.handCloseOffset;
        HFO = weaponScript.handFarOffset;
        WPO = weaponScript.weaponOffset;
        weaponHands = weaponScript.weaponHands;

        //set grabbed values to sprites

        //close hand
        handClose.transform.localPosition = new Vector2(HCO[0], HCO[1]);
        handClose.transform.localRotation = Quaternion.Euler(0, 0, HCO[2]);

        //far hand
        handFar.transform.localPosition = new Vector2(HFO[0], HFO[1]);
        handFar.transform.localRotation = Quaternion.Euler(0, 0, HFO[2]);
        //equipped weapon
        equipped.transform.localPosition = new Vector2(WPO[0], WPO[1]);
        equipped.transform.localRotation = Quaternion.Euler(0, 0, WPO[2]);

        playerAnim.updateHands(weaponHands[0], weaponHands[1]);
    }
    public void bareHandsOffset()
    {
        HCO = new float[] { -.4f, .3f, -65 };
        HFO = new float[] { -.7f, .3f, -65 };
    }
    public bool Fire()
    {
        if (equipped != null)
        {
            weaponScript.Fire();
            return true;
        }
        else
        {
            return false;
        }

    }
    public bool AltFire()
    {
        if (equipped != null)
        {
            weaponScript.AltFire();
            return true;
        }
        else
        {
            return false;
        }
    }
    public void Equip()
    {
        GameObject toEquip = scan.getEquippable();
        if (toEquip != null)
        {
            Drop();
            equipped = toEquip;
            weaponScript = equipped.GetComponent<weapon>();//set weaponscript
            //weaponscript interactions
            weaponScript.equip(gameObject);
            weaponScript.SetValues();
            equippedText.SetText(weaponScript.weaponName);
            //reset positions
            equipped.transform.localPosition = Vector2.zero;
            equipped.transform.localRotation = Quaternion.Euler(0, 0, 0);
            setOffset();
        }
    }
    public void Drop()
    {
        //set hands, forfeit control
        if (equipped != null)
        {
            Rigidbody2D temp = equipped.GetComponent<Rigidbody2D>();//control after drop
            equipped.transform.SetParent(null);
            scan.addDropped(equipped.GetComponent<Collider2D>());
            equipped = null;
            temp.velocity = Vector3.zero;
            temp.angularVelocity = 0;
            //UI+sprite updates
            equippedText.SetText("bare bands");
            playerAnim.updateHands('b', 'b');//fists
            hideMagInfo();
            bareHandsOffset();//fists offset
            temp = null;
        }
    }
    private void Update()
    {
        updateMaginfo();
    }
    private void Awake()
    {
        offset = new Vector3(1f, -1f);
    }
}