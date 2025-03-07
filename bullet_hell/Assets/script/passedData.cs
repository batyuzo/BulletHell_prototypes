using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class passedData : MonoBehaviour
{
    public string p1Name;
    public string p2Name;
    public musicKit p1Kit;
    public musicKit p2Kit;
    public string p1Skin;
    public string p2Skin;
    public int p1Rank;
    public int p2Rank;
    public string p1SkinDesc;
    public string p2SkinDesc;
    public string map;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("passedData");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void defaults(musicKit defaultKit, string defaultSkin, string defaultSkinDesc, string defaultKitDefs, string defaultMap)
    {
        //names default
        p1Name = "p1";
        p2Name = "p2";
        //kits default
        p1Kit = defaultKit;
        p2Kit = defaultKit;
        //zero ranks
        p1Rank = 0;
        p2Rank = 0;
        //skin defaults
        p1Skin = defaultSkin;
        p2Skin = defaultSkin;
        //skindesc defaults
        p1SkinDesc = defaultSkinDesc;
        p2SkinDesc = defaultSkinDesc;
        //map default
        map = defaultMap;//if matchmake dies
    }
}
