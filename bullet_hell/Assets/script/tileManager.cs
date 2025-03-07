using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileManager : MonoBehaviour
{

    [Header("selectswap refs")]
    Sprite crt1Base, crt1Selected;
    Sprite crt2Base, crt2Selected;
    Sprite hellstarBase, hellstarSelected;
    Sprite muteBase, muteSelected;

    [Header("asset refs")]
    musicAssets musicAssets;
    passedData passedData;

    [Header("button refs")]
    public UnityEngine.UI.Button btn_crt1;
    public UnityEngine.UI.Button btn_crt2;
    public UnityEngine.UI.Button btn_hellstar;
    public UnityEngine.UI.Button btn_mute;

    public void init(musicAssets musicAssetsRef, passedData passedDataRef, List<musicKit> kits, List<string> skins, string player)//called by menu
    {
        Debug.Log("i reach this," + player);
        musicAssets = musicAssetsRef;
        passedData = passedDataRef;
        //disable everything first
        btn_crt1.enabled = false;
        btn_crt2.enabled = false;
        btn_hellstar.enabled = false;
        btn_mute.enabled = false;
        //deselect tiles
        setTiles();
        //enable owned
        enableTiles(kits);
        //set active

        if (player == "p1")
        {
            selectKit("p1",passedData.p1Kit);
        }
        else
        {
            selectKit("p2", passedData.p2Kit);
        }
    }

    public void enableTiles(List<musicKit> kits)//called by menu
    {
        if (kits.Contains(musicAssets.crt1Kit))
        {
            btn_crt1.enabled = true;
        }
        if (kits.Contains(musicAssets.crt2Kit))
        {
            btn_crt2.enabled = true;
        }
        if (kits.Contains(musicAssets.hellstarKit))
        {
            btn_hellstar.enabled = true;
        }
        if (kits.Contains(musicAssets.muteKit))
        {
            btn_mute.enabled = true;
        }
    }

    public void setTiles()
    {
        btn_crt1.image.sprite = crt1Base;
        btn_crt2.image.sprite = crt2Base;
        btn_hellstar.image.sprite = hellstarBase;
        btn_mute.image.sprite = muteBase;
    }

    public void selectKit(string player, musicKit kit)//called by menu
    {
        setTiles();
        if (player == "p1")
        {
            passedData.p1Kit = kit;
            if (kit == musicAssets.crt1Kit)
            {
                btn_crt1.image.sprite = crt1Selected;

            }
            else if (kit == musicAssets.crt2Kit)
            {
                btn_crt2.image.sprite = crt2Selected;
            }
            else if (kit == musicAssets.hellstarKit)
            {
                btn_hellstar.image.sprite = hellstarSelected;
            }
            else if (kit == musicAssets.muteKit)
            {
                btn_mute.image.sprite = muteSelected;
            }

        }
        else
        {
            passedData.p2Kit = kit;
            if (kit == musicAssets.crt1Kit)
            {
                btn_crt1.image.sprite = crt1Selected;
            }
            else if (kit == musicAssets.crt2Kit)
            {
                btn_crt2.image.sprite = crt2Selected;
            }
            else if (kit == musicAssets.hellstarKit)
            {
                btn_hellstar.image.sprite = hellstarSelected;
            }
            else if (kit == musicAssets.muteKit)
            {
                btn_mute.image.sprite = muteSelected;
            }
        }

    }
}
