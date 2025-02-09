using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class bodyAnim : MonoBehaviour
{

    [Header("Script refs")]
    [SerializeField] playerController player;

    [Header("Static pose refs")]
    [SerializeField] Sprite stationary;
    [SerializeField] GameObject playerBody;

    [Header("Walkcycle refs")]
    [SerializeField] Sprite walk1;
    [SerializeField] Sprite walk2;
    [SerializeField] Sprite walk3;
    [SerializeField] Sprite walk4;
    [SerializeField] Sprite walk5;
    [SerializeField] Sprite walk6;

    public List<Sprite> walk;
    public int current=0;
    public int i;
    private int divide;
    private void Awake()
    {
        i = 0;
        walk= new List<Sprite> { walk1, walk2, walk3, walk4, walk5, walk6 };
        divide = 60/player.fps;

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
        playerBody.GetComponentInChildren<SpriteRenderer>().sprite = walk[current];   
    }


    private void idleAnim()
    {
        current = 0;
        playerBody.GetComponentInChildren<SpriteRenderer>().sprite = stationary;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }
    //bc fixed fps there
    private void FixedUpdate()
    {
        i++;
        if (player.moving && player.forwardMotion)
        {
            moveAnim(i,true);
        }
        else if(player.moving && !player.forwardMotion)
        {
            moveAnim(i,false);
        }
        else
        {
            idleAnim();
        }

    }

}
