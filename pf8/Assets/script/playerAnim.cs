using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class bodyAnim : MonoBehaviour
{

    [Header("script refs")]
    [SerializeField] playerController player;

    [Header("static pose refs")]
    [SerializeField] Sprite stationary;
    [SerializeField] Sprite head;
    [SerializeField] Sprite handCloseA;
    [SerializeField] Sprite handCloseB;
    [SerializeField] Sprite handFarA;
    [SerializeField] Sprite handFarB;

    [Header("gameobject refs")]
    [SerializeField] GameObject bodyObj;
    [SerializeField] GameObject handCloseObj;
    [SerializeField] GameObject handFarObj;
    [SerializeField] GameObject headObj;
    [SerializeField] GameObject playerAssets;

    [Header("walkcycle refs")]
    [SerializeField] Sprite walk1;
    [SerializeField] Sprite walk2;
    [SerializeField] Sprite walk3;
    [SerializeField] Sprite walk4;
    [SerializeField] Sprite walk5;
    [SerializeField] Sprite walk6;

    public Sprite[] walk;
    public int current = 0;
    public int i;
    private int divide;
    private void Awake()
    {
        init("knight");
    }

    public void init(string startingCharacter)
    {
        i = 0;
        walk = new Sprite[] { walk1, walk2, walk3, walk4, walk5, walk6 };
        divide = 60 / player.fps;

        //refs
        handCloseObj = transform.Find("gunHolder/handClose").gameObject;
        handFarObj = transform.Find("gunHolder/handFar").gameObject;
        headObj = transform.Find("headPos/head").gameObject;
        bodyObj = transform.Find("playerBody/body").gameObject;
        playerAssets = GameObject.FindGameObjectWithTag("playerAssets");

    }

    //always 6 frames long
    public void skinSwitch(string toSkin)
    {
        Debug.Log("initiated the thing with "+toSkin);
        //set refs
        if (toSkin == "rogue")
        {
            walk = playerAssets.GetComponent<assets>().rogue_walk;
            head = playerAssets.GetComponent<assets>().rogue_head;
            handCloseA = playerAssets.GetComponent<assets>().rogue_handCloseA;
            handFarA = playerAssets.GetComponent<assets>().rogue_handFarA;
            handCloseB = playerAssets.GetComponent<assets>().rogue_handCloseB;
            handFarB = playerAssets.GetComponent<assets>().rogue_handFarB;
            stationary= playerAssets.GetComponent<assets>().rogue_idle;
        }
        else if (toSkin == "butcher")
        {
            walk = playerAssets.GetComponent<assets>().butcher_walk;
            head = playerAssets.GetComponent<assets>().butcher_head;
            handCloseA = playerAssets.GetComponent<assets>().butcher_handCloseA;
            handFarA = playerAssets.GetComponent<assets>().butcher_handFarA;
            handCloseB = playerAssets.GetComponent<assets>().butcher_handCloseB;
            handFarB = playerAssets.GetComponent<assets>().butcher_handFarB;
            stationary = playerAssets.GetComponent<assets>().butcher_idle;
        }
        else if (toSkin == "samurai")
        {
            walk = playerAssets.GetComponent<assets>().samurai_walk;
            head = playerAssets.GetComponent<assets>().samurai_head;
            handCloseA = playerAssets.GetComponent<assets>().samurai_handCloseA;
            handFarA = playerAssets.GetComponent<assets>().samurai_handFarA;
            handCloseB = playerAssets.GetComponent<assets>().samurai_handCloseB;
            handFarB = playerAssets.GetComponent<assets>().samurai_handFarB;
            stationary = playerAssets.GetComponent<assets>().samurai_idle;
        }
        else if (toSkin == "knight")
        {
            walk = playerAssets.GetComponent<assets>().knight_walk;
            head = playerAssets.GetComponent<assets>().knight_head;
            handCloseA = playerAssets.GetComponent<assets>().knight_handCloseA;
            handFarA = playerAssets.GetComponent<assets>().knight_handFarA;
            handCloseB = playerAssets.GetComponent<assets>().knight_handCloseB;
            handFarB = playerAssets.GetComponent<assets>().knight_handFarB;
            stationary = playerAssets.GetComponent<assets>().knight_idle;
        }

        else if (toSkin == "bull")
        {
            walk = playerAssets.GetComponent<assets>().bull_walk;
            head = playerAssets.GetComponent<assets>().bull_head;
            handCloseA = playerAssets.GetComponent<assets>().bull_handCloseA;
            handFarA = playerAssets.GetComponent<assets>().bull_handFarA;
            handCloseB = playerAssets.GetComponent<assets>().bull_handCloseB;
            handFarB = playerAssets.GetComponent<assets>().bull_handFarB;
            stationary = playerAssets.GetComponent<assets>().bull_idle;
        }

        else if (toSkin == "entity")
        {
            walk = playerAssets.GetComponent<assets>().entity_walk;
            head = playerAssets.GetComponent<assets>().entity_head;
            handCloseA = playerAssets.GetComponent<assets>().entity_handCloseA;
            handFarA = playerAssets.GetComponent<assets>().entity_handFarA;
            handCloseB = playerAssets.GetComponent<assets>().entity_handCloseB;
            handFarB = playerAssets.GetComponent<assets>().entity_handFarB;
            stationary = playerAssets.GetComponent<assets>().entity_idle;
        }


        //refresh non-auto-refresh sprites
        headObj.GetComponent<SpriteRenderer>().sprite = head;
        handCloseObj.GetComponent<SpriteRenderer>().sprite = handCloseA;
        handFarObj.GetComponent<SpriteRenderer>().sprite = handFarA;

    }

    private void moveAnim(int frame, bool forward)
    {

        //moving forward
        if (forward)
        {
            if (frame % divide == 0 && current < 5)
            {
                current++;
            }
            else if (frame % divide == 0)
            {
                current = 0;
            }
        }
        //moving backwards
        else
        {
            if (frame % divide == 0 && current > 0)
            {
                current--;
            }
            else if (frame % divide == 0)
            {
                current = 5;
            }
        }
        bodyObj.GetComponent<SpriteRenderer>().sprite = walk[current];
    }

    private void idleAnim()
    {
        current = 0;
        if (bodyObj != null) { bodyObj.GetComponent<SpriteRenderer>().sprite = stationary; }

    }
    private void FixedUpdate()
    {
        i++;
        if (player.moving && player.forwardMotion)
        {
            moveAnim(i, true);
        }
        else if (player.moving && !player.forwardMotion)
        {
            moveAnim(i, false);
        }
        else
        {
            idleAnim();
        }

    }


}
